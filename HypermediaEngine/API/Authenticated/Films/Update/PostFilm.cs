using System;
using Domain;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Films.Update
{
    public class PostFilm : ApiAction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public PostFilm() : base("Update", "POST", "/api/films/{id}", Core.Primitives.Claim.Films)
        {
        }

        public PostFilm(Film film) : base("Update", "POST", "/api/films/{id}", Core.Primitives.Claim.Films)
        {
            Id = film.Id;
            Name = film.Name;
        }
    }
}