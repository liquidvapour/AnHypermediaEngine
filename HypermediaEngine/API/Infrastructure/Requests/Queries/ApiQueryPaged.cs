using System.Collections.Generic;
using Core.Primitives;

namespace HypermediaEngine.API.Infrastructure.Requests.Queries
{
    public abstract class ApiQueryPaged : ApiQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        protected ApiQueryPaged(string title, string href, Claim claim, IList<string> rel, int pageNumber = 0, int pageSize = 10) : base(title, href, claim, rel)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}