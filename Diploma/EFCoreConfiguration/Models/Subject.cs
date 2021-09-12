using System.Collections.Generic;

namespace EFCoreConfiguration.Models
{
    public class Subject
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public int CourseNumber { get; set; }
        public List<Group> Groups { get; set; }
        public List<User> Teachers { get; set; }
        public List<Work> Works { get; set; }
    }
}
