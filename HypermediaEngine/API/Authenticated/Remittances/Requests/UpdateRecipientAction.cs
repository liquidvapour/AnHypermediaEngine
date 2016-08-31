using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class UpdateRecipientAction : ApiAction
    {
        public Guid RemittanceId { get; set; }
        public Guid RecipientId { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }

        public Guid SendingReasonId { get; set; }
        
        public UpdateRecipientAction() : base("Update Recipient", "PUT", "/api/remittances/{remittanceId}/recipients/{recipientId}")
        {
        }

        public UpdateRecipientAction(Guid remittanceId, Guid recipientId, string name, string country, string city, string address, string postcode, Guid sendingReasonId) : this()
        {
            RemittanceId = remittanceId;
            RecipientId = recipientId;
            Name = name;
            Country = country;
            City = city;
            Address = address;
            Postcode = postcode;
            SendingReasonId = sendingReasonId;
        }
    }
}