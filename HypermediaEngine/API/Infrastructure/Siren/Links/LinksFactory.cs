using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Requests.Links;
using Nancy;
using Nancy.Helpers;
using Siren;
using Siren.Extensions;

namespace HypermediaEngine.API.Infrastructure.Siren.Links
{
    public class LinksFactory
    {
        private readonly NancyContext _context;
        private readonly IList<Link> _links;

        public LinksFactory(NancyContext context)
        {
            _context = context;
            _links = new List<Link>();
        }

        public IList<Link> Build()
        {
            return _links;
        }

        public LinksFactory With<T>(T request, params WithLink<T>[] overrides) where T : ApiLink
        {
            if (request.Claim != Claim.Public)
                if (_context.HasUserClaim(request.Claim) == false)
                    return this;

            Link link;

            if (request is ApiLinkPaged)
                link = GetPagedSirenLink(request);
            else
                link = GetSirenLink(request);

            foreach (var @override in overrides)
                @override.Apply(link);

            _links.Add(link);

            return this;
        }

        public LinksFactory WithPage<T>(int pageNumber, int pageSize, int totalEntries) where T : ApiLinkPaged, new()
        {
            if (pageNumber > 0)
                With(new T { PageNumber = pageNumber - 1, PageSize = pageSize }, WithLink<T>.Property(x => x.Title = "Previous"));

            double followingNumberOfPages = (totalEntries / pageSize) - pageNumber;
            if (followingNumberOfPages > 0.0d)
                With(new T { PageNumber = pageNumber + 1, PageSize = pageSize }, WithLink<T>.Property(x => x.Title = "Next"));

            return this;
        }

        private Link GetSirenLink<T>(T request) where T : ApiLink
        {
            return new Link(request.Title, _context.Request.Url.SiteBase + _context.Request.Url.BasePath + GetHrefWithProperties(request), request.Rel.ToArray());
        }

        private Link GetPagedSirenLink<T>(T request) where T : ApiLink
        {
            return new Link(request.Title, _context.Request.Url.SiteBase + _context.Request.Url.BasePath + GetHrefWithProperties(request), new List<string>(request.Rel) { "collection" }.ToArray());
        }

        private string GetHrefWithProperties<T>(T request) where T : ApiLink
        {
            var href = request.Href;

            var properties = typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (!property.CanRead)
                    continue;

                var propertyGetMethod = property.GetGetMethod(false);
                var propertySetMethod = property.GetSetMethod(false);
                if (propertyGetMethod == null || propertySetMethod == null)
                    continue;

                var value = property.GetValue(request, null);
                if (value != null && value.Equals(DefaultOf(property.PropertyType)))
                    value = null;
                
                var propertyName = property.Name.FormatAsName();
                var propertyValueTemplate = "{" + propertyName + "}";

                if (href.Contains(propertyValueTemplate))
                {
                    if (value != null)
                    {
                        href = href.Replace(propertyValueTemplate, HttpUtility.UrlEncode(value.ToString()));
                    }
                    else
                    {
                        href = href.Replace("&" + propertyName + "=" + propertyValueTemplate, "")
                                   .Replace(propertyName + "=" + propertyValueTemplate, "");
                    }
                }
                else
                {
                    if (value != null)
                    {
                        if (href.Contains("?"))
                            href += "&";
                        else
                            href += "?";

                        href += propertyName + "=" + HttpUtility.UrlEncode(value.ToString());
                    }
                }

            }

            return href;
        }

        private object DefaultOf(Type t)
        {
            return this.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
        }

        public T GetDefaultGeneric<T>()
        {
            return default(T);
        }
    }
}