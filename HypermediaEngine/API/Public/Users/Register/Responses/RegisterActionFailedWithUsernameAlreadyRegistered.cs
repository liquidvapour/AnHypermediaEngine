using System.Collections.Generic;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Users.Register.Responses
{
    public class RegisterActionFailedWithUsernameAlreadyRegistered: Entity
    {
        public RegisterActionFailedWithUsernameAlreadyRegistered(NancyContext context, Requests.RegisterAction request) : base(context.Request.Url.ToString(), new[] { "register", "error", "conflict"})
        {
            Properties = new Dictionary<string, object> { { "Error Message", string.Format("Username \"{0}\" is already registered...", request.Username) } };
        }
    }
}