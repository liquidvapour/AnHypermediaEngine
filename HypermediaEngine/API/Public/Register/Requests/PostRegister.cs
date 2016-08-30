using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Register.Requests
{
    public class PostRegister : ApiAction
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public PostRegister() : base("Register", "POST", "/api/register", Claim.Public)
        {
        }
    }
}