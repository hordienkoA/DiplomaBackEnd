using System.Text.Json.Serialization;

namespace EFCoreConfiguration.Models
{
    public class SubjectInfo : BaseEntity<int>
    {
        public RelationType RelationType { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RelationType
    {
        Owner,
        Teacher,
        Student
    }
}
