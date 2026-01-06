using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Task_Manager.Data;
using Task_Manager.Models;
using Task_Manager.Models.DTO;
using Task_Manager.Service.IService;

namespace Task_Manager.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private string secretKey;
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
            _mapper = mapper;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            var user = _db.ApplicationUsers.First(u=>u.UserName.ToLower() == requestDTO.Username.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, requestDTO.Password);
            if(user == null || !isValid)
            {
                return new LoginResponseDTO() { User = null, Token = "" };
            }
            var roles = await _userManager.GetRolesAsync(user);

            //if user wis found generate JWT Token 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponse = new()
            {
                User = _mapper.Map<UserDTO>(user),
                Token = tokenHandler.WriteToken(token)

            };
            return loginResponse;
        }

        public async Task<string> Register(RegistrationRequestDTO requestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = requestDTO.Email,
                NormalizedEmail = requestDTO.Email.ToUpper(),
                Name = requestDTO.Name,
                PhoneNumber = requestDTO.PhoneNumber,
                Email = requestDTO.Email,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, requestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == requestDTO.Email);
                    UserDTO userDTO = new()
                    {
                        Id = userToReturn.Id,
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }
    }
}
