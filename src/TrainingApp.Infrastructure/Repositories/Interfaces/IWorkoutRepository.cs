using TrainingApp.Domain.Entities;
using TrainingApp.Shared.Enums;

namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IWorkoutRepository
    {
        Task<Guid> AddWorkoutAsync(Workout workout);
        Task<IEnumerable<Workout>> GetAllWorkoutsAsync();
        Task<Workout> GetWorkoutByIdAsync(Guid id);
        Task<bool> UpdateWorkoutAsync(Workout workout);
        Task<bool> DeleteWorkoutAsync(Guid id);
        Task<IEnumerable<Workout>> GetFilteredWorkoutsAsync(int? duration, DifficultyLevel difficulty, Guid regionId);
    }
}
