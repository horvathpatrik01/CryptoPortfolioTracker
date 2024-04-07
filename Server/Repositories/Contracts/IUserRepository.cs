namespace Server.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUser(Guid? id);
        Task<User> ChangeUserName(Guid? id,string? newUsername);
        Task<User> DeleteUser(Guid? id);
    }
}
