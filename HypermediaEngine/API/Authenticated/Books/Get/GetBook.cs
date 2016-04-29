using System;
using System.Collections.Generic;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Books.Get
{
    public class GetBook : ApiQuery
    {
        public Guid Id { get; set; }

        public GetBook() : base("Product", "/api/books/{id}", Claim.Books, new[] { "book" })
        {
        }

        public GetBook(Guid id, IList<string> rel) : base("Book", "/api/books/{id}", Claim.Books, rel)
        {
            Id = id;
        }
    }
}