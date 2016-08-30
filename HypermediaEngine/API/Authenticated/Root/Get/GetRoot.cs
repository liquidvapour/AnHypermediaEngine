using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Root.Get
{
    public class GetRoot : ApiLink
    {
        public GetRoot() : base("Root", "/api", new[] { "root" }, Claim.Public)
        {
        }
    }
}