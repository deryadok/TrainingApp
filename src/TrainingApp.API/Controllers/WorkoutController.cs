using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TrainingApp.Application.Dtos.Workout;
using TrainingApp.Application.Services;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.RequestModel;

namespace TrainingApp.API.Controllers
{
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
        public IActionResult Insert([FromBody] ApiRequest<WorkoutInsertDto> request)
        {
            try
            {
                var data = request.Payload;
                var response = _workoutService.AddWorkout(data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Hata durumunda cevap dön
                return BadRequest(ResponseHelper<Guid>.GetResponse(true, ex.Message));
            }
        }
    }
}
