using College.Core.Entities;

namespace College.Core.Repositories
{
    public interface IAppUserRepository
    {
        Task<AppUser?> GetByEmailAsync(string email);
        Task<AppUser?> GetByIdAsync(int id);
        Task AddAsync(AppUser user);
    }
}
