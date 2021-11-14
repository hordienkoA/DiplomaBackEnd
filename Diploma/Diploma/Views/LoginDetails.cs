namespace Diploma.Views
{
    public class LoginDetails: IView
    {
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}
