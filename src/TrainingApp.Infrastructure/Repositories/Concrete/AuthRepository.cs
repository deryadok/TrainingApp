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
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "GetUserByUsername", new { p_Username = username });
            }
        }
    }
}
