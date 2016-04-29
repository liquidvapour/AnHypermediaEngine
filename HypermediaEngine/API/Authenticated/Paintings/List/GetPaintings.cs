using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Paintings.List
{
    public class GetPaintings : ApiQueryPaged
    {
        public GetPaintings() : base("Paintings", "/api/paintings", Claim.Paintings, new[] { "paintings" })
        {
        }

        public GetPaintings(string title) : base(title, "/api/paintings", Claim.Paintings, new[] { "paintings" })
        {
        }

        public GetPaintings(string title, int pageNumber, int pageSize) : base(title, "/api/paintings", Claim.Paintings, new[] { "paintings" }, pageNumber, pageSize)
        {
        }
    }
}