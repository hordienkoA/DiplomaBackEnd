using System.Text.Json.Serialization;

namespace EFCoreConfiguration.Models
{
    public class Task: BaseEntity<int>
    {
        public string Question { get; set; }
        public TaskType Type { get; set; }
        public string Answer { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskType
    {
        Text,
        Radio,
        Code,
        Check
    }
}
