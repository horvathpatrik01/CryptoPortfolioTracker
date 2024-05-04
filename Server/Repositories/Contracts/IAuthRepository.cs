namespace Server.Repositories.Contracts
{
    public interface IAuthRepository
    {
        Task<RegisterResult> CreateAccount(RegisterModel registerModel);
    }
}
