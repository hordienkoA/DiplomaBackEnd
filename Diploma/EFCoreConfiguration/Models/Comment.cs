namespace EFCoreConfiguration.Models
{
    public class Comment : BaseEntity<int>
    {
        public User Sender{ get; set; }
        public string SenderId { get; set; }
        public User Receiver { get; set; }
        public string ReceiverId { get; set; }
        public TaskInfo TaskInfo { get; set; }
        public int TaskInfoId { get; set; }
        public string Message { get; set; }
    }
}
