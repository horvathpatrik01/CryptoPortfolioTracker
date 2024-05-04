using Microsoft.AspNetCore.Identity;

namespace Server.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;

        public AuthRepository(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<RegisterResult> CreateAccount(RegisterModel registerModel)
        {
            try
            {
                var newUser = new User
                {
                    UserName = registerModel.UserName,
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
