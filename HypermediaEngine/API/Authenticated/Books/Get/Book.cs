using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Books.List;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using Nancy;
using Siren;

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
                            .With(new GetBooks(), WithLink<GetBooks>.Property(x => x.Title = "Back to all books"))
                            .Build();
        }
    }
}