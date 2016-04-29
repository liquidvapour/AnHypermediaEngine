using System.Collections.Generic;
using System.Linq;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Paintings.List
{
    public class PaintingsCollection: Entity
    {
        public PaintingsCollection(NancyContext context, IEnumerable<Domain.Painting> paintings) : base(context.Request.Url.ToString(), new[] { "paintings", "collection" })
        {
            int pageNumber = context.Request.Query.PageNumber;
            int pageSize = context.Request.Query.PageSize;
            int totalEntries = paintings.Count();

            var paintingsPage = paintings.Skip(pageNumber * pageSize).Take(pageSize);

            Properties = new Dictionary<string, object> { { "Page Details", PagedProperties.GetPageDetails(pageNumber, pageSize, totalEntries) } };
            Entities = new List<Entity>(paintingsPage.Select(x => new PaintingsCollectionItem(context, x)));
            Links = new LinksFactory(context).WithPage<GetPaintings>(pageNumber, pageSize, totalEntries).Build();
        }
    }
}