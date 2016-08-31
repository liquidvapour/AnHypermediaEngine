using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.DeliveryMethods.Requests
{
    public class DeliveryMethodsLookup : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public DeliveryMethodsLookup() : base("Delivery Method Lookup", "GET", "/api/deliveryMethods/{originCountry}/{destinationCountry}", false)
        {
        }

        public DeliveryMethodsLookup(string originCountry, string destinationCountry) : this()
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
        }
    }
}