using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Books.Get;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Books.List
{
    public class BooksCollectionItem : Entity
    {
        public BooksCollectionItem(NancyContext context, Domain.Book book) 
            : base(string.Format("{0}{1}{2}/{3}", context.Request.Url.SiteBase, context.Request.Url.BasePath, context.Request.Url.Path, book.Id), "book")
        {
            Properties = new Dictionary<string, object>
                             {
                                 { "ISBN", book.Isbn },
                                 { "Name", book.Name },
                                 { "Added On", book.CreatedOn.ToShortDateString() }
                             };

            Links = new LinksFactory(context).With(new GetBook(book.Id, new[] { "book", "detail" })).Build();
        }
    }
}