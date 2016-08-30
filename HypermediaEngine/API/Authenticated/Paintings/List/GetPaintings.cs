using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Paintings.List
{
    public class GetPaintings : ApiLinkPaged
    {
        public GetPaintings() : base("Paintings", "/api/paintings", Claim.Paintings, new[] { "paintings" })
        {
        }
    }
}