using System;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Users.Get
{
    public class GetUser : ApiLink
    {
        public Guid Id { get; set; }

        public GetUser(): base("User", "/api/users/{id}", new[] { "user", "detail" }, Claim.Authenticated)
        {
        }

        public GetUser(Guid id): this()
        {
            Id = id;
        }
    }
}