namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IWorkoutExerciseRepository
    {
        Task<Guid> AddWorkoutExerciseAsync(WorkoutExercise WorkoutExercise);
        Task<IEnumerable<WorkoutExercise>> GetAllWorkoutExercisesAsync();
        Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(Guid id);
        Task<bool> UpdateWorkoutExerciseAsync(WorkoutExercise WorkoutExercise);
        Task<bool> DeleteWorkoutExerciseAsync(Guid id);
        Task<bool> BulkInsertWorkoutExercisesAsync(List<WorkoutExercise> workoutExercises);
        Task<bool> BulkUpdateWorkoutExercisesAsync(Guid workoutId, List<WorkoutExercise> workoutExercises);
        Task<bool> BulkSoftDeleteWorkoutExercisesAsync(Guid workoutId);
    }
}
