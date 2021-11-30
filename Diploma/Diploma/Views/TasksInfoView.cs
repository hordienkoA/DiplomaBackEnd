using EFCoreConfiguration.Models;

namespace Diploma.Views
{
    public class TasksInfoView: IView
    {
        public int TaskId { get; set; }
        public int TaskInfoId { get; set; }
        public string Question { get; set; }
        public TaskType Type { get; set; }
        public string Answer { get; set; }
        public DateTime ValidTill { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
