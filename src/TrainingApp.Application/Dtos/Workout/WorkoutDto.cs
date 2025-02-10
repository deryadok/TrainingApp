using TrainingApp.Application.Dtos.Region;
using TrainingApp.Shared.Enums;

namespace TrainingApp.Application.Dtos.Workout
{
    public class WorkoutDto
    {
        public Guid WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public int Duration { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<RegionDto> Regions { get; set; }
    }
}
