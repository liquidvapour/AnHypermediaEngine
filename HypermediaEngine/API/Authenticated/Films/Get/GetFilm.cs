using System;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Films.Get
{
    public class GetFilm : ApiLink
    {
        public Guid Id { get; set; }

        public GetFilm(): base("Film", "/api/films/{id}", new[] { "film", "detail" }, Claim.Films)
        {
        }

        public GetFilm(Guid id): this()
        {
            Id = id;
        }
    }
}