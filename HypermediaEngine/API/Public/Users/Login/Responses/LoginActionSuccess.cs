using System;
using System.Collections.Generic;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Users.Login.Responses
{
    public class LoginActionSuccess : Entity
    {
        public LoginActionSuccess(NancyContext context, Guid accessToken) : base(context.Request.Url.ToString(), new [] { "login", "success" })
        {
            Properties = new Dictionary<string, object> { { "Access Token", accessToken } };
        }
    }
}