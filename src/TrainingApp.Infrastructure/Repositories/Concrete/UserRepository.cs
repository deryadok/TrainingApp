using Dapper;
using System.Data;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;
using TrainingApp.Infrastructure.Interfaces;

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
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", Guid.NewGuid().ToString());
            parameters.Add("@Firstname", user.Firstname);
            parameters.Add("@Lastname", user.Lastname);
            parameters.Add("@Username", user.Username);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            parameters.Add("@CreatedBy", user.CreatedBy);
            parameters.Add("@CreatedAt", DateTime.Now);
            parameters.Add("@DeleteFlag", user.DeleteFlag);

            return await _dbContext.Connection.ExecuteScalarAsync<Guid>(
                "CALL AddUser(@UserId, @Firstname, @Lastname, @Username, @Email, @Password, @CreatedBy, @CreatedAt, @DeleteFlag);",
                parameters,
                commandType: CommandType.Text
            );
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL DeleteUser(@UserId);", new { UserId = id });

            return affectedRows > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _dbContext.Connection.QueryAsync<User>("CALL GetAllUsers();");
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(
                "CALL GetUserById(@UserId);", new { UserId = id });
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", user.UserId);
            parameters.Add("@Firstname", user.Firstname);
            parameters.Add("@Lastname", user.Lastname);
            parameters.Add("@Username", user.Username);
            parameters.Add("@Email", user.Email);
            parameters.Add("@UpdatedBy", user.UpdatedBy);
            parameters.Add("@UpdatedAt", DateTime.Now);

            int affectedRows = await _dbContext.Connection.ExecuteScalarAsync<int>(
                "CALL UpdateUser(@UserId, @Firstname, @Lastname, @Username, @Email, @UpdatedBy, @UpdatedAt);",
                parameters
            );

            return affectedRows > 0;
        }
    }
}
