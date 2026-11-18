using College.Core.Entities;
using College.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace College.collegeAPI
{
    public static class DevelopmentAdminSeeder
    {
        public static async Task SeedAsync(
            IServiceProvider services,
            IConfiguration configuration)
        {
            var email = configuration["Admin:Email"];
            var password = configuration["Admin:Password"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;

            var userRepository = services.GetRequiredService<IAppUserRepository>();
            var passwordHasher = services.GetRequiredService<IPasswordHasher<AppUser>>();

            var existingUser = await userRepository.GetByEmailAsync(email);
            if (existingUser != null)
                return;

            var admin = new AppUser
            {
                Email = email,
                Role = UserRole.Admin
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, password);
            await userRepository.AddAsync(admin);
        }
    }
}
