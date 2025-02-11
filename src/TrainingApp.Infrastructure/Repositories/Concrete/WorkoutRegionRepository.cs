using Dapper;
using System.Data;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;
using System.Text;
using MySql.Data.MySqlClient;

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
            var parameters = new DynamicParameters();
            parameters.Add("@WorkoutRegionId", Guid.NewGuid().ToString());
            parameters.Add("@RegionId", workoutRegion.RegionId);
            parameters.Add("@WorkOutId", workoutRegion.WorkoutId);
            parameters.Add("@CreatedBy", workoutRegion.CreatedBy);
            parameters.Add("@CreatedAt", DateTime.Now);
            parameters.Add("@DeleteFlag", workoutRegion.DeleteFlag);

            return await _dbContext.Connection.ExecuteScalarAsync<Guid>(
                "CALL AddWorkoutRegion(@WorkoutRegionId, @RegionId, @WorkOutId, @CreatedBy, @CreatedAt, @DeleteFlag);",
                parameters,
                commandType: CommandType.Text
            );
        }

        public async Task<bool> BulkInsertWorkoutRegionsAsync(List<WorkoutRegion> workoutRegions)
        {
            var valuesBuilder = new StringBuilder();

            foreach (var workoutRegion in workoutRegions)
            {
                valuesBuilder.AppendFormat("('{0}', '{1}', '{2}', '{3}', NOW(), FALSE),",
                workoutRegion.WorkoutRegionId, workoutRegion.WorkoutId, workoutRegion.RegionId, workoutRegion.CreatedBy);
            }

            string values = valuesBuilder.ToString().TrimEnd(',');

            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL BulkInsertWorkoutRegions(@Values);", new { Values = values });

            return affectedRows > 0;
        }

        public async Task<bool> BulkUpdateWorkoutRegionsAsync(Guid workoutId, List<WorkoutRegion> workoutRegions)
        {
            var updateBuilder = new StringBuilder();
            foreach (var workoutRegion in workoutRegions)
            {
                updateBuilder.AppendFormat("WHEN '{0}' THEN '{1}' ", workoutRegion.WorkoutRegionId, workoutRegion.RegionId);
            }

            string updateValues = updateBuilder.ToString();

            var parameters = new DynamicParameters();
            parameters.Add("@WorkoutId", workoutId);
            parameters.Add("@Updates", updateValues);

            int affectedRows = await _dbContext.Connection.ExecuteAsync("CALL BulkUpdateWorkoutRegions(@WorkoutId, @Updates);", parameters);

            return affectedRows > 0;
        }

        public async Task<bool> BulkSoftDeleteWorkoutRegionsAsync(Guid workoutId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WorkoutId", workoutId);

            int affectedRows = await _dbContext.Connection.ExecuteAsync("CALL BulkSoftDeleteWorkoutRegions(@WorkoutId);", parameters);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteWorkoutRegionAsync(Guid id)
        {
            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL DeleteWorkoutRegion(@WorkoutRegionId);", new { WorkoutRegionId = id });

            return affectedRows > 0;
        }

        public async Task<IEnumerable<WorkoutRegion>> GetAllWorkoutRegionsAsync()
        {
            return await _dbContext.Connection.QueryAsync<WorkoutRegion>("CALL GetAllWorkoutRegions();");
        }

        public async Task<WorkoutRegion> GetWorkoutRegionByIdAsync(Guid id)
        {
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<WorkoutRegion>(
                "CALL GetWorkoutRegionById(@WorkoutRegionId);", new { WorkoutRegionId = id });
        }

        public async Task<bool> UpdateWorkoutRegionAsync(WorkoutRegion workoutRegion)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@WorkoutRegionId", Guid.NewGuid().ToString());
            parameters.Add("@RegionId", workoutRegion.RegionId);
            parameters.Add("@WorkOutId", workoutRegion.WorkoutId);
            parameters.Add("@UpdatedBy", workoutRegion.UpdatedBy);
            parameters.Add("@UpdatedAt", DateTime.Now);

            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL UpdateWorkoutRegion(@WorkoutRegionId, @WorkoutId, @ExerciseId, @UpdatedBy, @UpdatedAt);",
                parameters
            );

            return affectedRows > 0;
        }
    }
}
