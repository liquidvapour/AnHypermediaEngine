using System.Collections.Generic;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Users.Login.Responses
{
    public class LoginActionFailed : Entity
    {
        public LoginActionFailed(NancyContext context) : base(context.Request.Url.ToString(), new[] { "login", "failed", "error" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", "Login failed: incorrect username/password..." } };
        }
    }
}