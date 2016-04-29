using System.Collections.Generic;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Siren;
using HypermediaEngine.API.Public.Authentication.Requests;
using HypermediaEngine.API.Public.Register.Requests;
using HypermediaEngine.API.Public.ResetPassword.Requests;
using Nancy;

namespace HypermediaEngine.API.Infrastructure.Errors.Responses
{
    public class UnauthorisedResponse : Entity
    {
        public UnauthorisedResponse(NancyContext context) : this(context, new[] { "login", "unauthorised" })
        {
        }

        protected UnauthorisedResponse(NancyContext context, IList<string> @class) : base(context.Request.Url.ToString(), @class)
        {
            Actions = new ActionsFactory(context)
                                .With(new PostLogin())
                                .With(new PostRegister())
                                .With(new PostResetPassword())
                                .Build();
        }
    }
}