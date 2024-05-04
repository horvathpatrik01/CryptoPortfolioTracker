namespace Server.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUser(Guid? id);
        Task<User?> ChangeUserName(Guid? id,string? newUsername);
        Task<User?> ChangeEmailAddress(Guid? id, string? newEmailAddress);
        Task<User?> DeleteUser(Guid? id);
    }
}
