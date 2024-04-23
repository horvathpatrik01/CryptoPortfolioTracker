using CryptoPortfolioTracker.Services.Auth;
using Shared;
using Shared.Auth;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace CryptoPortfolioTracker.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;

        public UserService(IHttpClientFactory httpClientFactory,
            IAuthService authService)
        {
            this._authService = authService;
        }

        public Task<UserInfoDto> ChangeEmailAddress()
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoDto> ChangeUsername(string newUsername)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfoDto> DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfoDto?> GetUserInformation()
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;
                var response = await httpClient.GetAsync("api/User");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                UserInfoDto? userInfo = await response.Content.ReadFromJsonAsync<UserInfoDto>();
                return userInfo;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}