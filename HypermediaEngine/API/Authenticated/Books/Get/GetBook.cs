using System;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Books.Get
{
    public class GetBook : ApiLink
    {
        public Guid Id { get; set; }

        public GetBook() : base("Book", "/api/books/{id}", new[] { "book", "detail" }, Claim.Books)
        {
        }

        public GetBook(Guid id) : this() 
        {
            Id = id;
        }
    }
}