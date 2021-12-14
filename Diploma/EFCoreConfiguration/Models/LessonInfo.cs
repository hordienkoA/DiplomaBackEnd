using EFCoreConfiguration.Models.Enums;

namespace EFCoreConfiguration.Models
{
    public class LessonInfo: BaseEntity<int>
    {
        public StatusEnum Status { get; set; }
        public DateTime ValidTill { get; set; }
        public int Attemts { get; set; }
        public bool IsPassed { get; set; }
        public int Mark { get; set; }
        public string UserId { get; set; }
        public User User;
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
