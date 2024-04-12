using Database.DatabaseContext;
using Microsoft.AspNetCore.Identity;

namespace Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly UserManager<User> userManager;

        public UserRepository(AppDbContext appDbContext, UserManager<User> userManager)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }

        public async Task<User?> ChangeEmailAddress(Guid? id, string? newEmailAddress)
        {
            User? user = await appDbContext.Users.FindAsync(id);
            if (user is not null)
            {
                await userManager.SetEmailAsync(user,newEmailAddress);
            }

            return user;
        }

        public async Task<User?> ChangeUserName(Guid? id, string? newUsername)
        {
            User? user =await appDbContext.Users.FindAsync(id);
            if(user is not null)
            {
                await userManager.SetUserNameAsync(user,newUsername);
            }

            return user;
        }

        public async Task<User?> DeleteUser(Guid? id)
        {
            User? user = await appDbContext.Users.FindAsync(id);
            if (user is not null)
            {
                await userManager.DeleteAsync(user);
            }

            return user;
        }

        public async Task<User?> GetUser(Guid? id)
        {
            return await appDbContext.Users.FindAsync(id);
        }
    }
}
