using Shared.Auth;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CryptoPortfolioTracker.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<RegisterResult?> Register(RegisterModel model)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);
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
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<LoginResult?> Login(LoginModel model)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);
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
                await SecureStorage.Default.SetAsync(AppConstants.AuthStorageKey, loginResult.Token!);

                return loginResult;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Logout()
        {
            return SecureStorage.Default.Remove(AppConstants.AuthStorageKey);
        }

        public static async Task<string?> GetAuthToken()
        {
            var serializedAuthToken = await SecureStorage.Default.GetAsync(AppConstants.AuthStorageKey);
            return serializedAuthToken;
        }

        public async Task<HttpClient?> GetAuthenticatedHttpClient()
        {
            var token = await GetAuthToken();
            if (token is null) return null;
            var httpClient = _httpClientFactory.CreateClient(AppConstants.HttpClientName);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }

        public async Task<bool> IsTokenExpired()
        {
            try
            {
                var token = await SecureStorage.Default.GetAsync(AppConstants.AuthStorageKey);
                if (token == null)
                    return false;
                // Define token validation parameters
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Check if the token has an "exp" claim
                if (jwtToken == null || !jwtToken.Payload.ContainsKey("exp"))
                {
                    return false; // No expiration claim found; treat as expired
                }

                // Get the expiration time from the token
                var expirationTimeUnix = long.Parse(jwtToken.Payload["exp"].ToString());
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expirationTimeUnix).DateTime;

                // Compare the expiration time with the current time
                return DateTime.Now > expirationTime;
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., invalid token format
                Debug.WriteLine($"Error checking token expiration: {ex.Message}");
                return false; // Treat as expired on error
            }
        }
    }
}