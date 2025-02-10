using TrainingApp.Application.Dtos.Workout;

namespace TrainingApp.Application.Dtos.Exercise
{
    public class ExerciseInsertDto
    {
        public Guid UserId { get; set; }
        public int TotalExerciseDuration { get; set; }
        public List<WorkoutDto> Workouts { get; set; }
    }
}
