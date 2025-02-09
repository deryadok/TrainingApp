using TrainingApp.Shared.Enums;

namespace TrainingApp.Application.Dtos
{
    public class WorkoutDto
    {
        public Guid WorkoutId { get; set; }
        public string WorkoutName { get; set; }
        public int Duration { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public List<Guid> RegionIds { get; set; }
    }
}
