using GymManagement.Domain.Admins;

namespace GymManagement.Application.Common.Interfaces;

public interface IAdminRepository
{
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
}