using Task_Manager.Models.DTO;

namespace Task_Manager.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO requestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO);
    }
}
