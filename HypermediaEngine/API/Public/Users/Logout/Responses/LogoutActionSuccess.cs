using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Users.Logout.Responses
{
    public class LogoutActionSuccess : Entity
    {
        public LogoutActionSuccess(NancyContext context) : base(context.Request.Url.ToString(), new[] { "logout", "success" })
        {
        }
    }
}