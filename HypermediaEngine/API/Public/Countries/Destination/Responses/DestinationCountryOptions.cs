using System.Collections.Generic;
using System.Linq;
using Domain;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Countries.Destination.Responses
{
    public class DestinationCountryOptions : Entity
    {
        public DestinationCountryOptions(NancyContext context, IEnumerable<Country> countries) : base(context.Request.Url.ToString(), new[] { "destination-country-options" })
        {
            var options = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("", "Select destination country") };
            options.AddRange(countries.Select(x => new KeyValuePair<string, string>(x.Code, x.Name)));

            Properties = options.ToDictionary(x => x.Key, x => (object)x.Value);
        }
    }
}