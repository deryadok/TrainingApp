using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace TrainingApp.Infrastructure.Data
{
    public class DapperDbContext : IDisposable
    {
        private readonly IDbConnection _dbConnection;

        public DapperDbContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("TrainingApp");
            _dbConnection = new MySqlConnection(connectionString);
        }

        public IDbConnection Connection => _dbConnection;

        public void Dispose()
        {
            _dbConnection?.Dispose();
        }
    }
}
