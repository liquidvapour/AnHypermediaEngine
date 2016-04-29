using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Users.List
{
    public class GetUsers : ApiQueryPaged
    {
        public GetUsers() : base("Users", "/api/users", Claim.Administrator, new[] { "users" })
        {
        }

        public GetUsers(string title) : base(title, "/api/users", Claim.Administrator, new[] { "users" } )
        {
        }

        public GetUsers(string title, int pageNumber, int pageSize) : base(title, "/api/users", Claim.Administrator, new[] { "users" }, pageNumber, pageSize)
        {
        }
    }
}