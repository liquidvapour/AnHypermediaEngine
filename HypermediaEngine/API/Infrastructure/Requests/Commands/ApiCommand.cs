using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests.Commands
{
    public abstract class ApiCommand : ApiRequest
    {
        public string Method { get; private set; }

        protected ApiCommand(string title, string method, string href, Claim claim) : base(title, href, claim)
        {
            Method = method;
        }
    }
}