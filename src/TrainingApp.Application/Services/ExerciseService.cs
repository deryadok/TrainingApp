using AutoMapper;
using TrainingApp.Application.Dtos.Region;
using TrainingApp.Application.Dtos.Exercise;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Constants;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.Response;
using TrainingApp.Application.Dtos.Workout;

namespace TrainingApp.Application.Services
{
    public class ExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IWorkoutExerciseRepository _workoutExerciseRepository;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository,
            IWorkoutExerciseRepository workoutExerciseRepository,
            IMapper mapper)
        {
            _exerciseRepository = exerciseRepository;
            _workoutExerciseRepository = workoutExerciseRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Guid>> Add(ExerciseInsertDto exerciseInsertDto)
        {
            var exercise = _mapper.Map<Exercise>(exerciseInsertDto);

            var result = await _exerciseRepository.AddExerciseAsync(exercise);

            var exerciseWorkouts = _mapper.Map<List<WorkoutExercise>>(exercise.WorkoutExercises);

            _ = await _workoutExerciseRepository.BulkInsertWorkoutExercisesAsync(exerciseWorkouts);

            var success = !result.Equals(Guid.Empty);

            return ResponseHelper<Guid>.GetResponse(result, success, ResultMessages.InsertMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> Update(ExerciseUpdateDto exerciseUpdateDto)
        {
            var exercise = _mapper.Map<Exercise>(exerciseUpdateDto);

            var result = await _exerciseRepository.UpdateExerciseAsync(exercise);

            var exerciseWorkouts = _mapper.Map<List<WorkoutExercise>>(exercise.WorkoutExercises);

            _ = await _workoutExerciseRepository.BulkUpdateWorkoutExercisesAsync(exercise.ExerciseId, exerciseWorkouts);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.UpdatedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> Delete(Guid exerciseId)
        {
            var result = await _exerciseRepository.DeleteExerciseAsync(exerciseId);

            _ = await _workoutExerciseRepository.BulkSoftDeleteWorkoutExercisesAsync(exerciseId);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.DeletedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<ExerciseDto>> GetExerciseById(Guid id)
        {
            var returnModel = new ExerciseDto();

            var exercise = await _exerciseRepository.GetExerciseByIdAsync(id);

            var workouts = await _workoutExerciseRepository.GetWorkoutExerciseByIdAsync(id);

            returnModel = _mapper.Map<ExerciseDto>(exercise);
            returnModel.Workouts = _mapper.Map<List<WorkoutDto>>(workouts);

            bool success = exercise != null;

            return ResponseHelper<ExerciseDto>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<IEnumerable<ExerciseDto>>> GetAllExercises()
        {
            var Exercises = await _exerciseRepository.GetAllExercisesAsync();

            var returnModel = _mapper.Map<IEnumerable<ExerciseDto>>(Exercises);

            bool success = returnModel != null && returnModel.Any();

            return ResponseHelper<IEnumerable<ExerciseDto>>.GetResponse(returnModel, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }
    }
}
