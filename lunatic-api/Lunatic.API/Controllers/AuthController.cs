using Lunatic.Application.Contracts.Identity;
using Lunatic.Application.Models.Identity;
using Lunatic.Identity.Models;
using Microsoft.AspNetCore.Mvc;


namespace Lunatic.API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger) {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model) {
            try {
                if(!ModelState.IsValid) {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if(status == 0) {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model) {
            try {
                if(!ModelState.IsValid) {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Registeration(model, UserRoles.User);

                if(status == 0) {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
