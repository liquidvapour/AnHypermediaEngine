using System.Collections.Generic;
using Hypermedia.Siren;
using Nancy;

namespace HypermediaEngine.API.Infrastructure.Errors.Responses
{
    public class NotFoundResponse: Entity
    {
        public NotFoundResponse(NancyContext context) : base(context.Request.Url, new[] { "error", "not-found" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", "Resource not found!..." } };
        }
    }
}