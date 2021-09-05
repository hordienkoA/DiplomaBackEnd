using Microsoft.AspNetCore.Identity;

namespace EFCoreConfiguration.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
    }

}
