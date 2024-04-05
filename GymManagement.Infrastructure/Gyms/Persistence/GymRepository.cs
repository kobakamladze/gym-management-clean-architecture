using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence;

public class GymRepository : IGymRepository
{
    private readonly GymManagementDbContext _dbContext;

    public GymRepository(GymManagementDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task AddGymAsync(Gym gym)
    {
        await _dbContext.Gyms.AddAsync(gym);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Gyms.FindAsync(id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Gyms
            .AsNoTracking()
            .AnyAsync(g => g.Id == id);
    }

    public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
    {
        return await _dbContext.Gyms
            .Where(g => g.SubscriptionId == subscriptionId)
            .ToListAsync();
    }

    public Task UpdateGymAsync(Gym gym)
    {
        _dbContext.Gyms.Update(gym);
        return Task.CompletedTask;
    }

    public async Task RemoveGymAsync(Gym gym)
    {
        await _dbContext.Gyms
            .Where(g => g.Id == gym.Id)
            .ExecuteDeleteAsync();
    }

    public Task RemoveRangeAsync(List<Gym> gyms)
    {
        _dbContext.Gyms.RemoveRange(gyms);
        return Task.CompletedTask;
    }
}