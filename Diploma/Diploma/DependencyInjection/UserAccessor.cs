using System.Security.Claims;

namespace Diploma.DependencyInjection
{
    public class UserAccessor: IUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public ClaimsPrincipal User => _accessor.HttpContext.User;
    }

    public interface IUserAccessor
    {
        public ClaimsPrincipal User { get;}
    }
}
