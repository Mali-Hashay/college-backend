using College.Core.Entities;

namespace College.Core.Services
{
    public interface IAuthService
    {
        Task<RegisterResult> RegisterAsync(RegisterModel model);
        Task<JwtToken?> LoginAsync(LoginModel model);
    }
}
