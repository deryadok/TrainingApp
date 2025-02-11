using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos.Workout;
using TrainingApp.Application.Services;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.RequestModel;

namespace TrainingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutController : Controller
    {
        private readonly WorkoutService _workoutService;

        public WorkoutController(IWorkoutRepository workoutRepository, 
            IWorkoutRegionRepository workoutRegionRepository,
            IMapper mapper)
        {
            _workoutService = new WorkoutService(workoutRepository, workoutRegionRepository, mapper);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ApiRequest<WorkoutInsertDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _workoutService.Add(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<Guid>.GetResponse(true, ex.Message));
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ApiRequest<WorkoutUpdateDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _workoutService.Update(data);
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
                var response = await _workoutService.Delete(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetWorkoutById")]
        public async Task<IActionResult> GetWorkoutById([FromQuery] Guid userId)
        {
            try
            {
                var response = await _workoutService.GetWorkoutById(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpGet("GetAllWorkouts")]
        public async Task<IActionResult> GetAllWorkouts()
        {
            try
            {
                var response = await _workoutService.GetAllWorkouts();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<int>.GetResponse(true, ex.Message));
            }
        }

        [HttpPost("SearchWorkouts")]
        public async Task<IActionResult> SearchWorkouts([FromBody] ApiRequest<WorkoutFilterDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = await _workoutService.GetFilteredResult(data);
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
