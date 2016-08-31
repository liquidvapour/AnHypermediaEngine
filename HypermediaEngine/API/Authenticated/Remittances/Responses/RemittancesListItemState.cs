using System.Collections.Generic;
using Domain;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Public.Quotes.Requests;
using HypermediaEngine.API.Public.Quotes.Requests.PayoutNetworks;
using Nancy;
using Nancy.Helpers;
using Siren;

namespace HypermediaEngine.API.Authenticated.Remittances.Responses
{
    public class RemittancesListItemState : Entity
    {
        public RemittancesListItemState(NancyContext context, Remittance remittance) : base(context.Request.Url + "/" + HttpUtility.UrlEncode(remittance.Id.ToString()), new[] { "remittances-list-item" })
        {
            Properties = new Dictionary<string, object>
            {
                { "Status", remittance.Status.ToString() },
                { "Payment", remittance.Payment != null ? remittance.Payment.Status.ToString() : "N/A" },
                { "Destination", remittance.Corridor.Destination.Name },
                { "Recipient", remittance.Recipient != null ? remittance.Recipient.Name : "N/A" },
                { "Amount Sent", remittance.Quote.OriginCurrency + " " + remittance.Quote.OriginAmount },
                { "Amount Received", remittance.Quote.DestinationCurrency + " " + remittance.Quote.DestinationAmount },
                { "Date", remittance.CreatedOn.ToString("dd/MM/yyyy HH:mm") },
            };

            if (remittance.PayoutNetwork != null)
                Actions = new ActionsFactory(context)
                                .With(new ChangeAmountForPayoutNetworkAction(remittance.Corridor.Origin.Code, remittance.Corridor.Destination.Code, remittance.DeliveryMethod.Id, remittance.Quote.RequestedCurrency, remittance.Quote.RequestedAmount, remittance.PayoutNetwork.Id),
                                            WithAction<ChangeAmountForPayoutNetworkAction>.Property(x => x.Title = "Send Again"))
                                .Build();
            else
                Actions = new ActionsFactory(context)
                                .With(new ChangeAmountAction(remittance.Corridor.Origin.Code, remittance.Corridor.Destination.Code, remittance.DeliveryMethod.Id, remittance.Quote.RequestedCurrency, remittance.Quote.RequestedAmount),
                                            WithAction<ChangeAmountAction>.Property(x => x.Title = "Send Again"))
                                .Build();
        }
    }
}