using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Countries.Destination.Requests
{
    public class DestinationCountriesLookup : ApiAction
    {
        public string OriginCountry { get; set; }

        public DestinationCountriesLookup() : base("Destination Country Lookup", "GET", "/api/countries/{originCountry}", false)
        {
        }

        public DestinationCountriesLookup(string originCountry) : this()
        {
            OriginCountry = originCountry;
        }
    }
}