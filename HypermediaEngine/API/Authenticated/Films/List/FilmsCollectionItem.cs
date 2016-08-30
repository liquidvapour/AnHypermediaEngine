using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Films.Get;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Films.List
{
    public class FilmsCollectionItem: Entity
    {
        public FilmsCollectionItem(NancyContext context, Domain.Film film)
            : base(string.Format("{0}{1}{2}/{3}", context.Request.Url.SiteBase, context.Request.Url.BasePath, context.Request.Url.Path, film.Id), "film")
        {
            Properties = new Dictionary<string, object>
            {
                { "Thumbnail", film.ImageThumbnail },
                { "Name", film.Name }
            };
                
            Links = new LinksFactory(context).With(new GetFilm(film.Id)).Build();
        }
    }
}