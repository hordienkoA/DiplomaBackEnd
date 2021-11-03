using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EFCoreConfiguration.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public List<Subject> Subjects { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }
    }

}
