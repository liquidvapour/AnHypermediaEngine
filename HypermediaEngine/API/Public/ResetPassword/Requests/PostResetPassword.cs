using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.ResetPassword.Requests
{
    public class PostResetPassword : ApiAction
    {
        public string Username { get; set; }

        public PostResetPassword() : base("Reset Password", "POST", "/api/users/resetPassword", Claim.Public)
        {
        }
    }
}