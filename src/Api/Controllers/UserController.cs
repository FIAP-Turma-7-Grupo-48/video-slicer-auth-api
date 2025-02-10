using Controller.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCase.Dtos;

namespace video_authenticator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserApplication _userApplication;

        public UserController(ILogger<UserController> logger, IUserApplication userApplication)
        {
            _logger = logger;
            _userApplication = userApplication;
        }

        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest userRequest, CancellationToken cancellationToken)
        {
            await _userApplication.CreateUserAsync(userRequest);

            return NoContent();
        }

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserAuthenticateRequest userRequest, CancellationToken cancellationToken)
        {
            var response = await _userApplication.AuthenticateUserAsync(userRequest, cancellationToken);

            return Ok(response);
        }
    }
}