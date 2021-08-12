using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Diploma.JWT
{
    internal static class AuthOptions
    {
        public static string ISSUER;
        public static string AUDIENCE;
        public static string KEY;
        public static int Lifetime;

        
        public static SymmetricSecurityKey GetySymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
