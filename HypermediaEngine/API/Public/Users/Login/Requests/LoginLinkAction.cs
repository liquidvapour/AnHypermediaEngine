using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Login.Requests
{
    public class LoginLinkAction : ApiAction
    {
        public LoginLinkAction() : base("Login", "GET", "/api/login", false)
        {
        }
    }
}