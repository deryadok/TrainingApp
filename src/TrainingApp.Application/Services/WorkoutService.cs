using AutoMapper;
using TrainingApp.Application.Dtos.Region;
using TrainingApp.Application.Dtos.Workout;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Constants;
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

        public async Task<ApiResponse<Guid>> Add(WorkoutInsertDto workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);

            var result = await _workoutRepository.AddWorkoutAsync(workout);

            var workoutRegions = _mapper.Map<List<WorkoutRegion>>(workoutDto.Regions);

            _ = await _workoutRegionRepository.BulkInsertWorkoutRegionsAsync(workoutRegions);

            var success = !result.Equals(Guid.Empty);

            return ResponseHelper<Guid>.GetResponse(result, success, ResultMessages.InsertMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> Update(WorkoutUpdateDto workoutDto)
        {
            var workout = _mapper.Map<Workout>(workoutDto);

            var result = await _workoutRepository.UpdateWorkoutAsync(workout);

            var workoutRegions = _mapper.Map<List<WorkoutRegion>>(workoutDto.Regions);

            _ = await _workoutRegionRepository.BulkUpdateWorkoutRegionsAsync(workout.WorkoutId, workoutRegions);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.UpdatedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> Delete(Guid workoutId)
        {
            var result = await _workoutRepository.DeleteWorkoutAsync(workoutId);

            _ = await _workoutRegionRepository.BulkSoftDeleteWorkoutRegionsAsync(workoutId);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.DeletedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<WorkoutDto>> GetWorkoutById(Guid id)
        {
            var returnModel = new WorkoutDto();

            var workout = await _workoutRepository.GetWorkoutByIdAsync(id);

            var regions = await _workoutRegionRepository.GetWorkoutRegionByIdAsync(id);

            returnModel = _mapper.Map<WorkoutDto>(workout);
            returnModel.Regions = _mapper.Map<List<RegionDto>>(regions);

            bool success = workout != null;

            return ResponseHelper<WorkoutDto>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<IEnumerable<WorkoutDto>>> GetAllWorkouts()
        {
            var workouts = await _workoutRepository.GetAllWorkoutsAsync();

            var returnModel = _mapper.Map<IEnumerable<WorkoutDto>>(workouts);

            bool success = returnModel != null && returnModel.Any();

            return ResponseHelper<IEnumerable<WorkoutDto>>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<IEnumerable<WorkoutDto>>> GetFilteredResult(WorkoutFilterDto workoutFilter)
        {
            var workouts = await _workoutRepository.GetFilteredWorkoutsAsync(workoutFilter.Duration, workoutFilter.DifficultyLevel, workoutFilter.Region.RegionId);

            var returnModel = _mapper.Map<IEnumerable<WorkoutDto>>(workouts);

            bool success = returnModel.Any();

            return ResponseHelper<IEnumerable<WorkoutDto>>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }
    }
}
