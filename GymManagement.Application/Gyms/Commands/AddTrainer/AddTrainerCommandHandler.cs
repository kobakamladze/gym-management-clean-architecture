using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.AddTrainer;

public class AddTrainerCommandHandler : IRequestHandler<AddTrainerCommand, ErrorOr<Success>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IGymRepository _gymRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddTrainerCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IGymRepository gymRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _gymRepository = gymRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ErrorOr<Success>> Handle(AddTrainerCommand request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null) return Error.NotFound(description: "Subscription not found.");

        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        if (gym is null) return Error.NotFound(description: "Gym not found.");

        gym.AddTrainer(request.TrainerId);

        await _gymRepository.UpdateGymAsync(gym);
        await _unitOfWork.CommitChangesAsync();

        return Result.Success;
    }
}