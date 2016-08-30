using System;
using System.Collections.Generic;
using System.Linq;
using Core.Primitives;
using HypermediaEngine.API.Authenticated.Users.Claims;
using HypermediaEngine.API.Authenticated.Users.List;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Users.Get
{
    public class User: Entity
    {
        public User(NancyContext context, Domain.User user) : base(context.Request.Url.ToString(), "user")
        {
            Properties = new Dictionary<string, object>
            {
                { "Username", user.Username },
                { "Claims", string.Join(", ", user.Claims.Select(x => x.ToString()).ToList()) }
            };

            Links = new LinksFactory(context).With(new GetUsers(), WithLink<GetUsers>.Property(x => x.Title = "Back to all users")).Build();

            Actions = new ActionsFactory(context).With(new PutClaims(user), 
                                                                WithAction<PutClaims>.Field(x => x.Claims)
                                                                    .Having(x => x.Type = "select")
                                                                    .Having(x => x.Options = GetClaimsOptions())).Build();
        }

        public IList<KeyValuePair<string, string>> GetClaimsOptions()
        {
            var result = new List<KeyValuePair<string, string>>();

            foreach (Claim claim in Enum.GetValues(typeof(Claim)))
            {
                if (claim == Claim.Public)
                    continue;

                result.Add(new KeyValuePair<string, string>(claim.ToString(), claim.ToString()));
            }

            return result;
        }
    }
}