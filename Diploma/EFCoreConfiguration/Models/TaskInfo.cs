namespace EFCoreConfiguration.Models
{
    public class TaskInfo: BaseEntity<int>
    {
        public string Answer { get; set; }
        public DateTime ValidTill { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
