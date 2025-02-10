using TrainingApp.Domain.Entities;

namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IWorkoutRegionRepository
    {
        Task<Guid> AddWorkoutRegionAsync(WorkoutRegion WorkoutRegion);
        Task<IEnumerable<WorkoutRegion>> GetAllWorkoutRegionsAsync();
        Task<WorkoutRegion> GetWorkoutRegionByIdAsync(Guid id);
        Task<bool> UpdateWorkoutRegionAsync(WorkoutRegion WorkoutRegion);
        Task<bool> DeleteWorkoutRegionAsync(Guid id);
        Task<bool> BulkInsertWorkoutRegionsAsync(List<WorkoutRegion> workoutRegions);
        Task<bool> BulkUpdateWorkoutRegionsAsync(Guid workoutId, List<WorkoutRegion> workoutRegions);
        Task<bool> BulkSoftDeleteWorkoutRegionsAsync(Guid workoutId);
    }
}
