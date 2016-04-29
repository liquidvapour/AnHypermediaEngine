using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Users.Delete;
using HypermediaEngine.API.Authenticated.Users.Get;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Users.List
{
    public class UsersCollectionItem: Entity
    {
        public UsersCollectionItem(NancyContext context, Domain.User user)
            : base(string.Format("{0}{1}{2}/{3}", context.Request.Url.SiteBase, context.Request.Url.BasePath, context.Request.Url.Path, user.Id), "user")
        {
            Properties = new Dictionary<string, object>
                             {
                                 { "Username", user.Username },
                                 { "Created On", user.CreatedOn.ToShortDateString() }
                             };

            Links = new LinksFactory(context).With(new GetUser(user.Id, new[] { "user", "detail" })).Build();
            Actions = new ActionsFactory(context).With(new DeleteUser { Id = user.Id }).Build();
        }
    }
}