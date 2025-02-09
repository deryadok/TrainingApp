namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IWorkoutExerciseRepository
    {
        Task<Guid> AddWorkoutExerciseAsync(WorkoutExercise WorkoutExercise);
        Task<IEnumerable<WorkoutExercise>> GetAllWorkoutExercisesAsync();
        Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(Guid id);
        Task<bool> UpdateWorkoutExerciseAsync(WorkoutExercise WorkoutExercise);
        Task<bool> DeleteWorkoutExerciseAsync(Guid id);
    }
}
