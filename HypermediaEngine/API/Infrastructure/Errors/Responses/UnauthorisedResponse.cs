using System.Collections.Generic;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Public.Authentication.Requests;
using HypermediaEngine.API.Public.Register.Requests;
using HypermediaEngine.API.Public.ResetPassword.Requests;
using Nancy;
using Siren;

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
                                .With(new PostLogin(), WithAction<PostLogin>.Field(x => x.Password).Having(x => x.Type = "password"))
                                .With(new PostRegister(), 
                                                WithAction<PostRegister>.Field(x => x.Password).Having(x => x.Type = "password"),
                                                WithAction<PostRegister>.Field(x => x.ConfirmPassword).Having(x => x.Type = "password"))
                                .With(new PostResetPassword())
                                .Build();
        }
    }
}