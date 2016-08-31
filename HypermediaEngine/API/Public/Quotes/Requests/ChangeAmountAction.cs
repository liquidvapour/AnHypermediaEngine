using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class ChangeAmountAction : ApiAction
    {
        public string OriginCountry { get; set; }
        public string DestinationCountry { get; set; }

        public Guid DeliveryMethodId { get; set; }

        public string Currency { get; set; }
        public decimal Amount { get; set; }

        public ChangeAmountAction() : this("/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}")
        {
        }

        protected ChangeAmountAction(string href) : base("Amount", "GET", href, false)
        {
        }

        public ChangeAmountAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency) : this()
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
            DeliveryMethodId = deliveryMethodId;
            Currency = currency;
        }

        protected ChangeAmountAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, string href) : this(href)
        {
            OriginCountry = originCountry;
            DestinationCountry = destinationCountry;
            DeliveryMethodId = deliveryMethodId;
            Currency = currency;
        }

        public ChangeAmountAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, decimal amount) : this(originCountry, destinationCountry, deliveryMethodId, currency)
        {
            Amount = amount;
        }

        protected ChangeAmountAction(string originCountry, string destinationCountry, Guid deliveryMethodId, string currency, decimal amount, string href) : this(originCountry, destinationCountry, deliveryMethodId, currency, href)
        {
            Amount = amount;
        }
    }
}