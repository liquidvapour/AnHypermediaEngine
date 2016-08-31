using System;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests.PayoutNetworks
{
    public class SaveRemittanceForPayoutNetworkAction : SaveRemittanceAction
    {
        public Guid PayoutNetworkId { get; set; }

        public SaveRemittanceForPayoutNetworkAction() : base("/api/remittances/payoutNetworks")
        {
        }

        public SaveRemittanceForPayoutNetworkAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, decimal amount, Guid payoutNetworkId) : base(originCountry, destinationCountry, deliveryMethodId, currency, amount, "/api/remittances/payoutNetworks")
        {
            PayoutNetworkId = payoutNetworkId;
        }
    }
}