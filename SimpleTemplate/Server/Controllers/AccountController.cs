using Microsoft.AspNetCore.Mvc;
using SimpleTemplate.Application.Contracts;
using SimpleTemplate.Application.Requests;
using SimpleTemplate.Application.Responses;

namespace SimpleTemplate.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, typeof(AuthenticationRequest));
            }
            
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {

            return Ok(await _authenticationService.RegisterAsync(request));
        }
    }
}
