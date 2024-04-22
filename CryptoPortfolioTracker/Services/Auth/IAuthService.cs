using Shared.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Services.Auth
{
    public interface IAuthService
    {
        Task<RegisterResult?> Register(RegisterModel model);
        Task<LoginResult?> Login(LoginModel model);
        bool Logout();
    }
}
