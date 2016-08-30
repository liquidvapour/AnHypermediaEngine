using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Users.List
{
    public class GetUsers : ApiLinkPaged
    {
        public GetUsers() : base("Users", "/api/users", Claim.Administrator, new[] { "users" })
        {
        }
    }
}