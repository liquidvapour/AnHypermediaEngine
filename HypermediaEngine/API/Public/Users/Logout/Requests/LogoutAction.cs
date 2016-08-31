using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Logout.Requests
{
    public class LogoutAction : ApiAction
    {
        public LogoutAction() : base("Logout", "DELETE", "/api/logout")
        {
        }
    }
}