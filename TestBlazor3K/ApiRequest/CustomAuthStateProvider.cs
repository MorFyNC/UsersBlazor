using Blazor.Extensions.Storage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using TestBlazor3K.ApiRequest.Model;
using TestBlazor3K.ApiRequest.Services;

namespace TestBlazor3K.ApiRequest
{
    public class CustomAuthStateProvider: AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private ILogger<CustomAuthStateProvider> _logger;
        public CustomAuthStateProvider(HttpClient httpClient, ILogger<CustomAuthStateProvider> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            var data = UserService.CurrentUserData;
            if(data is null)
            {
                identity = new ClaimsIdentity();
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }

            identity = new ClaimsIdentity(JWTService.ParseClaimsFromJwt(data.JWT));

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
