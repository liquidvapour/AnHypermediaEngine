using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class SelectDestinationCountryAction : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public SelectDestinationCountryAction() : base("Send to", "GET", "/api/quote/{originCountry}", false)
        {
        }

        public SelectDestinationCountryAction(string originCountry) : this()
        {
            OriginCountry = originCountry;
        }

        public SelectDestinationCountryAction(string originCountry, string destinationCountry) : this(originCountry)
        {
            DestinationCountry = destinationCountry;
        }
    }
}