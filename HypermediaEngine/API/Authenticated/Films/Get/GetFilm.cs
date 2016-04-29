using System;
using System.Collections.Generic;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Films.Get
{
    public class GetFilm : ApiQuery
    {
        public Guid Id { get; set; }

        public GetFilm(): base("Film", "/api/films/{id}", Claim.Films, new[] { "film" })
        {
        }

        public GetFilm(Guid id, IList<string> rel): base("Film", "/api/films/{id}", Claim.Films, rel)
        {
            Id = id;
        }
    }
}