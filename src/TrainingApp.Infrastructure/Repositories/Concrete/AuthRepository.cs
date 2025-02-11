using Dapper;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;
using TrainingApp.Infrastructure.Interfaces;

namespace TrainingApp.Infrastructure.Concrete
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperDbContext _dbContext;

        public AuthRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Connection.QueryFirstOrDefaultAsync<User>(
                "GetUserByUsername", new { p_Username = username });

        }

        public async Task RegisterUserAsync(User user)
        {
            await _dbContext.Connection.ExecuteAsync(
                "INSERT INTO User (UserId, Firstname, Lastname, Username, Email, Password, CreatedAt, CreatedBy, DeleteFlag) " +
                "VALUES (@UserId, @Firstname, @Lastname, @Username, @Email, @Password, @CreatedAt, @CreatedBy, FALSE);",
                user);

        }
    }
}
