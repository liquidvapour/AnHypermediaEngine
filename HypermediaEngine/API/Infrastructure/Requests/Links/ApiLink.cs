using System.Collections.Generic;

namespace HypermediaEngine.API.Infrastructure.Requests.Links
{
    public abstract class ApiLink : ApiRequest
    {
        public IList<string> Rel { get; private set; }

        protected ApiLink(string title, string href, IList<string> rel, bool requiresAuthentication = false) : base(title, href, requiresAuthentication)
        {
            Rel = rel;
        }
    }
}