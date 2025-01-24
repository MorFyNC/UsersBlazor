using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;
using TestBlazor3K.ApiRequest.Model;

namespace TestBlazor3K.ApiRequest
{
    public class ApiRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiRequestService> _logger;

        public ApiRequestService(HttpClient httpClient, ILogger<ApiRequestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<UserData> GetAllUsersAsync()
        {
            var url = "api/UsersLogins/getAllUsers";

            try
            {
                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (string.IsNullOrEmpty(responseContent))
                {
                    _logger.LogWarning("Ответ от сервера пуст.");
                    return new UserData();
                }

                var usersData = JsonSerializer.Deserialize<UserData>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return usersData ?? new UserData();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе");
                return new UserData();
            }
        }

        public async Task<UserAddData> AddNewUser(ReqDataUser user)
        {
            var url = "api/UsersLogins/createNewUserAndLogin";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, user);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var userAddData = JsonSerializer.Deserialize<UserAddData>(responseContent);

                return userAddData ?? new UserAddData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе: {ex.Message}");
                return new UserAddData();
            }
        }

        public async Task<LoginResponseModel> LoginAsync(ReqUserLogin reqUserLogin)
        {
            var url = "api/Auth/login";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, reqUserLogin);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var LoginResponse = JsonSerializer.Deserialize<LoginResponseModel>(responseContent);

                return LoginResponse ?? new LoginResponseModel();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Ошибка при запросе: {e.Message}");
                return new LoginResponseModel();
            }
        }
        
        public async Task<bool> ValidateJWT()
        {
            var url = "api/Auth/checkAuth";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, "");

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var isJWTCorrect = JsonSerializer.Deserialize<JWTStatus>(responseContent);

                return isJWTCorrect.status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        record class JWTStatus(bool status);
    }
}
