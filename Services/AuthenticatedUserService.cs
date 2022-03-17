using System.Security.Claims;

namespace BlazorControlCefa.Services
{
    public class AuthenticatedUserService: IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }        

        public string UserId { get; }

        public string Username { get; }

    }
}
