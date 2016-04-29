using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Public.ResetPassword.Requests
{
    public class PostResetPassword : ApiCommand
    {
        public string Username { get; set; }

        public PostResetPassword() : base("Reset Password", "POST", "/api/users/resetPassword", Claim.Public)
        {
        }
    }
}