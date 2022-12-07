using System.Security.Claims;

namespace app_ingenieria_ufinet.Utils
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }
}
