using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Public.Register.Requests
{
    public class PostRegister : ApiCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public PostRegister() : base("Register", "POST", "/api/register", Claim.Public)
        {
        }
    }
}