using Dapper;
using System.Data;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;

namespace TrainingApp.Infrastructure.Concrete
{
    public class WorkoutRegionRepository : IWorkoutRegionRepository
    {
        private readonly DapperDbContext _dbContext;

        public WorkoutRegionRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddWorkoutRegionAsync(WorkoutRegion workoutRegion)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutRegionId", Guid.NewGuid().ToString());
                parameters.Add("@RegionId", workoutRegion.RegionId);
                parameters.Add("@WorkOutId", workoutRegion.WorkoutId);
                parameters.Add("@CreatedBy", workoutRegion.CreatedBy);
                parameters.Add("@CreatedAt", DateTime.Now);
                parameters.Add("@DeleteFlag", workoutRegion.DeleteFlag);

                return await connection.ExecuteScalarAsync<Guid>(
                    "CALL AddWorkoutRegion(@WorkoutRegionId, @RegionId, @WorkOutId, @CreatedBy, @CreatedAt, @DeleteFlag);",
                    parameters,
                    commandType: CommandType.Text
                );
            }
        }

        public async Task<bool> DeleteWorkoutRegionAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL DeleteWorkoutRegion(@WorkoutRegionId);", new { WorkoutRegionId = id });

                return affectedRows > 0;
            }
        }

        public async Task<IEnumerable<WorkoutRegion>> GetAllWorkoutRegionsAsync()
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryAsync<WorkoutRegion>("CALL GetAllWorkoutRegions();");
            }
        }

        public async Task<WorkoutRegion> GetWorkoutRegionByIdAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<WorkoutRegion>(
                    "CALL GetWorkoutRegionById(@WorkoutRegionId);", new { WorkoutRegionId = id });
            }
        }

        public async Task<bool> UpdateWorkoutRegionAsync(WorkoutRegion workoutRegion)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutRegionId", Guid.NewGuid().ToString());
                parameters.Add("@RegionId", workoutRegion.RegionId);
                parameters.Add("@WorkOutId", workoutRegion.WorkoutId);
                parameters.Add("@UpdatedBy", workoutRegion.UpdatedBy);
                parameters.Add("@UpdatedAt", DateTime.Now);

                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL UpdateWorkoutRegion(@WorkoutRegionId, @WorkoutId, @ExerciseId, @UpdatedBy, @UpdatedAt);",
                    parameters
                );

                return affectedRows > 0;
            }
        }
    }
}
