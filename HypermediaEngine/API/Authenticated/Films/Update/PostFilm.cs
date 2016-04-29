using System;
using Domain;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Authenticated.Films.Update
{
    public class PostFilm : ApiCommand
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