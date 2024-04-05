using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.DeleteGym;

public class DeleteGymCommandHandler : IRequestHandler<DeleteGymCommand, ErrorOr<Deleted>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGymCommandHandler(
        IGymRepository gymRepository,
        ISubscriptionsRepository subscriptionsRepository,
        IUnitOfWork unitOfWork)
    {
        _gymRepository = gymRepository;
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Deleted>> Handle(DeleteGymCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        
        if (gym is null) return Error.NotFound(description: "Gym not found.");
        
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        
        if (subscription is null) return Error.NotFound(description: "Subscription not found.");
        if (!subscription.HasGym(gym.Id)) return Error.Unexpected(description: "Gym not found.");
        
        subscription.RemoveGym(gym.Id);

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymRepository.RemoveGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();
        
        return Result.Deleted;
    }
}