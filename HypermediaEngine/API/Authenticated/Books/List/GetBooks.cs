using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Books.List
{
    public class GetBooks : ApiQueryPaged
    {
        public GetBooks() : base("Books", "/api/books", Claim.Books, new[] { "books" })
        {
        }

        public GetBooks(string title) : base(title, "/api/books", Claim.Books, new[] { "books" } )
        {
        }

        public GetBooks(string title, int pageNumber, int pageSize) : base(title, "/api/books", Claim.Books, new[] { "books" }, pageNumber, pageSize)
        {
        }
    }
}