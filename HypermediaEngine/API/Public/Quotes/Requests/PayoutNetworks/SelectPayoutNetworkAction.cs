using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests.PayoutNetworks
{
    public class SelectPayoutNetworkAction : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public Guid DeliveryMethodId { get; set; }
        public Guid PayoutNetworkId { get; set; }

        public SelectPayoutNetworkAction() : base("Select Payout Network", "GET", "/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}/payoutNetworks", false)
        {
        }

        public SelectPayoutNetworkAction(string originCountry, string destinationCountry, Guid deliveryMethodId) : this()
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
            DeliveryMethodId = deliveryMethodId;
        }

        public SelectPayoutNetworkAction(string originCountry, string destinationCountry, Guid deliveryMethodId, Guid payoutNetworkId) : this(originCountry, destinationCountry, deliveryMethodId)
        {
            PayoutNetworkId = payoutNetworkId;
        }
    }
}