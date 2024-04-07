using Database.DatabaseContext;
using Database.Entities;
using Microsoft.AspNetCore.Identity;
using Server.DTOs.Auth;

namespace Server.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;

        public AuthRepository(UserManager<User> userManager,AppDbContext appDbContext)
        {
            this._userManager = userManager;
            this._appDbContext = appDbContext;
        }

        public async Task<RegisterResult> CreateAccount(RegisterModel registerModel)
        {
            try
            {
                var newUser = new User
                {
                    UserName = registerModel.Email,
                    Email = registerModel.Email,
                    CreatedTimeStamp = DateTime.Now,
                };
                var result = await this._userManager.CreateAsync(newUser, registerModel.Password);


                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);

                    return new RegisterResult { Successful = false, Errors = errors };

                }
                return new RegisterResult { Successful = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
