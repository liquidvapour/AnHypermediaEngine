using System.Collections.Generic;
using System.Linq;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Films.List
{
    public class FilmsCollection: Entity
    {
        public FilmsCollection(NancyContext context, IEnumerable<Domain.Film> films) : base(context.Request.Url.ToString(), new[] { "films", "collection" })
        {
            int pageNumber = context.Request.Query.PageNumber;
            int pageSize = context.Request.Query.PageSize;
            int totalEntries = films.Count();

            var filmsPage = films.Where(x => context.Request.Query.Search == null || x.Description.Contains(context.Request.Query.Search))
                                    .Skip(pageNumber * pageSize)
                                    .Take(pageSize);

            Properties = new Dictionary<string, object> { { "Page Details", PagedProperties.GetPageDetails(pageNumber, pageSize, totalEntries) } };
            Entities = new List<Entity>(filmsPage.Select(x => new FilmsCollectionItem(context, x)));
            Links = new LinksFactory(context).WithPage<GetFilms>(pageNumber, pageSize, totalEntries).Build();
        }
    }
}