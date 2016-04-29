using Hypermedia.Siren;
using Nancy;

namespace HypermediaEngine.API.Public.Authentication.Responses
{
    public class LogoutSuccess : Entity
    {
        public LogoutSuccess(NancyContext context) : base(context.Request.Url.ToString(), new[] { "logout", "success" })
        {
        }
    }
}