using System;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ModelBinding;

namespace HypermediaEngine.API.Infrastructure.Errors
{
    public class ErrorHandler : IStatusCodeHandler
    {
        private readonly IResponseFormatterFactory _responseFormatterFactory;

        public ErrorHandler(IResponseFormatterFactory responseFormatterFactory)
        {
            _responseFormatterFactory = responseFormatterFactory;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.MethodNotAllowed;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var formatter = _responseFormatterFactory.Create(context);

            context.Response.StatusCode = HttpStatusCode.OK;

            object errorObject;
            context.Items.TryGetValue(NancyEngine.ERROR_EXCEPTION, out errorObject);
            var exception = errorObject as Exception;

            if (exception != null)
            {
                var managedException = exception.InnerException as HypermediaEngineException;
                if (managedException != null)
                {
                    context.Response = formatter.AsJson(managedException.Response).WithContentType("application/vnd.siren+json; charset=utf-8");
                    return;
                }

                var bindingException = exception.InnerException as ModelBindingException;
                if (bindingException != null)
                {
                    context.Response = formatter.AsJson(new BadRequestResponse(context, bindingException.PropertyBindingExceptions)).WithContentType("application/vnd.siren+json; charset=utf-8");
                    return;
                }
            }

            context.Response = formatter.AsJson(new InternalServerErrorResponse(context)).WithContentType("application/vnd.siren+json; charset=utf-8");
        }
    }
}