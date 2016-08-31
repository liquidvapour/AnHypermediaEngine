using System;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class AddRecipientAction : ApiAction
    {
        public Guid RemittanceId { get; set; }

        public string Name { get; set; }
        
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        
        public Guid SendingReasonId { get; set; }

        public AddRecipientAction() : base("Add Recipient", "POST", "/api/remittances/{remittanceId}/recipients")
        {
        }

        public AddRecipientAction(Guid remittanceId, string country) : this()
        {
            RemittanceId = remittanceId;
            Country = country;
        }
    }
}