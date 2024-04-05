using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, ErrorOr<Gym>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateGymCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymRepository gymRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Gym>> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null) return Error.NotFound(description: "Subscription not found.");
        
        var gym = new Gym(
            name: request.Name,
            maxRooms: subscription.GetMaxRooms(),
            subscriptionId: subscription.Id);

        var addGymResult = subscription.AddGym(gym);
        if (addGymResult.IsError) return addGymResult.Errors;

        await _subscriptionsRepository.UpdateAsync(subscription);
        await _gymRepository.AddGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return gym;
    }
}