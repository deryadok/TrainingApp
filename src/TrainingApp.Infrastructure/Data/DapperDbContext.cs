using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace TrainingApp.Infrastructure.Data
{
    public class DapperDbContext
    {
        private readonly IDbConnection _dbConnection;

        public DapperDbContext(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("TrainingApp");
            _dbConnection = new MySqlConnection(connectionString);
            if (_dbConnection?.State == ConnectionState.Closed)
                _dbConnection.Open();
        }

        public IDbConnection Connection => _dbConnection;

        public void Dispose()
        {
            if (_dbConnection?.State == ConnectionState.Open)
                _dbConnection?.Close();
        }
    }
}
