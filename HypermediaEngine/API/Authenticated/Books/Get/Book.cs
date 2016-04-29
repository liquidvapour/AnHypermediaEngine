using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Books.List;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Books.Get
{
    public class Book : Entity
    {
        public Book(NancyContext context, Domain.Book book) : base(context.Request.Url.ToString(), "book")
        {
            Properties = new Dictionary<string, object>
                             {
                                 { "Name", book.Name },
                                 { "ISBN", book.Isbn },
                                 { "Description", book.Description }
                             };
                
            Links = new LinksFactory(context)
                            .With(new GetBooks("Back to all books"))
                            .Build();
        }
    }
}