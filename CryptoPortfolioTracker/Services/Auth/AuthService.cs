using Shared.Auth;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CryptoPortfolioTracker.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<RegisterResult?> Register(RegisterModel model)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("httpclient");
                var result = await httpClient.PostAsJsonAsync("api/Register", model);
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<RegisterResult>();
                }
                else
                {
                    var message = await result.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public async Task<LoginResult?> Login(LoginModel model)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("httpclient");
                var loginAsJson = JsonSerializer.Serialize(model);
                var response = await httpClient.PostAsync("api/Login",
                    new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
                var loginResult = JsonSerializer.Deserialize<LoginResult>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (!response.IsSuccessStatusCode || loginResult is null)
                {
                    return loginResult;
                }
                await SecureStorage.Default.SetAsync("auth", JsonSerializer.Serialize(loginResult.Token));

                return loginResult;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Logout()
        {
            return SecureStorage.Default.Remove("auth");
        }

        public static async Task<string?> GetAuthToken()
        {
            var serializedAuthToken = await SecureStorage.Default.GetAsync("auth");
            if (serializedAuthToken is null) return null;
            return JsonSerializer.Deserialize<string>(serializedAuthToken);
        }
    }
}
