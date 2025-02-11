using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos.Exercise;
using TrainingApp.Application.Services;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.RequestModel;

namespace TrainingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        private readonly ExerciseService _exerciseService;

        public ExerciseController(IExerciseRepository exerciseRepository,
            IWorkoutExerciseRepository workoutExerciseRepository,
            IMapper mapper)
        {
            _exerciseService = new ExerciseService(exerciseRepository, workoutExerciseRepository, mapper);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ApiRequest<ExerciseInsertDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _exerciseService.Add(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<Guid>.GetResponse(true, ex.Message));
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ApiRequest<ExerciseUpdateDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _exerciseService.Update(data);
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
                var response = await _exerciseService.Delete(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetExerciseById")]
        public async Task<IActionResult> GetExerciseById([FromQuery] Guid userId)
        {
            try
            {
                var response = await _exerciseService.GetExerciseById(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetAllExercises")]
        public async Task<IActionResult> GetAllExercises()
        {
            try
            {
                var response = await _exerciseService.GetAllExercises();
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
