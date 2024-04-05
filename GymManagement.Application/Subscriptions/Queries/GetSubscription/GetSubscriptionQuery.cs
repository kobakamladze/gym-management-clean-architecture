using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Queries.GetSubscription;

public record class GetSubscriptionQuery(Guid SubscriptionId): IRequest<ErrorOr<Subscription>>;