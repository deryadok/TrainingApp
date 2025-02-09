using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;

namespace TrainingApp.Infrastructure.Data
{
    public class DatabaseInitializer
    {
        private readonly DapperDbContext _dbContext;
        private readonly ILogger<DatabaseInitializer> _logger;
        private const int BatchSize = 1000; // Her seferinde 1000 kayıt ekle

        public DatabaseInitializer(DapperDbContext dbContext, ILogger<DatabaseInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SeedDataAsync()
        {
            try
            {
                using (var connection = _dbContext.Connection)
                {

                    var workoutCount = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Workouts;");
                    if (workoutCount > 0)
                    {
                        _logger.LogInformation("Seed verisi eklenmedi, zaten veri mevcut.");
                        return;
                    }

                    _logger.LogInformation("Bulk Insert ile büyük veri ekleniyor...");

                    // 10.000 antrenman ekleyelim (örnek)
                    int totalWorkouts = 10000;
                    for (int i = 0; i < totalWorkouts; i += BatchSize)
                    {
                        StringBuilder workoutSql = new StringBuilder("INSERT INTO Workouts (Name, Duration, Difficulty, Region, CreatedBy) VALUES ");
                        for (int j = 0; j < BatchSize && (i + j) < totalWorkouts; j++)
                        {
                            workoutSql.Append($"('Workout-{i + j}', {new Random().Next(5, 60)}, 'Medium', 'Full Body', 'System'),");
                        }
                        workoutSql.Length--; // Son virgülü kaldır
                        await connection.ExecuteAsync(workoutSql.ToString());
                    }

                    _logger.LogInformation("Workouts tablosuna büyük veri başarıyla eklendi.");

                    // 1.000.000 hareket ekleyelim
                    int totalExercises = 1000000;
                    for (int i = 0; i < totalExercises; i += BatchSize)
                    {
                        StringBuilder exerciseSql = new StringBuilder("INSERT INTO Exercises (WorkoutId, Name, Description, CreatedBy) VALUES ");
                        for (int j = 0; j < BatchSize && (i + j) < totalExercises; j++)
                        {
                            exerciseSql.Append($"({new Random().Next(1, totalWorkouts)}, 'Exercise-{i + j}', 'Description-{i + j}', 'System'),");
                        }
                        exerciseSql.Length--; // Son virgülü kaldır
                        await connection.ExecuteAsync(exerciseSql.ToString());
                    }

                    _logger.LogInformation("Exercises tablosuna büyük veri başarıyla eklendi.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Seed verisi eklenirken hata oluştu: {Message}", ex.Message);
                throw;
            }
        }
    }
}