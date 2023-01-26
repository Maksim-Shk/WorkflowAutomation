using WorkflowAutomation.Shared;
using WorkflowAutomation.Shared.Identity;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;

namespace WorkflowAutomation.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage, 
            IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(WorkflowHttpClientDefaults.Default);
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterDto registerDto)
        {
            var registerAsJson = JsonSerializer.Serialize(registerDto);
            var response = await _httpClient.PostAsync("/Accounts", new StringContent(registerAsJson, Encoding.UTF8, "application/json"));
            var registerResult = JsonSerializer.Deserialize<RegisterResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return registerResult;
        }

        public async Task<LoginResult> Login(LoginDto loginDto)
        {
            var loginAsJson = JsonSerializer.Serialize(loginDto);
            var response = await _httpClient.PostAsync("/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync("authToken", loginResult.Token);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginDto.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
