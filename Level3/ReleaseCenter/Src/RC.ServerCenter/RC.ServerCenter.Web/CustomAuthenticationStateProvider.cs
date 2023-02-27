using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace RC.ServerCenter.Web
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISyncLocalStorageService _localStorage;
        public CustomAuthenticationStateProvider(ISyncLocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        private const string _tokenKey = "Token";
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal? claimsPrincipal = null;
            try
            {
                string? token = GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    Claim[] claims = new[]
                    {
                        new Claim("Token",token)
                    };
                    claimsPrincipal = new(new ClaimsIdentity(claims, "CustomAuthenticationState"));
                }
            }
            catch
            {

            }
            AuthenticationState result = new(claimsPrincipal ?? new());
            return Task.FromResult(result);
        }
        public string? GetToken()
        {
            string? token = _localStorage.GetItem<string>(_tokenKey);
            return token;
        }
        public void SetToken(string token)
        {
            _localStorage.SetItem(_tokenKey, token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public void LoginOut()
        {
            _localStorage.RemoveItem(_tokenKey);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
