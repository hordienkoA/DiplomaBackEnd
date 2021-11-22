using EFCoreConfiguration.Models;

namespace Diploma.Views
{
    public class TaskView: IView
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public TaskType Type { get; set; }
        public string Answer { get; set; }
        public int LessonId { get; set; }

    }
}
