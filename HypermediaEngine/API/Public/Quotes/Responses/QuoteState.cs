using System.Collections.Generic;
using System.Linq;
using Domain;
using HypermediaEngine.API.Authenticated.Remittances.Requests;
using HypermediaEngine.API.Authenticated.Remittances.Requests.PayoutNetworks;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using HypermediaEngine.API.Public.Countries.Destination.Requests;
using HypermediaEngine.API.Public.Countries.Origin.Requests;
using HypermediaEngine.API.Public.DeliveryMethods.Requests;
using HypermediaEngine.API.Public.Languages.Requests;
using HypermediaEngine.API.Public.PayoutNetworks.Requests;
using HypermediaEngine.API.Public.Quotes.Requests;
using HypermediaEngine.API.Public.Quotes.Requests.PayoutNetworks;
using HypermediaEngine.API.Public.Users;
using HypermediaEngine.API.Public.Users.Login.Requests;
using HypermediaEngine.API.Public.Users.Logout.Requests;
using HypermediaEngine.API.Public.Users.Register.Requests;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Quotes.Responses
{
    public class QuoteState: Entity
    {
        public QuoteState(NancyContext context) : base(context.Request.Url.ToString(), new[] { "quote" })
        {
            Properties = new Dictionary<string, object>
            {
                { "title", "Send Money Online Anytime, Anywhere" }
            };

            var linksBuilder = new LinksFactory(context)
                                    .With(new HomeLink())
                                    .With(new ListRemittances());

            if (context.IsUserAuthenticated() == false)
                linksBuilder = linksBuilder
                                .With(new LoginLink(), WithLink<LoginLink>.Property(x => x.Href = context.WithRedirectFor(x.Href)))
                                .With(new RegisterLink(), WithLink<RegisterLink>.Property(x => x.Href = context.WithRedirectFor(x.Href)));

            Links = linksBuilder.Build();
        }

        public QuoteState(NancyContext context, Country origin) : this(context)
        {
            Actions = new ActionsFactory(context)
                            .With(new SelectOriginCountryAction(origin.Code),
                                        WithAction<SelectOriginCountryAction>.Field(x => x.OriginCountry)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                            .With(new SelectDestinationCountryAction(origin.Code),
                                        WithAction<SelectDestinationCountryAction>.Field(x => x.DestinationCountry)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DestinationCountriesLookup(origin.Code)).Build().Single()))
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .With(new LogoutAction())
                            .Build();
        }

        public QuoteState(NancyContext context, Country origin, Country destination) : this(context)
        {
            Actions = new ActionsFactory(context)
                            .With(new SelectOriginCountryAction(origin.Code),
                                        WithAction<SelectOriginCountryAction>.Field(x => x.OriginCountry)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                            .With(new SelectDestinationCountryAction(origin.Code, destination.Code),
                                        WithAction<SelectDestinationCountryAction>.Field(x => x.DestinationCountry)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DestinationCountriesLookup(origin.Code)).Build().Single()))
                            .With(new SelectDeliveryMethodAction(origin.Code, destination.Code),
                                        WithAction<SelectDeliveryMethodAction>.Field(x => x.DeliveryMethodId)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DeliveryMethodsLookup(origin.Code, destination.Code)).Build().Single()))
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .With(new LogoutAction())
                            .Build();
        }


        public QuoteState(NancyContext context, Country origin, Country destination, DeliveryMethod deliveryMethod) : this(context)
        {
            var actionsFactory = new ActionsFactory(context)
                                        .With(new SelectOriginCountryAction(origin.Code),
                                                    WithAction<SelectOriginCountryAction>.Field(x => x.OriginCountry)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                                        .With(new SelectDestinationCountryAction(origin.Code, destination.Code),
                                                    WithAction<SelectDestinationCountryAction>.Field(x => x.DestinationCountry)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DestinationCountriesLookup(origin.Code)).Build().Single()))
                                        .With(new SelectDeliveryMethodAction(origin.Code, destination.Code, deliveryMethod.Id),
                                                    WithAction<SelectDeliveryMethodAction>.Field(x => x.DeliveryMethodId)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DeliveryMethodsLookup(origin.Code, destination.Code)).Build().Single()))
                                        .With(new ChangeLanguageAction("en-GB"),
                                                    WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                                        .With(new LogoutAction());


            if (deliveryMethod.Type.Code == "bnk" || deliveryMethod.Type.Code == "csh" || deliveryMethod.Type.Code == "mob")
            {
                actionsFactory = actionsFactory.With(new SelectPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id),
                                                            WithAction<SelectPayoutNetworkAction>.Field(x => x.PayoutNetworkId)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new PayoutNetworksLookup(origin.Code, destination.Code, deliveryMethod.Id)).Build().Single()));
            }

            if (deliveryMethod.Type.Code == "dtd")
            {
                actionsFactory = actionsFactory.With(new ChangeAmountAction(origin.Code, destination.Code, deliveryMethod.Id, origin.Currency.Code),
                                                            WithAction<ChangeAmountAction>.Field(x => x.Currency)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                                {
                                                                    new KeyValuePair<string, string>(origin.Currency.Code, origin.Currency.Name),
                                                                    new KeyValuePair<string, string>(destination.Currency.Code, destination.Currency.Name)
                                                                }),
                                                            WithAction<ChangeAmountAction>.Field(x => x.Amount)
                                                                .Having(x => x.Min = 0)
                                                                .Having(x => x.Step = 0.2m));
            }

            Actions = actionsFactory.Build();
        }

        public QuoteState(NancyContext context, Country origin, Country destination, DeliveryMethod deliveryMethod, Quote quote)
            : this(context)
        {
            var actionsFactory = new ActionsFactory(context)
                                                    .With(new SelectOriginCountryAction(origin.Code),
                                                                WithAction<SelectOriginCountryAction>.Field(x => x.OriginCountry)
                                                                    .Having(x => x.Type = "select")
                                                                    .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                                                    .With(new SelectDestinationCountryAction(origin.Code, destination.Code),
                                                                WithAction<SelectDestinationCountryAction>.Field(x => x.DestinationCountry)
                                                                    .Having(x => x.Type = "select")
                                                                    .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DestinationCountriesLookup(origin.Code)).Build().Single()))
                                                    .With(new SelectDeliveryMethodAction(origin.Code, destination.Code, deliveryMethod.Id),
                                                                WithAction<SelectDeliveryMethodAction>.Field(x => x.DeliveryMethodId)
                                                                    .Having(x => x.Type = "select")
                                                                    .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DeliveryMethodsLookup(origin.Code, destination.Code)).Build().Single()))
                                                    .With(new ChangeLanguageAction("en-GB"),
                                                                WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                                                    .Having(x => x.Type = "select")
                                                                    .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                                                    .With(new LogoutAction());


            if (deliveryMethod.Type.Code == "bnk" || deliveryMethod.Type.Code == "csh" || deliveryMethod.Type.Code == "mob")
            {
                actionsFactory = actionsFactory.With(new SelectPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id),
                                                            WithAction<SelectPayoutNetworkAction>.Field(x => x.PayoutNetworkId)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new PayoutNetworksLookup(origin.Code, destination.Code, deliveryMethod.Id)).Build().Single()));
            }

            if (deliveryMethod.Type.Code == "dtd")
            {
                actionsFactory = actionsFactory.With(new ChangeAmountAction(origin.Code, destination.Code, deliveryMethod.Id, quote.RequestedCurrency, quote.RequestedAmount),
                                                            WithAction<ChangeAmountAction>.Field(x => x.Currency)
                                                                .Having(x => x.Type = "select")
                                                                .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                                {
                                                                    new KeyValuePair<string, string>(origin.Currency.Code, origin.Currency.Name),
                                                                    new KeyValuePair<string, string>(destination.Currency.Code, destination.Currency.Name)
                                                                }),
                                                            WithAction<ChangeAmountAction>.Field(x => x.Amount)
                                                                .Having(x => x.Min = 0)
                                                                .Having(x => x.Step = 0.2m));
            }


            if (quote.RequestedAmount > 0)
            {
                Properties.Add("Send amount", quote.OriginCurrency + " " + quote.OriginAmount.ToString("0.00"));
                Properties.Add("Fees", quote.OriginCurrency + " " + quote.Fees.ToString("0.00"));
                Properties.Add("Total to pay", quote.OriginCurrency + " " + quote.Payment.ToString("0.00"));
                Properties.Add("Your recipient gets", quote.DestinationCurrency + " " + quote.DestinationAmount.ToString("0.00"));
                Properties.Add("Exchange rate", string.Format("{0} 1 = {1} {2}", quote.OriginCurrency, quote.DestinationCurrency, 1 * quote.ExchangeRate));

                if (context.IsUserAuthenticated())
                    actionsFactory = actionsFactory.With(new SaveRemittanceAction(origin.Code, destination.Code, deliveryMethod.Id, quote.RequestedCurrency, quote.RequestedAmount),
                                                                                WithAction<SaveRemittanceAction>.Field(x => x.OriginCountry).Having(x => x.Type = "hidden"),
                                                                                WithAction<SaveRemittanceAction>.Field(x => x.DestinationCountry).Having(x => x.Type = "hidden"),
                                                                                WithAction<SaveRemittanceAction>.Field(x => x.DeliveryMethodId).Having(x => x.Type = "hidden"),
                                                                                WithAction<SaveRemittanceAction>.Field(x => x.Currency).Having(x => x.Type = "hidden"),
                                                                                WithAction<SaveRemittanceAction>.Field(x => x.Amount).Having(x => x.Type = "hidden"));
                else
                    actionsFactory = actionsFactory.With(new LoginLinkAction(),
                                                                WithAction<LoginLinkAction>.Property(x => x.Title = "Send Now"),
                                                                WithAction<LoginLinkAction>.Property(x => x.Name = "send-now"),
                                                                WithAction<LoginLinkAction>.Property(x => x.Href = context.WithRedirectFor(x.Href)));
            }

            Actions = actionsFactory.Build();
        }


        public QuoteState(NancyContext context, Country origin, Country destination, DeliveryMethod deliveryMethod, PayoutNetwork payoutNetwork)
            : this(context)
        {
            Actions = new ActionsFactory(context)
                            .With(new SelectOriginCountryAction(origin.Code),
                                        WithAction<SelectOriginCountryAction>.Field(x => x.OriginCountry)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                            .With(new SelectDestinationCountryAction(origin.Code, destination.Code),
                                        WithAction<SelectDestinationCountryAction>.Field(x => x.DestinationCountry)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DestinationCountriesLookup(origin.Code)).Build().Single()))
                            .With(new SelectDeliveryMethodAction(origin.Code, destination.Code, deliveryMethod.Id),
                                        WithAction<SelectDeliveryMethodAction>.Field(x => x.DeliveryMethodId)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DeliveryMethodsLookup(origin.Code, destination.Code)).Build().Single()))
                            .With(new SelectPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id, payoutNetwork.Id),
                                                WithAction<SelectPayoutNetworkAction>.Field(x => x.PayoutNetworkId)
                                                    .Having(x => x.Type = "select")
                                                    .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new PayoutNetworksLookup(origin.Code, destination.Code, deliveryMethod.Id)).Build().Single()))
                            .With(new ChangeAmountForPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id, origin.Currency.Code, payoutNetwork.Id),
                                                WithAction<ChangeAmountForPayoutNetworkAction>.Field(x => x.Currency)
                                                    .Having(x => x.Type = "select")
                                                    .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                    {
                                                        new KeyValuePair<string, string>(origin.Currency.Code, origin.Currency.Name),
                                                        new KeyValuePair<string, string>(destination.Currency.Code, destination.Currency.Name)
                                                    }),
                                                WithAction<ChangeAmountForPayoutNetworkAction>.Field(x => x.Amount)
                                                    .Having(x => x.Min = 0)
                                                    .Having(x => x.Step = 0.2m))
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .With(new LogoutAction())
                            .Build();
        }

        public QuoteState(NancyContext context, Country origin, Country destination, DeliveryMethod deliveryMethod, PayoutNetwork payoutNetwork, Quote quote)
            : this(context, origin, destination, deliveryMethod, payoutNetwork)
        {
            var actionsFactory = new ActionsFactory(context)
                                        .With(new SelectOriginCountryAction(origin.Code),
                                                    WithAction<SelectOriginCountryAction>.Field(x => x.OriginCountry)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                                        .With(new SelectDestinationCountryAction(origin.Code, destination.Code),
                                                    WithAction<SelectDestinationCountryAction>.Field(x => x.DestinationCountry)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DestinationCountriesLookup(origin.Code)).Build().Single()))
                                        .With(new SelectDeliveryMethodAction(origin.Code, destination.Code, deliveryMethod.Id),
                                                    WithAction<SelectDeliveryMethodAction>.Field(x => x.DeliveryMethodId)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new DeliveryMethodsLookup(origin.Code, destination.Code)).Build().Single()))
                                        .With(new SelectPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id, payoutNetwork.Id),
                                                    WithAction<SelectPayoutNetworkAction>.Field(x => x.PayoutNetworkId)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new PayoutNetworksLookup(origin.Code, destination.Code, deliveryMethod.Id)).Build().Single()))
                                        .With(new ChangeAmountForPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id, quote.RequestedCurrency, quote.RequestedAmount, payoutNetwork.Id),
                                                    WithAction<ChangeAmountForPayoutNetworkAction>.Field(x => x.Currency)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.Options = new List<KeyValuePair<string, string>>
                                                            {
                                                                new KeyValuePair<string, string>(origin.Currency.Code, origin.Currency.Name),
                                                                new KeyValuePair<string, string>(destination.Currency.Code, destination.Currency.Name)
                                                            }),
                                                    WithAction<ChangeAmountForPayoutNetworkAction>.Field(x => x.Amount)
                                                        .Having(x => x.Min = 0)
                                                        .Having(x => x.Step = 0.2m))
                                        .With(new ChangeLanguageAction("en-GB"),
                                                    WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                                        .Having(x => x.Type = "select")
                                                        .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                                        .With(new LogoutAction());

            if (quote.RequestedAmount > 0)
            {
                Properties.Add("Send amount", quote.OriginCurrency + " " + quote.OriginAmount.ToString("0.00"));
                Properties.Add("Fees", quote.OriginCurrency + " " + quote.Fees.ToString("0.00"));
                Properties.Add("Total to pay", quote.OriginCurrency + " " + quote.Payment.ToString("0.00"));
                Properties.Add("Your recipient gets", quote.DestinationCurrency + " " + quote.DestinationAmount.ToString("0.00"));
                Properties.Add("Exchange rate", string.Format("{0} 1 = {1} {2}", quote.OriginCurrency, quote.DestinationCurrency, 1 * quote.ExchangeRate));

                if (context.IsUserAuthenticated())
                    actionsFactory = actionsFactory.With(new SaveRemittanceForPayoutNetworkAction(origin.Code, destination.Code, deliveryMethod.Id, quote.RequestedCurrency, quote.RequestedAmount, payoutNetwork.Id),
                                                                WithAction<SaveRemittanceForPayoutNetworkAction>.Field(x => x.OriginCountry).Having(x => x.Type = "hidden"),
                                                                WithAction<SaveRemittanceForPayoutNetworkAction>.Field(x => x.DestinationCountry).Having(x => x.Type = "hidden"),
                                                                WithAction<SaveRemittanceForPayoutNetworkAction>.Field(x => x.DeliveryMethodId).Having(x => x.Type = "hidden"),
                                                                WithAction<SaveRemittanceForPayoutNetworkAction>.Field(x => x.PayoutNetworkId).Having(x => x.Type = "hidden"),
                                                                WithAction<SaveRemittanceForPayoutNetworkAction>.Field(x => x.Currency).Having(x => x.Type = "hidden"),
                                                                WithAction<SaveRemittanceForPayoutNetworkAction>.Field(x => x.Amount).Having(x => x.Type = "hidden"));
                else
                    actionsFactory = actionsFactory.With(new LoginLinkAction(),
                                                                WithAction<LoginLinkAction>.Property(x => x.Title = "Send Now"),
                                                                WithAction<LoginLinkAction>.Property(x => x.Name = "send-now"),
                                                                WithAction<LoginLinkAction>.Property(x => x.Href = context.WithRedirectFor(x.Href)));
            }

            Actions = actionsFactory.Build();
        }
    }
}