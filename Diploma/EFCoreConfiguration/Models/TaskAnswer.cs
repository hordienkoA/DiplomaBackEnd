namespace EFCoreConfiguration.Models
{
    public class TaskAnswer: BaseEntity<int>
    {
        public Task Task { get; set; }
        public string AnswerText { get; set; }
        public int AnswerNumber { get; set; }
        public bool Correct { get; set; }
        public int LessonInfoId { get; set; }
        public LessonInfo LessonInfo { get; set; }
    }
}
