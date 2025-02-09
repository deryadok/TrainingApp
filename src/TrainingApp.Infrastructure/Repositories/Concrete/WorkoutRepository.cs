using Dapper;
using System.Data;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;
using TrainingApp.Shared.Enums;

namespace TrainingApp.Infrastructure.Concrete
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly DapperDbContext _dbContext;

        public WorkoutRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddWorkoutAsync(Workout workout)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutId", Guid.NewGuid().ToString());
                parameters.Add("@Name", workout.Name);
                parameters.Add("@Duration", workout.Duration);
                parameters.Add("@Difficulty", workout.Difficulty);
                parameters.Add("@CreatedBy", workout.CreatedBy);
                parameters.Add("@CreatedAt", DateTime.Now);
                parameters.Add("@DeleteFlag", workout.DeleteFlag);

                return await connection.ExecuteScalarAsync<Guid>(
                    "CALL AddWorkout(@WorkoutId, @Name, @Duration, @Difficulty, @CreatedBy, @CreatedAt, @DeleteFlag);",
                    parameters,
                    commandType: CommandType.Text
                );
            }
        }

        public async Task<IEnumerable<Workout>> GetAllWorkoutsAsync()
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryAsync<Workout>("CALL GetAllWorkouts();");
            }
        }

        public async Task<Workout> GetWorkoutByIdAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<Workout>(
                    "CALL GetWorkoutById(@WorkoutId);", new { WorkoutId = id });
            }
        }

        public async Task<bool> UpdateWorkoutAsync(Workout workout)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutId", workout.WorkoutId);
                parameters.Add("@Name", workout.Name);
                parameters.Add("@Duration", workout.Duration);
                parameters.Add("@Difficulty", workout.Difficulty);
                parameters.Add("@UpdatedBy", workout.UpdatedBy);
                parameters.Add("@UpdatedAt", DateTime.Now);

                int affectedRows = await connection.ExecuteAsync(
                    "CALL UpdateWorkout(@WorkoutId, @Name, @Duration, @Difficulty, @UpdatedBy, @UpdatedAt);",
                    parameters
                );

                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteWorkoutAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                int affectedRows = await connection.ExecuteAsync(
                    "CALL DeleteWorkout(@WorkoutId);", new { WorkoutId = id });

                return affectedRows > 0;
            }
        }

        public async Task<IEnumerable<Workout>> GetFilteredWorkoutsAsync(int? duration, DifficultyLevel difficulty, Guid regionId)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Duration", duration);
                parameters.Add("@Difficulty", difficulty);
                parameters.Add("@RegionId", regionId);

                return await connection.QueryAsync<Workout>(
                    "CALL GetFilteredWorkouts(@Duration, @Difficulty, @RegionId);",
                    parameters
                );
            }
        }
    }
}
