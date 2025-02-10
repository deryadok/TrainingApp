using AutoMapper;
using TrainingApp.Application.Dtos.Workout;
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

        public async Task<ApiResponse<Guid>> AddWorkout(WorkoutInsertDto workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);

            var result = await _workoutRepository.AddWorkoutAsync(workout);

            var workoutRegions = _mapper.Map<List<WorkoutRegion>>(workoutDto.Regions);

            _ = await _workoutRegionRepository.BulkInsertWorkoutRegionsAsync(workoutRegions);

            return ResponseHelper<Guid>.GetResponse(result, true);
        }

        public async Task<ApiResponse<bool>> Update(WorkoutUpdateDto workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);

            var result = await _workoutRepository.UpdateWorkoutAsync(workout);

            var workoutRegions = _mapper.Map<List<WorkoutRegion>>(workoutDto.Regions);

            _ = await _workoutRegionRepository.BulkUpdateWorkoutRegionsAsync(workout.WorkoutId, workoutRegions);

            return ResponseHelper<bool>.GetResponse(result, true);
        }

        public async Task<ApiResponse<bool>> Delete(Guid workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);

            var result = await _workoutRepository.UpdateWorkoutAsync(workout);

            _ = await _workoutRegionRepository.BulkSoftDeleteWorkoutRegionsAsync(workout.WorkoutId);

            return ResponseHelper<bool>.GetResponse(result, true);
        }
    }
}
