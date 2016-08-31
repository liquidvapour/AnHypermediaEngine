using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.PayoutNetworks.Requests
{
    public class PayoutNetworksLookup : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public Guid DeliveryMethodId { get; set; }

        public PayoutNetworksLookup() : base("Payout Network Lookup", "GET", "/api/payoutNetworks/{originCountry}/{destinationCountry}/{deliveryMethodId}", false)
        {
        }

        public PayoutNetworksLookup(string originCountry, string destinationCountry, Guid deliveryMethodId) : this()
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
            DeliveryMethodId = deliveryMethodId;
        }
    }
}