
using SimpleTemplate.Application.Requests;
using SimpleTemplate.Application.Responses;

namespace SimpleTemplate.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AddRoles(AddRolesRequest request);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<bool> ChangePassword(ChangePasswordRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
        Task<bool> ResetPassword(ChangePasswordRequest request);
    }
}
