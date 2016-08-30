using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Authentication.Requests
{
    public class PostLogin : ApiAction
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public PostLogin() : base("Login", "POST", "/api/login", Claim.Public)
        {
        }
    }
}