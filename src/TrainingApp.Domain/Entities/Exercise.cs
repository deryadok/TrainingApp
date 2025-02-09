using TrainingApp.Domain.Common;

namespace TrainingApp.Domain.Entities
{
    public class Exercise : BaseEntity
    {
        public Guid ExerciseId { get; set; }
        public Guid UserId { get; set; }
        public int TotalExerciseDuration { get; set; }

        public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; }
        public virtual User User { get; set; }
    }
}
