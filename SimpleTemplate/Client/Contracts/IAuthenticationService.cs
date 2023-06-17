using SimpleTemplate.Client.Services.Base;

namespace SimpleTemplate.Client.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(AuthenticationRequest request);
        Task<bool> Register(RegistrationRequest request);
        Task Logout();
    }
}
