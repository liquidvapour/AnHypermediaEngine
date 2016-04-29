using HypermediaEngine.API.Infrastructure.Errors.Responses;
using Nancy;
using Nancy.ErrorHandling;

namespace HypermediaEngine.API.Infrastructure.Errors
{
    public class UnauthorisedHandler : IStatusCodeHandler
    {
        private readonly IResponseFormatterFactory _responseFormatterFactory;

        public UnauthorisedHandler(IResponseFormatterFactory responseFormatterFactory)
        {
            _responseFormatterFactory = responseFormatterFactory;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.Unauthorized;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var formatter = _responseFormatterFactory.Create(context);
            context.Response = formatter.AsJson(new UnauthorisedResponse(context)).WithContentType("application/vnd.siren+json; charset=utf-8");
        }
    }
}