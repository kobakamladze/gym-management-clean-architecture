using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.ListGyms;

public class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
{
    private readonly IGymRepository _gymRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public ListGymsQueryHandler(
        IGymRepository gymRepository,
        ISubscriptionsRepository subscriptionsRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }
    
    public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery request, CancellationToken cancellationToken)
    {
        if (!await _subscriptionsRepository.ExistsAsync(request.SubscriptionId))
            return Error.NotFound(description: "Subscription not found.");

        return await _gymRepository.ListBySubscriptionIdAsync(request.SubscriptionId);
    }
}