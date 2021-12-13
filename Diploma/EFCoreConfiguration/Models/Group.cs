using System.Collections.Generic;

namespace EFCoreConfiguration.Models
{
    public class Group: BaseEntity<int>
    {
        public string Name { get; set; }
        public int CourseNumber { get; set; }
        public List<User> Users { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
