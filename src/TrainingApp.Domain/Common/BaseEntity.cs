using TrainingApp.Domain.Entities;

namespace TrainingApp.Domain.Common
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool DeleteFlag { get; set; } = false;
        public virtual User CreatedUser { get; set; }
        public virtual User UpdatedUser { get; set; }

    }
}
