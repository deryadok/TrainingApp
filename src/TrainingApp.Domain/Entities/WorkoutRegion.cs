using TrainingApp.Domain.Common;

namespace TrainingApp.Domain.Entities
{
    public class WorkoutRegion : BaseEntity
    {
        public Guid WorkoutRegionId { get; set; }
        public Guid RegionId { get; set; }
        public Guid WorkoutId { get; set; }

        public virtual Region Region { get; set; }
        public virtual Workout Workout { get; set; }
    }
}
