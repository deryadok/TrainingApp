using Dapper;
using System.Data;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;

namespace TrainingApp.Infrastructure.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _dbContext;

        public UserRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddUserAsync(User user)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_UserId", Guid.NewGuid().ToString());
                parameters.Add("p_Firstname", user.Firstname);
                parameters.Add("p_Lastname", user.Lastname);
                parameters.Add("p_Username", user.Username);
                parameters.Add("p_Email", user.Email);
                parameters.Add("p_Password", user.Password);
                parameters.Add("p_CreatedAt", DateTime.Now);
                parameters.Add("p_DeleteFlag", user.DeleteFlag);

                return await connection.ExecuteScalarAsync<Guid>(
                    "AddUser;",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                int affectedRows = await connection.ExecuteAsync(
                    "CALL DeleteUser(@UserId);", new { UserId = id });

                return affectedRows > 0;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryAsync<User>("CALL GetAllUsers();");
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "CALL GetUserById(@UserId);", new { UserId = id });
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", Guid.NewGuid().ToString());
                parameters.Add("@Firstname", user.Firstname);
                parameters.Add("@Lastname", user.Lastname);
                parameters.Add("@Username", user.Username);
                parameters.Add("@Email", user.Email);
                parameters.Add("@UpdatedBy", user.UpdatedBy);
                parameters.Add("@UpdatedAt", DateTime.Now);

                int affectedRows = await connection.ExecuteAsync(
                    "CALL UpdateUser(@UserId, @Firstname, @Lastname, @Username, @Email, @UpdatedBy, @UpdatedAt);",
                    parameters
                );

                return affectedRows > 0;
            }
        }
    }
}
