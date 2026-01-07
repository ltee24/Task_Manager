using Microsoft.AspNetCore.Mvc;
using Task_Manager.Models.DTO;
using Task_Manager.Service.IService;

namespace Task_Manager.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDTO _response;

        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDTO();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            var registerationResponse = await _authService.Register(registrationRequestDTO);
            if(!string.IsNullOrEmpty(registerationResponse))
            {
                _response.IsSuccess = false;
                _response.Message = registerationResponse;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _authService.Login(loginRequestDTO);
            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }
    }
}
