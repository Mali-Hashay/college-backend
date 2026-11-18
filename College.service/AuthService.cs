using College.Core.Entities;
using College.Core.Repositories;
using College.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace College.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly JwtService _jwtService;

        public AuthService(
            IAppUserRepository appUserRepository,
            IStudentRepository studentRepository,
            IPasswordHasher<AppUser> passwordHasher,
            JwtService jwtService)
        {
            _appUserRepository = appUserRepository;
            _studentRepository = studentRepository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
                return RegisterResult.PasswordsDoNotMatch;

            var existingUser = await _appUserRepository.GetByEmailAsync(model.Email);
            if (existingUser != null)
                return RegisterResult.EmailAlreadyExists;

            var appUser = new AppUser
            {
                Email = model.Email,
                Role = UserRole.Student
            };

            appUser.PasswordHash = _passwordHasher.HashPassword(appUser, model.Password);
            await _appUserRepository.AddAsync(appUser);

            var student = new Student
            {
                UserId = appUser.Id,
                Name = model.Name
            };

            await _studentRepository.PostAsync(student);
            return RegisterResult.Success;
        }

        public async Task<JwtToken?> LoginAsync(LoginModel model)
        {
            var appUser = await _appUserRepository.GetByEmailAsync(model.Email);
            if (appUser == null || string.IsNullOrEmpty(appUser.PasswordHash))
                return null;

            var passwordResult = _passwordHasher.VerifyHashedPassword(
                appUser,
                appUser.PasswordHash,
                model.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
                return null;

            return _jwtService.GenerateToken(appUser);
        }
    }
}
