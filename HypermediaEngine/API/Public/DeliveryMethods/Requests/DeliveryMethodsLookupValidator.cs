using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.DeliveryMethods.Requests
{
    public class DeliveryMethodsLookupValidator : ApiActionValidator<DeliveryMethodsLookup>
    {
        public DeliveryMethodsLookupValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
            RuleFor(x => x.DestinationCountry).NotNull().NotEmpty();
        }
    }
}