using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Authentication.Requests
{
    public class DeleteLogout : ApiAction
    {
        public DeleteLogout() : base("Log out", "DELETE", "/api/logout", Claim.Public)
        {
        }
    }
}