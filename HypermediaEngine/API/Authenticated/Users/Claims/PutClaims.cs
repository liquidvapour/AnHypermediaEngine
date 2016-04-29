using System;
using System.Collections.Generic;
using System.Linq;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Authenticated.Users.Claims
{
    public class PutClaims : ApiCommand 
    {
        public Guid Id { get; set; }
        public string[] Claims { get; set; }
        public IList<KeyValuePair<string, string>> ClaimsOptions
        {
            get
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