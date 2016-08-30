using System.Collections.Generic;
using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests.Links
{
    public abstract class ApiLink : ApiRequest
    {
        public IList<string> Rel { get; private set; }

        protected ApiLink(string title, string href, IList<string> rel, Claim claim) : base(title, href, claim)
        {
            Rel = rel;
        }
    }
}