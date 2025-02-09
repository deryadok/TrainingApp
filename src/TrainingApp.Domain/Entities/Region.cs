using TrainingApp.Domain.Common;

namespace TrainingApp.Domain.Entities
{
    public class Region : BaseEntity
    {
        public Guid RegionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<WorkoutRegion> WorkoutRegions { get; set; }
    }
}
