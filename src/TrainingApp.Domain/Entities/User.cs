using TrainingApp.Domain.Common;

namespace TrainingApp.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}
