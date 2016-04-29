using System.Collections.Generic;
using Hypermedia.Siren;

namespace HypermediaEngine.API.Public.Authentication.Responses
{
    public abstract class LoginResponse: Entity
    {
        protected LoginResponse(string href, IList<string> @class) : base(href, @class)
        {
        }

        protected LoginResponse(string href, string @class) : base(href, new [] { "login", @class })
        {
        }
    }
}