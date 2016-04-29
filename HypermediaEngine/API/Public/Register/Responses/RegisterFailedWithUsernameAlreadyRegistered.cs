using System.Collections.Generic;
using Hypermedia.Siren;
using HypermediaEngine.API.Public.Register.Requests;
using Nancy;

namespace HypermediaEngine.API.Public.Register.Responses
{
    public class RegisterFailedWithUsernameAlreadyRegistered: Entity
    {
        public RegisterFailedWithUsernameAlreadyRegistered(NancyContext context, PostRegister request) : base(context.Request.Url.ToString(), new[] { "register", "error", "conflict"})
        {
            Properties = new Dictionary<string, object> { { "Error Message", string.Format("Username \"{0}\" is already registered...", request.Username) } };
        }
    }
}