using System;
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

        public static bool IsUserAuthenticated(this NancyContext context)
        {
            var user = context.GetUser();
            if (user == null)
                return false;

            return true;
        }


        public static string GetFullUrlFor(this NancyContext context, string endpoint)
        {
            var url = context.Request.Url.Scheme + "://" + context.Request.Url.HostName + context.Request.Url.BasePath + endpoint;

            string accessToken = context.Request.Query["accessToken"].Value;
            if (string.IsNullOrWhiteSpace(accessToken))
                return url;

            if (url.Contains("?"))
                return url + "&accessToken=" + accessToken;

            return url + "?accessToken=" + accessToken;
        }

        public static string GetFullUrlFor(this NancyContext context, Guid accessToken)
        {
            var urlEncodedAccessToken = HttpUtility.UrlEncode(accessToken.ToString());

            string returnUrl = context.Request.Query["returnUrl"].Value;
            if (string.IsNullOrWhiteSpace(returnUrl))
                return context.GetFullUrlFor("/api" + "?accessToken=" + urlEncodedAccessToken);

            if (returnUrl.Contains("?"))
                return returnUrl + "&accessToken=" + urlEncodedAccessToken;

            return returnUrl + "?accessToken=" + urlEncodedAccessToken;
        }

        public static string WithRedirectFor(this NancyContext context, string href)
        {
            if (href.Contains("returnUrl="))
                return href;

            var redirectUrlParameter = "?returnUrl=";
            if (href.Contains("?"))
                redirectUrlParameter = "&returnUrl=";

            string returnUrl = context.Request.Query["returnUrl"].Value;
            if (string.IsNullOrWhiteSpace(returnUrl) == false)
                return href + redirectUrlParameter + HttpUtility.UrlEncode(returnUrl);

            return href + redirectUrlParameter + HttpUtility.UrlEncode(GetRequestUrl(context));
        }

        private static string GetRequestUrl(this NancyContext context)
        {
            return context.Request.Url.Scheme + "://" + context.Request.Url.HostName + context.Request.Url.BasePath + context.Request.Url.Path + context.Request.Url.Query;
        }
    }
}