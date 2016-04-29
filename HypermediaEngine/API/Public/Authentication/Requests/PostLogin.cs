using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Public.Authentication.Requests
{
    public class PostLogin : ApiCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public PostLogin() : base("Login", "POST", "/api/login", Claim.Public)
        {
        }
    }
}