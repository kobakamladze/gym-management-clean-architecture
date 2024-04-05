using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription;

public record class CreateSubscriptionCommand(
    SubscriptionType Subscription,
    Guid AdminId) : IRequest<ErrorOr<Subscription>>;