using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class SelectOriginCountryAction : ApiAction
    {
        public string OriginCountry { get; set; }

        public SelectOriginCountryAction() : base("Send from", "GET", "/api/quote", false)
        {
        }

        public SelectOriginCountryAction(string originCountry) : this()
        {
            OriginCountry = originCountry;
        }
    }
}