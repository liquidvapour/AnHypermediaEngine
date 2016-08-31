using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class SelectDeliveryMethodAction : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public Guid DeliveryMethodId { get; set; }

        public SelectDeliveryMethodAction() : base("Delivery Method", "GET", "/api/quote/{originCountry}/{destinationCountry}", false)
        {
        }

        public SelectDeliveryMethodAction(string originCountry, string destinationCountry) : this()
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
        }


        public SelectDeliveryMethodAction(string originCountry, string destinationCountry, Guid deliveryMethodId) : this(originCountry, destinationCountry)
        {
            DeliveryMethodId = deliveryMethodId;
        }
    }
}