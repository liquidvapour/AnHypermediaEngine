using System.Collections.Generic;
using System.Linq;
using Domain;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Books.List
{
    public class BooksCollection: Entity
    {
        public BooksCollection(NancyContext context, IEnumerable<Book> books) : base(context.Request.Url.ToString(), new[] { "books", "collection" })
        {
            int pageNumber = context.Request.Query.PageNumber;
            int pageSize = context.Request.Query.PageSize;
            int totalEntries = books.Count();

            var booksPage =  books.Skip(pageNumber * pageSize).Take(pageSize);

            Properties = new Dictionary<string, object> { { "Page Details", PagedProperties.GetPageDetails(pageNumber, pageSize, totalEntries) } };
            Entities = new List<Entity>(booksPage.Select(x => new BooksCollectionItem(context, x)));
            Links = new LinksFactory(context).WithPage<GetBooks>(pageNumber, pageSize, totalEntries).Build();
        }
    }
}