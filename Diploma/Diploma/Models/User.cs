using Microsoft.AspNetCore.Identity;

namespace Diploma.Models
{
    public class User: IdentityUser
    {
        public Sex Sex { get; set; }
    }

    public enum Sex
    {
        Male,
        Female,
        Other
    }
}
