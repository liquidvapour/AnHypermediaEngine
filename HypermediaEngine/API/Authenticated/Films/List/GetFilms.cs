using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Films.List
{
    public class GetFilms : ApiLinkPaged
    {
        public GetFilms() : base("Films", "/api/films", Claim.Films, new[] { "films" }, 0, 12)
        {
        }
    }
}