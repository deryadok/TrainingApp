using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos;
using TrainingApp.Application.Services;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.RequestModel;

namespace TrainingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userService = new UserService(userRepository, mapper);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ApiRequest<UserDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _userService.AddUser(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }
    }
}
