using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainingApp.API.Request;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Constants;
using TrainingApp.Shared.Helpers;

namespace TrainingApp.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository,
            IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _authRepository.GetUserByUsernameAsync(model.Username);

            PasswordVerificationResult result = PasswordHelper.VerifyPassword(model.Username, model.Password);

            if (user == null || result != PasswordVerificationResult.Success)
            {
                return Unauthorized(new { message = ResultMessages.InvalidLoginMessage });
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel model)
        {
            var existingUser = await _authRepository.GetUserByUsernameAsync(model.Username);

            if (existingUser != null)
                return BadRequest(new { message = ResultMessages.UsernameInUseMessage });            

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Username = model.Username,
                Email = model.Email,
                Password = PasswordHelper.HashPassword(model.Username, model.Password),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid() // Varsayılan admin ID veya sistem tarafından atanır
            };

            await _authRepository.RegisterUserAsync(newUser);

            var token = GenerateJwtToken(newUser);
            return Ok(new { token });
        }

        private string GenerateJwtToken(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.PreferredUsername, user.Username),
            };

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
