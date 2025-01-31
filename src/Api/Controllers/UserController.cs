using Controller.Application.Interfaces;
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

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("create/user")]
        public async Task<IActionResult> CreateUser(UserRequest userRequest, CancellationToken cancellationToken)
        {
            await _userApplication.CreateUserAsync(userRequest);

            return Ok();
        }

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserRequest userRequest, CancellationToken cancellationToken)
        {
            var response = await _userApplication.AuthenticateUserAsync(userRequest, cancellationToken);

            return Ok(response);
        }
    }
}