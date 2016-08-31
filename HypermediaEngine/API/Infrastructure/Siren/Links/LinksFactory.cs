using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (request.RequiresAuthentication && _context.IsUserAuthenticated() == false)
                return this;

            var link = GetSirenLink(request);

            foreach (var @override in overrides)
                @override.Apply(link);

            _links.Add(link);

            return this;
        }

        private Link GetSirenLink<T>(T request) where T : ApiLink
        {
            return new Link(request.Title, _context.GetFullUrlFor(GetHrefWithProperties(request)), request.Rel.ToArray());
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