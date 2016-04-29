using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Primitives;
using Hypermedia.Siren;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Requests.Queries;
using Nancy;

namespace HypermediaEngine.API.Infrastructure.Siren
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

        public LinksFactory With<T>(T request) where T : ApiQuery
        {
            if (request.Claim != Claim.Public)
                if (_context.HasUserClaim(request.Claim) == false)
                    return this;

            if (request is ApiQueryPaged)
                _links.Add(GetPagedSirenLink(request));
            else
                _links.Add(GetSirenLink(request));

            return this;
        }

        public LinksFactory WithPage<T>(int pageNumber, int pageSize, int totalEntries) where T : ApiQueryPaged, new()
        {
            Type type = typeof(T);
            ConstructorInfo tCtor = type.GetConstructor(new[] { typeof(string), typeof(int), typeof(int) });

            if (pageNumber > 0)
                With((T) tCtor.Invoke(new object[] { "Previous", pageNumber - 1, pageSize }));

            double followingNumberOfPages = (totalEntries / pageSize) - pageNumber;
            if (followingNumberOfPages > 0.0d)
                With((T) tCtor.Invoke(new object[] { "Next", pageNumber + 1, pageSize }));

            return this;
        }

        private Link GetSirenLink<T>(T request) where T : ApiQuery
        {
            return new Link
                       {
                           Title = request.Title,
                           Href = _context.Request.Url.SiteBase + _context.Request.Url.BasePath + GetHrefWithProperties(request),
                           Rel = request.Rel.ToArray()
                       };
        }

        private Link GetPagedSirenLink<T>(T request) where T : ApiQuery
        {
            return new Link
                       {
                           Title = request.Title,
                           Href = _context.Request.Url.SiteBase + _context.Request.Url.BasePath + GetHrefWithProperties(request),
                           Rel = new List<string>(request.Rel) { "collection" }.ToArray()
                       };
        }

        private static string GetHrefWithProperties<T>(T request) where T : ApiQuery
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

                var value = property.GetValue(request, null) == null ? null : property.GetValue(request, null).ToString();
                var propertyName = ToCamelCase(property.Name);
                var propertyValueTemplate = "{" + propertyName + "}";

                if (href.Contains(propertyValueTemplate))
                {
                    href = href.Replace(propertyValueTemplate, value);
                }
                else
                {
                    if (href.Contains("?"))
                        href += "&";
                    else
                        href += "?";

                    href += propertyName + "=" + value;
                }

            }

            return href;
        }

        private static string ToCamelCase(string name)
        {
            return name.First().ToString().ToLower() + string.Join("", name.Skip(1));
        }
    }
}