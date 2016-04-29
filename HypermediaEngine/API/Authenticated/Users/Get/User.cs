using System.Collections.Generic;
using System.Linq;
using HypermediaEngine.API.Authenticated.Users.Claims;
using HypermediaEngine.API.Authenticated.Users.List;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

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
 
            Links = new LinksFactory(context).With(new GetUsers("Back to all users")).Build();
            Actions = new ActionsFactory(context).With(new PutClaims(user)).Build();
        }
    }
}