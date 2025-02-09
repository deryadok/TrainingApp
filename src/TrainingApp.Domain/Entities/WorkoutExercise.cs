using TrainingApp.Domain.Common;
using TrainingApp.Domain.Entities;

public class WorkoutExercise : BaseEntity
{ 
    public Guid WorkoutExerciseId { get; set; }
    public Guid WorkoutId { get; set; }
    public Guid ExerciseId { get; set; }

    public virtual Workout Workout { get; set; }
    public virtual Exercise Exercise { get; set; }
}
