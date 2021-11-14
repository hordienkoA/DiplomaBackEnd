using EFCoreConfiguration.Models;

namespace Diploma.JWT
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(User user);
    }
}
