using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class SelectRecipientAction : ApiAction
    {
        public Guid RemittanceId { get; set; }
        public Guid RecipientId { get; set; }

        public SelectRecipientAction() : base("Select Recipient", "GET", "/api/remittances/{remittanceId}/recipients")
        {
        }

        public SelectRecipientAction(Guid remittanceId) : this()
        {
            RemittanceId = remittanceId;
        }

        public SelectRecipientAction(Guid remittanceId, Guid recipientId) : this(remittanceId)
        {
            RecipientId = recipientId;
        }
    }
}