using System;
using System.Collections.Generic;
using Nancy.Security;

namespace HypermediaEngine.API.Infrastructure.Users
{
    public class UserIdentity : IUserIdentity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        public Guid AccessToken { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}