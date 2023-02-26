using System.Text.Json;
using System.Security.Claims;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

namespace WorkflowAutomation.Client
{
    public class JwtTokenProvider 
    {

        private readonly ILocalStorageService _localStorage;

        public JwtTokenProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetJwtToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            return token;
        }
    }
}