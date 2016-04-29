using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Root.Get
{
    public class GetRoot : ApiQuery
    {
        public GetRoot() : base("Root", "/api", Claim.Public, new[] { "root" })
        {
        }
    }
}