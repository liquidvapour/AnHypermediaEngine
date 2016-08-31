using System.Collections.Generic;
using System.Linq;
using Domain;
using HypermediaEngine.API.Authenticated.Remittances.Requests;
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
    public class RemittancesListState : Entity
    {
        public RemittancesListState(NancyContext context, IEnumerable<Remittance> remitances) : base(context.Request.Url.ToString(), new[] { "remittances-list" })
        {
            Properties = new Dictionary<string, object>
            {
                { "title", "Remittances" }
            };

            Entities = new List<Entity>(remitances.Select(x => new RemittancesListItemState(context, x)));

            Links = new LinksFactory(context)
                            .With(new HomeLink())
                            .With(new ListRemittances())
                            .Build();

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