using System.Collections.Generic;
using Nancy;

namespace HypermediaEngine.API.Public.Authentication.Responses
{
    public class LoginFailed : LoginResponse
    {
        public LoginFailed(NancyContext context) : base(context.Request.Url.ToString(), new[] { "login", "failed", "error" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", "Login failed: incorrect username/password..." } };
        }
    }
}