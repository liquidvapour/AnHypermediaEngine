using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Public.Users.Login.Requests
{
    public class LoginLink : ApiLink
    {
        public LoginLink() : base("Login", "/api/login", new [] { "login" })
        {
        }
    }
}