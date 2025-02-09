using TrainingApp.Domain.Common;
using TrainingApp.Shared.Enums;


namespace TrainingApp.Domain.Entities
{
    public class Workout : BaseEntity
    {
        public Guid WorkoutId { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public virtual ICollection<WorkoutRegion> WorkoutRegions { get; set; }
        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
