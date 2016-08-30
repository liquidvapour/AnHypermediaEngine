using System.Collections.Generic;
using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests.Links
{
    public abstract class ApiLinkPaged : ApiLink
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        protected ApiLinkPaged(string title, string href, Claim claim, IList<string> rel, int pageNumber = 0, int pageSize = 10) : base(title, href, rel, claim)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}