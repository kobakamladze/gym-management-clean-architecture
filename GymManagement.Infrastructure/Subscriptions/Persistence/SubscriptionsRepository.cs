using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions.Persistence;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly GymManagementDbContext _dbContext;

    public SubscriptionsRepository(GymManagementDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Subscriptions
            .AsNoTracking()
            .AnyAsync(s => s.Id ==id);
    }

    public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
    {
        return await _dbContext.Subscriptions.FindAsync(subscriptionId);
    }

    public async Task<List<Subscription>> ListAsync()
    {
        return await _dbContext.Subscriptions.ToListAsync();
    }

    public async Task RemoveSubscriptionAsync(Guid subscriptionId)
    {
        await _dbContext.Subscriptions
            .Where(s => s.Id == subscriptionId)
            .ExecuteDeleteAsync();
    }

    public Task UpdateAsync(Subscription subscription)
    {
        _dbContext.Update(subscription);
        return Task.CompletedTask;
    }
}