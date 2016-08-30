using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Siren;

namespace HypermediaEngine.API.Infrastructure.Errors.Responses
{
    public class BadRequestResponse: Entity
    {
        public BadRequestResponse(NancyContext context, IEnumerable<KeyValuePair<string, IList<ModelValidationError>>> validationErrors) : base(context.Request.Url, new[] { "error", "bad-request" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", validationErrors.First().Value.First().ErrorMessage } };
        }

        public BadRequestResponse(NancyContext context, IEnumerable<PropertyBindingException> bindingErrors) : base(context.Request.Url, new[] { "error", "bad-request" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", bindingErrors.First().Message } };
        }
    }
}