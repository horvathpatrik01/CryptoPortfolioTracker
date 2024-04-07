using Server.DTOs.Auth;

namespace Server.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<RegisterResult> CreateAccount(RegisterModel registerModel);
    }
}
