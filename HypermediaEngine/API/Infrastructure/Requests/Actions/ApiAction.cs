using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests.Actions
{
    public abstract class ApiAction : ApiRequest
    {
        public string Method { get; private set; }

        protected ApiAction(string title, string method, string href, Claim claim) : base(title, href, claim)
        {
            Method = method;
        }
    }
}