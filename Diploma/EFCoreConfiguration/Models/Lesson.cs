using EFCoreConfiguration.Models.Enums;

namespace EFCoreConfiguration.Models
{
    public class Lesson : BaseEntity<int>
    {
        public string Name { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime ValidTill { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
