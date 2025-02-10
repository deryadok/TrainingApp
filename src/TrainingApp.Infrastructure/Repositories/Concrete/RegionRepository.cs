using Dapper;
using System.Data;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Data;

namespace TrainingApp.Infrastructure.Concrete
{
    public class RegionRepository : IRegionRepository
    {
        private readonly DapperDbContext _dbContext;

        public RegionRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddRegionAsync(Region region)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RegionId", Guid.NewGuid().ToString());
                parameters.Add("@Name", region.Name);
                parameters.Add("@Description", region.Description);
                parameters.Add("@CreatedBy", region.CreatedBy);
                parameters.Add("@CreatedAt", DateTime.Now);
                parameters.Add("@DeleteFlag", region.DeleteFlag);

                return await connection.ExecuteScalarAsync<Guid>(
                    "CALL AddRegion(@RegionId, @Name, @Description, @CreatedBy, @CreatedAt, @DeleteFlag);",
                    parameters,
                    commandType: CommandType.Text
                );
            }
        }

        public async Task<bool> DeleteRegionAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL DeleteRegion(@RegionId);", new { RegionId = id });

                return affectedRows > 0;
            }
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryAsync<Region>("CALL GetAllRegions();");
            }
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            using (var connection = _dbContext.Connection)
            {
                return await connection.QueryFirstOrDefaultAsync<Region>(
                    "CALL GetRegionById(@RegionId);", new { RegionId = id });
            }
        }

        public async Task<bool> UpdateRegionAsync(Region region)
        {
            using (var connection = _dbContext.Connection)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RegionId", Guid.NewGuid().ToString());
                parameters.Add("@Name", region.Name);
                parameters.Add("@Description", region.Description);
                parameters.Add("@UpdatedBy", region.UpdatedBy);
                parameters.Add("@UpdatedAt", DateTime.Now);

                int affectedRows = await connection.ExecuteScalarAsync<int>(
                    "CALL UpdateRegion(@RegionId, @Name, @Description, @UpdatedBy, @UpdatedAt);",
                    parameters
                );

                return affectedRows > 0;
            }
        }
    }
}
