using System;
using System.Collections.Generic;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Users.Get
{
    public class GetUser : ApiQuery
    {
        public Guid Id { get; set; }

        public GetUser(): base("User", "/api/users/{id}", Claim.Administrator, new[] { "user" })
        {
        }

        public GetUser(Guid id, IList<string> rel): base("User", "/api/users/{id}", Claim.Administrator, rel)
        {
            Id = id;
        }
    }
}