using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class SelectPaymentMethodAction : ApiAction
    {
        public Guid RemittanceId { get; set; }
        public Guid PaymentMethodId { get; set; }

        public SelectPaymentMethodAction() : base("Payment Method", "PATCH", "/api/remittances/{remittanceId}/paymentMethod")
        {
        }

        public SelectPaymentMethodAction(Guid remittanceId) : this()
        {
            RemittanceId = remittanceId;
        }

        public SelectPaymentMethodAction(Guid remittanceId, Guid paymentMethodId) : this(remittanceId)
        {
            PaymentMethodId = paymentMethodId;
        }
    }
}