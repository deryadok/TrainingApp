using Dapper;
using System.Data;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;
using TrainingApp.Infrastructure.Interfaces;

namespace TrainingApp.Infrastructure.Concrete
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DapperDbContext _dbContext;

        public ExerciseRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddExerciseAsync(Exercise exercise)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ExerciseId", Guid.NewGuid().ToString());
            parameters.Add("@UserId", exercise.ExerciseId);
            parameters.Add("@TotalExerciseDuration", exercise.TotalExerciseDuration);
            parameters.Add("@CreatedBy", exercise.CreatedBy);
            parameters.Add("@CreatedAt", DateTime.Now);
            parameters.Add("@DeleteFlag", exercise.DeleteFlag);

            return await _dbContext.Connection.ExecuteScalarAsync<Guid>(
                "CALL AddExercise(@ExerciseId, @UserId, @TotalExerciseDuration, @CreatedBy, @CreatedAt, @DeleteFlag);",
                parameters,
                commandType: CommandType.Text
            );
        }

        public async Task<bool> DeleteExerciseAsync(Guid id)
        {
            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL DeleteExercise(@ExerciseId);", new { ExerciseId = id });

            return affectedRows > 0;
        }

        public async Task<IEnumerable<Exercise>> GetAllExercisesAsync()
        {
            return await _dbContext.Connection.QueryAsync<Exercise>("CALL GetAllExercises();");
        }

        public async Task<Exercise> GetExerciseByIdAsync(Guid id)
        {
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<Exercise>(
                "CALL GetExerciseById(@ExerciseId);", new { ExerciseId = id });
        }

        public async Task<bool> UpdateExerciseAsync(Exercise exercise)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ExerciseId", Guid.NewGuid().ToString());
            parameters.Add("@UserId", exercise.ExerciseId);
            parameters.Add("@TotalExerciseDuration", exercise.TotalExerciseDuration);
            parameters.Add("@UpdatedBy", exercise.UpdatedBy);
            parameters.Add("@UpdatedAt", DateTime.Now);

            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL UpdateExercise(@ExerciseId, @UserId, @TotalExerciseDuration, @UpdatedBy, @UpdatedAt);",
                parameters
            );

            return affectedRows > 0;

        }
    }
}
