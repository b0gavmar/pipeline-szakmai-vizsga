using PipeLine.HttpService.Dtos;
using PipeLine.Shared.Dtos.UserDtos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PipeLine.HttpService.Services
{
    public class LoginHttpService : ILoginHttpService
    {
        private readonly HttpClient _httpClient;

        public LoginHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7020/api/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string?> LoginAsync(LoginRegisterRequest loginDto)
        {
            var json = JsonSerializer.Serialize(loginDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Authentication/adminLogin", content);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return token;
            }

            return null;
        }

        public async Task<string?> RegisterAsync(LoginRegisterRequest registerDto)
        {
            var json = JsonSerializer.Serialize(registerDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Authentication/registerAsAdmin", content);

            if (response.IsSuccessStatusCode)
            {
                return "success";
            }

            return null;
        }

        public async Task<bool> LogoutAsync()
        {
            var response = await _httpClient.PostAsync("/api/Authentication/logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}
