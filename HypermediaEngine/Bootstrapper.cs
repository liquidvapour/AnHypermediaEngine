using System;
using System.Collections.Generic;
using System.Linq;
using Core.Persistency;
using Domain.Users;
using HypermediaEngine.API.Infrastructure.Users;
using InMemoryDb;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses.Negotiation;
using Nancy.Security;
using Nancy.TinyIoc;
using Nancy.ViewEngines.Razor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace HypermediaEngine
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            InternalConfiguration.ResponseProcessors.Clear();
            InternalConfiguration.ResponseProcessors.Add(typeof(JsonProcessor));
            InternalConfiguration.ResponseProcessors.Add(typeof(RazorViewEngine));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<IRepository>(new InMemoryRepository());
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.BeforeRequest += nancyContext =>
            {
                var acceptHeaders = new List<Tuple<string, decimal>>(nancyContext.Request.Headers.Accept);
                acceptHeaders.Insert(0, new Tuple<string, decimal>("application/vnd.siren+json", 1m));

                nancyContext.Request.Headers.Accept = acceptHeaders;

                return null;
            };

            pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
            {
                x.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                x.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,PATCH,OPTIONS");
            });

            StatelessAuthentication.Enable(pipelines, new StatelessAuthenticationConfiguration(ctx =>
            {
                var statelessAuthenticator = container.Resolve<IStatelessAuthenticator>();

                var accessToken = ctx.Request.Query["accessToken"];
                if (accessToken == null)
                    return null;

                return statelessAuthenticator.GetUserIdentityBy(new Guid(accessToken));
            }));
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("Website/Views/", viewName));

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Styles", "Website/Styles"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Images", "Website/Images"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Fonts", "Website/Fonts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts", "Website/Scripts"));
        }
    }

    public sealed class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            NullValueHandling = NullValueHandling.Ignore;
            
            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = false,
                CamelCaseText = true
            });

            Formatting = Formatting.Indented;
        }
    }

    public class JsonRegistration : IRegistrations
    {
        public IEnumerable<TypeRegistration> TypeRegistrations
        {
            get
            {
                yield return new TypeRegistration(typeof(JsonSerializer), typeof(CustomJsonSerializer));
            }
        }

        public IEnumerable<CollectionTypeRegistration> CollectionTypeRegistrations { get; protected set; }
        public IEnumerable<InstanceRegistration> InstanceRegistrations { get; protected set; }
    }

    public interface IStatelessAuthenticator
    {
        IUserIdentity GetUserIdentityBy(Guid accessToken);
    }

    public class StatelessAuthenticator : IStatelessAuthenticator
    {
        private readonly IRepository _repository;

        public StatelessAuthenticator(IRepository repository)
        {
            _repository = repository;
        }

        public IUserIdentity GetUserIdentityBy(Guid accessToken)
        {
            var user = _repository.SingleOrDefault<Sender>(x => x.AccessTokens.Any(y => y == accessToken));
            if (user == null)
                return null;

            return new UserIdentity
            {
                Id = user.Id,
                UserName = user.Username,
                AccessToken = accessToken
            };
        }
    }
}