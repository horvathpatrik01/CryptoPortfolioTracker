using CryptoPortfolioTracker.Services.Auth;
using Shared;
using Shared.Auth;
using System.Net.Http.Json;
using System.Text.Json;

namespace CryptoPortfolioTracker.Services.User
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
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
                var token = await AuthService.GetAuthToken();
                if (token is null) return null;
                var httpClient = httpClientFactory.CreateClient("httpclient");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
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
                Console.WriteLine(ex.Message);
                return default;
            }
        }
    }
}
