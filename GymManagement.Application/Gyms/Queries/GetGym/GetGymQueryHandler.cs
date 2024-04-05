using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly IGymRepository _gymRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetGymQueryHandler(
        IGymRepository gymRepository,
        ISubscriptionsRepository subscriptionsRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        if (! await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
            return Error.NotFound(description: "Subscription not found.");
        
        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        if (gym is null) return Error.NotFound(description: "Gym not found.");

        return gym;
    }
}