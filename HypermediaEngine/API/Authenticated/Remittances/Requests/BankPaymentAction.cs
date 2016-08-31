using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class BankPaymentAction : ApiAction
    {
        public Guid RemittanceId { get; set; }

        public string AccountHolderName { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string ConfirmAccountNumber { get; set; }

        public BankPaymentAction() : base("Pay Now", "POST", "/api/remittances/{remittanceId}/payment/bank")
        {
        }

        public BankPaymentAction(Guid remittanceId) : this()
        {
            RemittanceId = remittanceId;
        }
    }
}