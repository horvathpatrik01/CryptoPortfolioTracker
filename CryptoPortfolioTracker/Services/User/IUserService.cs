using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Services.User
{
    public interface IUserService
    {
        Task<UserInfoDto?> GetUserInformation();

        Task<UserInfoDto?> ChangeUsername(string newUsername);

        Task<UserInfoDto?> ChangeEmailAddress(string newEmailAddress);

        Task<UserInfoDto?> DeleteAccount();
    }
}