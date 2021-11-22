
namespace Diploma.Views
{
    public class UserView: IView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public  int Age { get; set; }
        public int? GroupId { get; set; } 
    }
}
