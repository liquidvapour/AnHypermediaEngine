using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Primitives;
using Hypermedia.Siren.Actions.Fields;
using Hypermedia.Siren.Actions.Fields.Hidden;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Requests.Commands;
using Nancy;
using Action = Hypermedia.Siren.Actions.Action;

namespace HypermediaEngine.API.Infrastructure.Siren
{
    public class ActionsFactory
    {
        private readonly NancyContext _context;
        private readonly IList<Action> _actions;

        public ActionsFactory(NancyContext context)
        {
            _context = context;
            _actions = new List<Action>();
        }

        public IList<Action> Build()
        {
            return _actions;
        }

        public ActionsFactory With<T>(T request) where T : ApiCommand
        {
            if (request.Claim != Claim.Public)
                if (_context.HasUserClaim(request.Claim) == false)
                    return this;

            var href = request.Href;
            var fields = new List<Field>();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (!property.CanRead)
                    continue;

                var propertyGetMethod = property.GetGetMethod(false);
                var propertySetMethod = property.GetSetMethod(false);
                if (propertyGetMethod == null || propertySetMethod == null)
                    continue;

                var value = property.GetValue(request, null);
                var propertyName = ToCamelCase(property.Name);
                var propertyValueTemplate = "{" + propertyName + "}";

                if (href.Contains(propertyValueTemplate))
                {
                    href = href.Replace(propertyValueTemplate, value.ToString());
                }
                else
                {
                    Field field;

                    if (property.PropertyType == typeof (Guid))
                    {
                        field = new HiddenStringField(ToCamelCase(property.Name), value as string, true);
                    }
                    else if (property.PropertyType == typeof (string[]))
                    {
                        var options = properties.Single(x => x.Name == property.Name + "Options").GetValue(request) as IList<KeyValuePair<string, string>>;
                        field = new MultiSelectField(property.Name, ToCamelCase(property.Name), options, value as string[], IsRequired<T>(property.Name));
                    }
                    else if (property.Name.ToLower().Contains("password"))
                    {
                        field = new PasswordField(property.Name, ToCamelCase(property.Name), value as string, IsRequired<T>(property.Name));
                    }
                    else
                    {
                        field = new StringField(property.Name, ToCamelCase(property.Name), value as string, IsRequired<T>(property.Name));
                    }

                    fields.Add(field);
                }
            }

            _actions.Add(new Action(request.Title, ToCamelCase(request.Title), request.Method, _context.Request.Url.SiteBase + _context.Request.Url.BasePath + href, fields));

            return this;
        }

        private static bool IsRequired<T>(string propertyName) where T : ApiCommand
        {
            var isRequired = false;

            var validatorType = AppDomain.CurrentDomain.GetAssemblies()
                                            .SelectMany(s => s.GetTypes())
                                            .SingleOrDefault(p => typeof(ApiCommandValidator<T>).IsAssignableFrom(p));

            if (validatorType == null)
                return false;

            var requestValidator = (ApiCommandValidator<T>)Activator.CreateInstance(validatorType);
            foreach (var property in requestValidator)
            {
                if ((property as FluentValidation.Internal.PropertyRule).PropertyName == propertyName)
                {
                    foreach (var propertyValidator in property.Validators)
                    {
                        if (propertyValidator.GetType() == typeof(FluentValidation.Validators.NotNullValidator))
                        {
                            isRequired = true;
                        }
                    }
                }
            }

            return isRequired;
        }

        private static string ToCamelCase(string name)
        {
            return name.First().ToString().ToLower() + string.Join("", name.Skip(1));
        }
    }
}
