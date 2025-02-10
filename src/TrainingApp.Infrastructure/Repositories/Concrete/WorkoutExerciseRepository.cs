using Dapper;
using System.Data;
using System.Text;
using TrainingApp.Infrastructure.Data;
using TrainingApp.Infrastructure.Interfaces;

namespace TrainingApp.Infrastructure.Concrete
{
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        private readonly DapperDbContext _dbContext;

        public WorkoutExerciseRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddWorkoutExerciseAsync(WorkoutExercise workoutExercise)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutExerciseId", Guid.NewGuid().ToString());
                parameters.Add("@WorkoutId", workoutExercise.WorkoutId);
                parameters.Add("@ExerciseId", workoutExercise.ExerciseId);
                parameters.Add("@CreatedBy", workoutExercise.CreatedBy);
                parameters.Add("@CreatedAt", DateTime.Now);
                parameters.Add("@DeleteFlag", workoutExercise.DeleteFlag);

                return await connection.ExecuteScalarAsync<Guid>(
                    "CALL AddWorkoutExercise(@WorkoutExerciseId, @WorkoutId, @ExerciseId, @CreatedBy, @CreatedAt, @DeleteFlag);",
                    parameters,
                    commandType: CommandType.Text
                );
            }
        }

        public async Task<bool> BulkInsertWorkoutExercisesAsync(List<WorkoutExercise> workoutExercises)
        {
            using (var connection = _dbContext.Connection)
            {
                var valuesBuilder = new StringBuilder();

                foreach (var workoutExercise in workoutExercises)
                {
                    valuesBuilder.AppendFormat("('{0}', '{1}', '{2}', '{3}', NOW(), FALSE),",
                    workoutExercise.WorkoutExerciseId, workoutExercise.WorkoutId, workoutExercise.ExerciseId, workoutExercise.CreatedBy);
                }

                string values = valuesBuilder.ToString().TrimEnd(',');

                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL BulkInsertWorkoutRegions(@Values);", new { Values = values });

                return affectedRows > 0;
            }
        }

        public async Task<bool> BulkUpdateWorkoutExercisesAsync(Guid workoutId, List<WorkoutExercise> workoutExercises)
        {
            using (var connection = _dbContext.Connection)
            {
                var updateBuilder = new StringBuilder();
                foreach (var workoutExercise in workoutExercises)
                {
                    updateBuilder.AppendFormat("WHEN '{0}' THEN '{1}' ", workoutExercise.WorkoutExerciseId, workoutExercise.ExerciseId);
                }

                string updateValues = updateBuilder.ToString();

                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutId", workoutId);
                parameters.Add("@Updates", updateValues);

                int affectedRows = await connection.ExecuteAsync("CALL BulkUpdateWorkoutExercises(@WorkoutId, @Updates);", parameters);
                return affectedRows > 0;
            }
        }

        public async Task<bool> BulkSoftDeleteWorkoutExercisesAsync(Guid workoutId)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutId", workoutId);

                int affectedRows = await connection.ExecuteAsync("CALL BulkSoftDeleteWorkoutExercises(@WorkoutId);", parameters);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteWorkoutExerciseAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL DeleteWorkoutExercise(@WorkoutExerciseId);", new { WorkoutExerciseId = id });

                return affectedRows > 0;
            }
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllWorkoutExercisesAsync()
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryAsync<WorkoutExercise>("CALL GetAllWorkoutExercises();");
            }
        }

        public async Task<WorkoutExercise> GetWorkoutExerciseByIdAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<WorkoutExercise>(
                    "CALL GetWorkoutExerciseById(@WorkoutExerciseId);", new { WorkoutExerciseId = id });
            }
        }

        public async Task<bool> UpdateWorkoutExerciseAsync(WorkoutExercise workoutExercise)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@WorkoutExerciseId", Guid.NewGuid().ToString());
                parameters.Add("@WorkoutId", workoutExercise.WorkoutId);
                parameters.Add("@ExerciseId", workoutExercise.ExerciseId);
                parameters.Add("@UpdatedBy", workoutExercise.UpdatedBy);
                parameters.Add("@UpdatedAt", DateTime.Now);

                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL UpdateWorkoutExercise(@WorkoutExerciseId, @WorkoutId, @ExerciseId, @UpdatedBy, @UpdatedAt);",
                    parameters
                );

                return affectedRows > 0;
            }
        }
    }
}
