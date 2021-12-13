using System.Collections.Generic;
using EFCoreConfiguration.Models.Enums;

namespace EFCoreConfiguration.Models
{
    public class Lesson : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public List<LessonInfo> LessonInfos { get; set; }
        public List<Task> Tasks {get;set;}
    }
}
