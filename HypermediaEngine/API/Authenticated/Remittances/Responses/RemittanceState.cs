using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Users;
using HypermediaEngine.API.Authenticated.PaymentMethods.Requests;
using HypermediaEngine.API.Authenticated.Remittances.Requests;
using HypermediaEngine.API.Authenticated.SendingReasons.Requests;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using HypermediaEngine.API.Public.Languages.Requests;
using HypermediaEngine.API.Public.Quotes.Requests;
using HypermediaEngine.API.Public.Users;
using HypermediaEngine.API.Public.Users.Logout.Requests;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Remittances.Responses
{
    public class RemittanceState : Entity
    {
        public RemittanceState(NancyContext context) : base(context.Request.Url.ToString(), new[] { "remittance" })
        {
            Properties = new Dictionary<string, object>
            {
                { "title", "Send Money Online Anytime, Anywhere" }
            };

            Links = new LinksFactory(context)
                            .With(new HomeLink())
                            .With(new ListRemittances())
                            .Build();
        }

        public RemittanceState(NancyContext context, Remittance remittance, IList<Recipient> recipients) : this(context)
        {
            var actionsFactory = new ActionsFactory(context)
                                        .With(new LogoutAction())
                                        .With(new ChangeLanguageAction("en-GB"),
                                                    WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()));

            if (recipients.Any())
            {
                var recipientsList = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(null, "New Recipient") };
                recipientsList.AddRange(recipients.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)));

                actionsFactory = actionsFactory.With(new SelectRecipientAction(remittance.Id),
                                                            WithAction<SelectRecipientAction>.Field(x => x.RecipientId)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.Options = recipientsList))
                                               .With(new AddRecipientAction(remittance.Id, remittance.Corridor.Destination.Code),
                                                            WithAction<AddRecipientAction>.Field(x => x.Country)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                                {
                                                                    new KeyValuePair<string, string>(remittance.Corridor.Destination.Code, remittance.Corridor.Destination.Name),
                                                                }),
                                                            WithAction<AddRecipientAction>.Field(x => x.SendingReasonId)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new SendingReasonsLookup()).Build().Single()));
            }
            else
            {
                actionsFactory = actionsFactory.With(new AddRecipientAction(remittance.Id, remittance.Corridor.Destination.Code),
                                                            WithAction<AddRecipientAction>.Field(x => x.Country)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                                {
                                                                    new KeyValuePair<string, string>(remittance.Corridor.Destination.Code, remittance.Corridor.Destination.Name),
                                                                }),
                                                            WithAction<AddRecipientAction>.Field(x => x.SendingReasonId)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new SendingReasonsLookup()).Build().Single()));
            }

            Actions = actionsFactory.Build();
        }

        public RemittanceState(NancyContext context, Remittance remittance, IEnumerable<Recipient> recipients, Recipient recipient) : this(context)
        {
            var actionsFactory = new ActionsFactory(context)
                                        .With(new LogoutAction())
                                        .With(new ChangeLanguageAction("en-GB"),
                                                    WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()));

            var recipientsList = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(null, "New Recipient") };
            recipientsList.AddRange(recipients.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)));

            actionsFactory = actionsFactory.With(new SelectRecipientAction(remittance.Id, recipient.Id),
                                                        WithAction<SelectRecipientAction>.Field(x => x.RecipientId)
                                                            .Having(x => x.Type = "select")
                                                            .Having(x => x.Options = recipientsList))
                                            .With(new UpdateRecipientAction(remittance.Id, recipient.Id, recipient.Name, recipient.Country, recipient.City, recipient.Address, recipient.Postcode, recipient.SendingReasonId),
                                                            WithAction<UpdateRecipientAction>.Field(x => x.Country)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                                {
                                                                    new KeyValuePair<string, string>(remittance.Corridor.Destination.Code, remittance.Corridor.Destination.Name),
                                                                }),
                                                            WithAction<UpdateRecipientAction>.Field(x => x.SendingReasonId)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new SendingReasonsLookup()).Build().Single()));

            Actions = actionsFactory.Build();
        }

        public RemittanceState(NancyContext context, Remittance remittance, Recipient recipient) : this(context)
        {
            Actions = new ActionsFactory(context)
                            .With(new SelectPaymentMethodAction(remittance.Id),
                                        WithAction<SelectPaymentMethodAction>.Field(x => x.PaymentMethodId)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new PaymentMethodsLookup()).Build().Single()))
                            .With(new LogoutAction())
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .Build();
        }

        public RemittanceState(NancyContext context, Remittance remittance, Recipient recipient, PaymentMethod paymentMethod) : this(context)
        {
            Actions = new ActionsFactory(context)
                            .With(new SelectPaymentMethodAction(remittance.Id, paymentMethod.Id),
                                        WithAction<SelectPaymentMethodAction>.Field(x => x.PaymentMethodId)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new PaymentMethodsLookup()).Build().Single()))
                            .With(new BankPaymentAction(remittance.Id))
                            .With(new LogoutAction())
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .Build();
        }

        public RemittanceState(NancyContext context, Remittance remittance, Recipient recipient, PaymentMethod paymentMethod, Payment payment) : this(context)
        {
            Properties.Add("Success", "Your remittance as been accepted and is now being processed!...");

            Actions = new ActionsFactory(context)
                                        .With(new LogoutAction())
                                        .With(new ChangeLanguageAction("en-GB"),
                                                    WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                                        .Build();
        }
    }
}