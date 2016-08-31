using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class HomeLink : ApiLink
    {
        public HomeLink() : base("Home", "/api", new[] { "home" })
        {
        }
    }
}