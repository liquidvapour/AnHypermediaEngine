using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Books.List
{
    public class GetBooks : ApiLinkPaged
    {
        public GetBooks() : base("Books", "/api/books", Claim.Books, new[] { "books" })
        {
        }
    }
}