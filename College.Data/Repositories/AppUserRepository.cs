using College.Core.Entities;
using College.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace College.Data.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly DataContext _context;

        public AppUserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _context.AppUsers
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            return await _context.AppUsers
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task AddAsync(AppUser user)
        {
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
