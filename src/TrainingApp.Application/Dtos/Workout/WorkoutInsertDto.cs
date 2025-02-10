using TrainingApp.Application.Dtos.Region;
using TrainingApp.Shared.Enums;

namespace TrainingApp.Application.Dtos.Workout
{
    public class WorkoutInsertDto
    {
        public string WorkoutName { get; set; }
        public int Duration { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<RegionDto> Regions { get; set; }
    }
}
