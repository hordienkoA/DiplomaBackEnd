using System.Collections.Generic;

namespace Diploma.Views
{
    public class LessonView : IView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskView> Tasks { get; set; }
    }
}
