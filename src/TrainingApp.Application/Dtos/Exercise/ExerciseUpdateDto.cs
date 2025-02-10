using TrainingApp.Application.Dtos.Workout;

namespace TrainingApp.Application.Dtos.Exercise
{
    public class ExerciseUpdateDto
    {
        public Guid ExerciseId { get; set; }
        public Guid UserId { get; set; }
        public int TotalExerciseDuration { get; set; }
        public List<WorkoutDto> Workouts { get; set; }
    }
}
