using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Countries.Origin.Requests
{
    public class OriginCountriesLookup : ApiAction
    {
        public OriginCountriesLookup() : base("Origin Country Lookup", "GET", "/api/countries", false)
        {
        }
    }
}