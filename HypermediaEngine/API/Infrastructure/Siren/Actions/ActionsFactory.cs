using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Requests.Actions;
using Nancy;
using Nancy.Helpers;
using Siren;
using Siren.Extensions;
using Action = Siren.Action;

namespace HypermediaEngine.API.Infrastructure.Siren.Actions
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

        public ActionsFactory With<T>(T request, params IWithAction<T>[] overrides) where T : ApiAction
        {
            if (request.RequiresAuthentication && _context.IsUserAuthenticated() == false)
                return this;

            var href = request.Href;

            IList<Field> fields = null;

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
                    if (fields == null)
                        fields = new List<Field>();

                    var fieldType = "string";
                    if (property.PropertyType == typeof(decimal))
                        fieldType = "number";

                    var field = new Field(property.Name, fieldType, value, IsRequired<T>(property.Name));

                    var fieldOverrides = overrides.Where(x => x is IHavingActionFieldOverride<T>).Cast<IHavingActionFieldOverride<T>>().SingleOrDefault(x => x.GetFieldName() == property.Name);
                    if (fieldOverrides != null)
                        fieldOverrides.Apply(field);

                    fields.Add(field);
                }
            }

            var action = new Action(request.Title, request.Method, _context.GetFullUrlFor(href), fields);
            var actionOverrides = overrides.Where(x => x is IHavingActionOverride<T>).Cast<IHavingActionOverride<T>>();
            foreach (var @override in actionOverrides)
                @override.Apply(action);

            _actions.Add(action);

            return this;
        }

        private static bool IsRequired<T>(string propertyName) where T : ApiAction
        {
            var isRequired = false;

            var validatorType = AppDomain.CurrentDomain.GetAssemblies()
                                            .SelectMany(s => s.GetTypes())
                                            .SingleOrDefault(p => typeof(ApiActionValidator<T>).IsAssignableFrom(p));

            if (validatorType == null)
                return false;

            var requestValidator = (ApiActionValidator<T>)Activator.CreateInstance(validatorType);
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
