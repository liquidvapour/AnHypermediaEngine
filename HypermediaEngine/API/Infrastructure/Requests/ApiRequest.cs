
using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests
{
    public abstract class ApiRequest
    {
        public string Title { get; private set; }
        public string Href { get; private set; }
        public Claim Claim { get; private set; }

        protected ApiRequest(string title, string href, Claim claim)
        {
            Title = title;
            Href = href;
            Claim = claim;
        }
    }
}