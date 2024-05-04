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

        public UserService(IAuthService authService)
        {
            this._authService = authService;
        }

        public async Task<UserInfoDto?> ChangeEmailAddress(string newEmailAddress)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;
                var response = await httpClient.PatchAsync($"api/User/ChangeEmailAddress/{newEmailAddress}", null);

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

        public async Task<UserInfoDto?> ChangeUsername(string newUsername)
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;
                var response = await httpClient.PatchAsync($"api/User/{newUsername}", null);

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

        public async Task<UserInfoDto?> DeleteAccount()
        {
            try
            {
                var httpClient = await _authService.GetAuthenticatedHttpClient();
                if (httpClient is null) return null;
                var response = await httpClient.DeleteAsync("api/User");

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