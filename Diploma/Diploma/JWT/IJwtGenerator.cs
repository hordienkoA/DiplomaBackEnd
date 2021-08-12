using Diploma.Models;

namespace Diploma.JWT
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
