using System.Collections.Generic;
using System.Linq;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using Nancy;

namespace HypermediaEngine.API.Authenticated.Users.List
{
    public class UsersCollection: Entity
    {
        public UsersCollection(NancyContext context, IEnumerable<Domain.User> users) : base(context.Request.Url.ToString(), new[] { "users", "collection" })
        {
            int pageNumber = context.Request.Query.PageNumber;
            int pageSize = context.Request.Query.PageSize;
            int totalEntries = users.Count();

            var usersPage = users.Skip(pageNumber * pageSize).Take(pageSize);

            Properties = new Dictionary<string, object> { { "Page Details", PagedProperties.GetPageDetails(pageNumber, pageSize, totalEntries) } };
            Entities = new List<Entity>(usersPage.Select(x => new UsersCollectionItem(context, x)));
            Links = new LinksFactory(context).WithPage<GetUsers>(pageNumber, pageSize, totalEntries).Build();
        }
    }
}