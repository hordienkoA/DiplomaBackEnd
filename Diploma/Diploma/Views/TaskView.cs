using EFCoreConfiguration.Models;

namespace Diploma.Views
{
    public class TaskView: IView
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public TaskType Type { get; set; }
        public string AnswerText { get; set; }
        public int AnswerNumber { get; set; }
        public int LessonId { get; set; }

    }
}
