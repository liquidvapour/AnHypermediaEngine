using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Films.List;
using HypermediaEngine.API.Authenticated.Films.Update;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Films.Get
{
    public class Film: Entity
    {
        public Film(NancyContext context, Domain.Film film) : base(context.Request.Url.ToString(), "film")
        {
            Properties = new Dictionary<string, object>
                             {
                                 { "Image", film.Image },
                                 { "Name", film.Name },
                                 { "Description", film.Description }
                             };
                
            Links = new LinksFactory(context).With(new GetFilms("Back to all films")).Build();
            Actions = new ActionsFactory(context).With(new PostFilm(film)).Build();
        }
    }
}