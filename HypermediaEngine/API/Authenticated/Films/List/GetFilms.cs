using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Films.List
{
    public class GetFilms : ApiQueryPaged
    {
        public string Search { get; set; }

        public GetFilms() : base("Films", "/api/films?search={search}", Claim.Films, new[] { "films" }, 0, 12)
        {
        }

        public GetFilms(string title) : base(title, "/api/films?search={search}", Claim.Films, new[] { "films" }, 0, 12)
        {
        }

        public GetFilms(string title, int pageNumber, int pageSize) : base(title, "/api/films?search={search}", Claim.Films, new[] { "films" }, pageNumber, pageSize)
        {
        }
    }
}