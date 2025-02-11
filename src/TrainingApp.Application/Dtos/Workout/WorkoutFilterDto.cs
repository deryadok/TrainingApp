using TrainingApp.Application.Dtos.Region;
using TrainingApp.Shared.Enums;

namespace TrainingApp.Application.Dtos.Workout
{
    public class WorkoutFilterDto
    {
        public int Duration { get; set; }

        public DifficultyLevel DifficultyLevel { get; set; }

        public RegionDto Region { get; set; }
    }
}
