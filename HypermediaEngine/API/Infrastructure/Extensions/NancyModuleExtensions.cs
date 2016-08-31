using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace HypermediaEngine.API.Infrastructure.Extensions
{
    public static class NancyModuleExtensions
    {
        public static T BindAndValidate<T>(this NancyModule module)
        {
            var request = module.Bind<T>();

            var requestValidation = module.Validate(request);
            if (requestValidation.IsValid == false)
                throw new HypermediaEngineException(new BadRequestResponse(module.Context, requestValidation.Errors));

            return request;
        }
    }
}