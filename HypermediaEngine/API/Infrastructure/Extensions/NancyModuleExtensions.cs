using System;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Requests;
using HypermediaEngine.API.Infrastructure.Requests.Actions;
using HypermediaEngine.API.Infrastructure.Requests.Links;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Siren;

namespace HypermediaEngine.API.Infrastructure.Extensions
{
    public static class NancyModuleExtensions
    {
        public static Response Query<T, TU>(this NancyModule module, Func<T, TU> requestHandler, int cachingInSeconds = 0)
            where TU: Entity
            where T : ApiLink
        {
            var request = module.BindAndValidate<T>();

            return module.Response.AsJson(requestHandler(request))
                                  .WithContentType("application/vnd.siren+json; charset=utf-8")
                                  .WithHeader("Cache-Control", string.Format("Max-Age={0}", cachingInSeconds));
        }

        public static Response Command<T, TU>(this NancyModule module, Func<T, TU> commandHandler)
            where TU: Entity
            where T : ApiAction
        {
            var command = module.BindAndValidate<T>();
            
            return module.Response.AsJson(commandHandler(command))
                                  .WithContentType("application/vnd.siren+json; charset=utf-8");
        }

        public static T BindAndValidate<T>(this NancyModule module) where T : ApiRequest
        {
            var request = module.Bind<T>();

            var requestValidation = module.Validate(request);
            if (requestValidation.IsValid == false)
                throw new HypermediaEngineException(new BadRequestResponse(module.Context, requestValidation.Errors));

            return request;
        }
    }
}