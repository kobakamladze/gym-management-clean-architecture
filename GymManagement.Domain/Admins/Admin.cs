using ErrorOr;
using GymManagement.Domain.Subscriptions;

namespace GymManagement.Domain.Admins;

public class Admin
{
    public Guid Id { get; private set; }
    public Guid UserId { get; }
    public Guid? SubscriptionId { get; private set; }

    public Admin(
        Guid? id,
        Guid userId,
        Guid? subscriptionId = null)
    {
        Id = id ?? Guid.NewGuid();
        SubscriptionId = subscriptionId;
        UserId = userId;
    }

    public void SetSubscription(Subscription subscription)
    {
        if (SubscriptionId.HasValue) throw new Exception("Subscription is already set.");

        SubscriptionId = subscription.Id;
    }

    public void DeleteSubscription(Guid subscriptionId)
    {
        if (!SubscriptionId.HasValue) throw new Exception("Now subscription set.");

        SubscriptionId = null;
    }
    
    private Admin() { }
}