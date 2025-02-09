using TrainingApp.Domain.Entities;

namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IExerciseRepository
    {
        Task<Guid> AddExerciseAsync(Exercise exercise);
        Task<IEnumerable<Exercise>> GetAllExercisesAsync();
        Task<Exercise> GetExerciseByIdAsync(Guid id);
        Task<bool> UpdateExerciseAsync(Exercise exercise);
        Task<bool> DeleteExerciseAsync(Guid id);
    }
}
