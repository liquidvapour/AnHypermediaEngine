using System.Collections.Generic;
using System.Linq;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using HypermediaEngine.API.Public.Languages.Requests;
using HypermediaEngine.API.Public.Quotes.Requests;
using HypermediaEngine.API.Public.Users;
using HypermediaEngine.API.Public.Users.Login.Requests;
using HypermediaEngine.API.Public.Users.Register.Requests;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Infrastructure.Errors.Responses
{
    public class UnauthorisedResponse : Entity
    {
        public UnauthorisedResponse(NancyContext context) : this(context, new[] { "login" })
        {
        }

        protected UnauthorisedResponse(NancyContext context, IList<string> @class) : base(context.Request.Url.ToString(), @class)
        {
            Links = new LinksFactory(context)
                            .With(new HomeLink())
                            .With(new LoginLink(), WithLink<LoginLink>.Property(x => x.Href = context.WithRedirectFor(x.Href)))
                            .With(new RegisterLink(), WithLink<RegisterLink>.Property(x => x.Href = context.WithRedirectFor(x.Href)))
                            .Build();

            Actions = new ActionsFactory(context)
                            .With(new LoginAction(), 
                                        WithAction<LoginAction>.Property(x => x.Href = context.WithRedirectFor(x.Href)),
                                        WithAction<LoginAction>.Field(x => x.Password).Having(x => x.Type = "password"))
                            .With(new ChangeLanguageAction("en-GB"),
                                        WithAction<ChangeLanguageAction>.Field(x => x.Language)
                                            .Having(x => x.Type = "select")
                                            .Having(x => x.OptionsLookup = new ActionsFactory(context).With(new LanguagesLookup()).Build().Single()))
                            .Build();
        }
    }
}