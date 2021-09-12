using System.Collections.Generic;

namespace EFCoreConfiguration.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseNumber { get; set; }
        public List<User> Students { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
