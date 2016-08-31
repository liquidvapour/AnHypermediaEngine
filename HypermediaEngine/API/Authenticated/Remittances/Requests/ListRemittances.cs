using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class ListRemittances : ApiLink
    {
        public ListRemittances() : base("Remittances", "/api/remittances", new [] { "remittances-list" }, true)
        {
        }
    }
}