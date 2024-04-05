using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins;

public class AdminRepository : IAdminRepository
{
    private readonly GymManagementDbContext _dbContext;

    public AdminRepository(GymManagementDbContext dbContext) =>
        _dbContext = dbContext;
    
    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await _dbContext.Admins.FindAsync(adminId);
    }

    public Task UpdateAsync(Admin admin)
    {
        _dbContext.Admins.Update(admin);

        return Task.CompletedTask;
    }
}