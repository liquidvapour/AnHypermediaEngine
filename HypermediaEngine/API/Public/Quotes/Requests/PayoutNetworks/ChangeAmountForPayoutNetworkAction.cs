using System;

namespace HypermediaEngine.API.Public.Quotes.Requests.PayoutNetworks
{
    public class ChangeAmountForPayoutNetworkAction : ChangeAmountAction
    {
        public Guid PayoutNetworkId { get; set; }

        public ChangeAmountForPayoutNetworkAction() : base("/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}/payoutNetworks/{payoutNetworkId}")
        {
        }

        public ChangeAmountForPayoutNetworkAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, Guid payoutNetworkId) : base(originCountry, destinationCountry, deliveryMethodId, currency, "/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}/payoutNetworks/{payoutNetworkId}")
        {
            PayoutNetworkId = payoutNetworkId;
        }

        public ChangeAmountForPayoutNetworkAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, decimal amount, Guid payoutNetworkId) : base(originCountry, destinationCountry, deliveryMethodId, currency, amount, "/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}/payoutNetworks/{payoutNetworkId}")
        {
            PayoutNetworkId = payoutNetworkId;
        }
    }
}