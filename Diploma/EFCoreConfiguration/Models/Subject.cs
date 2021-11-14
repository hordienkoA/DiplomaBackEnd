namespace EFCoreConfiguration.Models
{
    public class Subject: BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Course{ get; set; }
        public List<Group> Groups { get; set; }
        public List<User> Users { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
