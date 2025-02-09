using AutoMapper;
using TrainingApp.Application.Dtos;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.Response;

namespace TrainingApp.Application.Services
{
    public class WorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IWorkoutRegionRepository _workoutRegionRepository;
        private readonly IMapper _mapper;

        public WorkoutService(IWorkoutRepository workoutRepository, 
            IWorkoutRegionRepository workoutRegionRepository,
            IMapper mapper)
        {
            _workoutRepository = workoutRepository;
            _workoutRegionRepository = workoutRegionRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Guid>> AddWorkout(WorkoutDto workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);

            var result = await _workoutRepository.AddWorkoutAsync(workout);

            return ResponseHelper<Guid>.GetResponse(result, true);
        }
    }
}
