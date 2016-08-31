using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class SaveRemittanceAction : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public Guid DeliveryMethodId { get; set; }

        public string Currency { get; set; }
        public decimal Amount { get; set; }

        public SaveRemittanceAction() : this("/api/remittances")
        {
        }

        protected SaveRemittanceAction(string href) : base("Send Now", "POST", href)
        {
        }

        public SaveRemittanceAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, decimal amount) : this(originCountry, destinationCountry, deliveryMethodId, currency, amount, "/api/remittances")
        {
        }

        protected SaveRemittanceAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, decimal amount, string href) : this(href)
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
            DeliveryMethodId = deliveryMethodId;
            Currency = currency;
            Amount = amount;
        }
    }
}