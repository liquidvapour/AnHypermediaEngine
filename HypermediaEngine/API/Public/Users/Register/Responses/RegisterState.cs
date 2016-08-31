using System.Collections.Generic;
using System.Linq;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using HypermediaEngine.API.Public.Countries.Origin.Requests;
using HypermediaEngine.API.Public.Languages.Requests;
using HypermediaEngine.API.Public.Quotes.Requests;
using HypermediaEngine.API.Public.Users.Login.Requests;
using HypermediaEngine.API.Public.Users.Register.Requests;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Users.Register.Responses
{
    public class RegisterState : Entity
    {
        public RegisterState(NancyContext context, RegisterLink request) : base(context.Request.Url.ToString(), new[] { "register" })
        {
            Properties = new Dictionary<string, object>
            {
                { "title", "Register" }
            };

            Links = new LinksFactory(context)
                            .With(new HomeLink())
                            .With(new LoginLink(), WithLink<LoginLink>.Property(x => x.Href = context.WithRedirectFor(x.Href)))
                            .With(new RegisterLink(request.Country), WithLink<RegisterLink>.Property(x => x.Href = context.WithRedirectFor(x.Href)))
                            .Build();

            Actions = new ActionsFactory(context)
                            .With(new SelectRegisterCountryAction(request.Country),
                                        WithAction<SelectRegisterCountryAction>.Field(x => x.Country)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new OriginCountriesLookup()).Build().Single()))
                            .With(new RegisterAction(request.Country), 
                                        WithAction<RegisterAction>.Property(x => x.Href = context.WithRedirectFor(x.Href)),
                                        WithAction<RegisterAction>.Field(x => x.Password).Having(x => x.Type = "password"),
                                        WithAction<RegisterAction>.Field(x => x.ConfirmPassword).Having(x => x.Type = "password"))
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .Build();
        }
    }
}