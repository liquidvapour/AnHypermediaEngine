using System.Collections.Generic;
using System.Linq;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Countries.Origin.Responses
{
    public class OriginCountryOptions : Entity
    {
        public OriginCountryOptions(NancyContext context, IDictionary<string, string> options) : base(context.Request.Url.ToString(), new[] { "origin-country-options" })
        {
            Properties = options.ToDictionary(x => x.Key, x => (object) x.Value);
        }
    }
}