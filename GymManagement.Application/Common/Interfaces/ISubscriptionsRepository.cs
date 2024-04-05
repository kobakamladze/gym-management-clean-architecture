using GymManagement.Domain.Subscriptions;

namespace GymManagement.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<bool> ExistsAsync(Guid id);
    Task<Subscription?> GetByIdAsync(Guid subscriptionId);
    Task<List<Subscription>> ListAsync();
    Task RemoveSubscriptionAsync(Guid subscriptionId);
    Task UpdateAsync(Subscription subscription);
}