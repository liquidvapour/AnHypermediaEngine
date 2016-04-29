using System;
using System.Collections.Generic;
using Nancy;

namespace HypermediaEngine.API.Public.Authentication.Responses
{
    public class LoginSuccess : LoginResponse
    {
        public LoginSuccess(NancyContext context, Guid accessToken) : base(context.Request.Url.ToString(), "success")
        {
            Properties = new Dictionary<string, object> { { "Access Token", accessToken } };
        }
    }
}