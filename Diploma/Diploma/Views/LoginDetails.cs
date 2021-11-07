using System.Collections.Generic;

namespace Diploma.Views
{
    public class LoginDetails: IView
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}
