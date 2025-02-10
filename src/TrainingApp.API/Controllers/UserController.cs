using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos.User;
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
        public async Task<IActionResult> Insert([FromBody] ApiRequest<UserInsertDto> request)
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

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ApiRequest<UserUpdateDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _userService.UpdateUser(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] ApiRequest<Guid> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _userService.DeleteUser(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] Guid userId)
        {
            try
            {
                var response = await _userService.GetUserById(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var response = await _userService.GetAllUsers();
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
