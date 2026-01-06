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
            var errorMessage = await _authService.Register(registrationRequestDTO);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
