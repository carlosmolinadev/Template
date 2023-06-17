using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleTemplate.Client.Auth;
using SimpleTemplate.Client.Contracts;
using SimpleTemplate.Client.Services.Base;
using System.Net.Http.Headers;

namespace SimpleTemplate.Client.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Register(RegistrationRequest request)
        {
            try
            {
                var response = await _client.RegisterAsync(request);

                if (!string.IsNullOrEmpty(response.UserId))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public Task<bool> AddToRoles(string request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Authenticate(AuthenticationRequest request)
        {
            try
            {
                AuthenticationRequest authenticationRequest = new AuthenticationRequest() { Username = request.Username, Password = request.Password };
                var authenticationResponse = await _client.AuthenticateAsync(authenticationRequest);

                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.Token);
                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(request.Username);
                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResponse.Token);
                    return true;
                }
                
            }
            catch
            {
                throw;
            }
            return false;
        }

        public Task<bool> ChangePassword(Application.Requests.ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(Application.Requests.RegistrationRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(Application.Requests.ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
