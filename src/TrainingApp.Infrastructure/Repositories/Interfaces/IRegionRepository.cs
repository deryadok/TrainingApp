using TrainingApp.Domain.Entities;

namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IRegionRepository
    {
        Task<Guid> AddRegionAsync(Region region);
        Task<IEnumerable<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(Guid id);
        Task<bool> UpdateRegionAsync(Region region);
        Task<bool> DeleteRegionAsync(Guid id);
    }
}
