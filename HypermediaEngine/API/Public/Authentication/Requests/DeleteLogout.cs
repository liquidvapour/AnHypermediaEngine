using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Public.Authentication.Requests
{
    public class DeleteLogout : ApiCommand
    {
        public DeleteLogout() : base("Log out", "DELETE", "/api/logout", Claim.Public)
        {
        }
    }
}