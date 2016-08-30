using System;
using System.Linq;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Users;
using Nancy;
using Nancy.Helpers;

namespace HypermediaEngine.API.Infrastructure.Extensions
{
    public static class NancyContextExtensions
    {
        public static UserIdentity GetUser(this NancyContext context)
        {
            return context.CurrentUser as UserIdentity;
        }

        public static bool HasUserClaim(this NancyContext context, Claim claim)
        {
            var user = context.GetUser();
            if (user == null)
                return false;

            return user.Claims.Contains(claim.ToString());
        }


        public static string GetFullUrlFor(this NancyContext context, string endpoint)
        {
            return context.Request.Url.Scheme + "://" + context.Request.Url.HostName + context.Request.Url.BasePath + endpoint;
        }

        public static string WithRedirectFor(this NancyContext context, string href)
        {
            if (href.Contains("returnUrl="))
                return string.Empty;

            var redirectUrlParameter = "?returnUrl=";
            if (href.Contains("?"))
                redirectUrlParameter = "&returnUrl=";

            string returnUrl = context.Request.Query["returnUrl"].Value;
            if (string.IsNullOrWhiteSpace(returnUrl) == false)
                return redirectUrlParameter + HttpUtility.UrlEncode(returnUrl);

            return redirectUrlParameter + HttpUtility.UrlEncode(GetRequestUrl(context));
        }

        private static string GetRequestUrl(this NancyContext context)
        {
            return context.Request.Url.Scheme + "://" + context.Request.Url.HostName + context.Request.Url.BasePath + context.Request.Url.Path + context.Request.Url.Query;
        }
    }
}