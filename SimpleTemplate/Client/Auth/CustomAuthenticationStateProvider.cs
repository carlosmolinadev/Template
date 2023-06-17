using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using System.Diagnostics.CodeAnalysis;

namespace SimpleTemplate.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("token");

            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseTokenClaims(savedToken), "jwt")));
        }

        public void SetUserAuthenticated(string email)
        {
            var authUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "apiauth"));
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void SetUserLoggedOut()
        {
            var anonUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonUser));
            NotifyAuthenticationStateChanged(authState);
        }
        private static bool IsNotNull([NotNullWhen(true)] object? obj) => obj != null;

        private IEnumerable<Claim> ParseTokenClaims(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            if (keyValuePairs!.TryGetValue(ClaimTypes.Role, out object roles))
            {
                if (roles.ToString()!.Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                    foreach (var parsedRole in parsedRoles!)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
