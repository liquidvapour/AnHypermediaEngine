using System;
using System.Linq;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Users.Claims
{
    public class PutClaims : ApiAction 
    {
        public Guid Id { get; set; }
        public string[] Claims { get; set; }
        

        public PutClaims() : base("Update", "PUT", "/api/users/{id}/claims", Claim.Administrator)
        {
        }

        public PutClaims(Domain.User user) : base("Update", "PUT", "/api/users/{id}/claims", Claim.Administrator)
        {
            Id = user.Id;
            Claims = user.Claims.Select(x => x.ToString()).ToArray();
        }
    }
}