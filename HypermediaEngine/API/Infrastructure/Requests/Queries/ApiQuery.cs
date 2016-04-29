using System.Collections.Generic;
using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests.Queries
{
    public abstract class ApiQuery : ApiRequest
    {
        public IList<string> Rel { get; private set; }

        protected ApiQuery(string title, string href, Claim claim, IList<string> rel) : base(title, href, claim)
        {
            Rel = rel;
        }
    }
}