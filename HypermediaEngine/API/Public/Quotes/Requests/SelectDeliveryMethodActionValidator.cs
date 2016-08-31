using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class SelectDeliveryMethodActionValidator : ApiActionValidator<SelectDeliveryMethodAction>
    {
        public SelectDeliveryMethodActionValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
            RuleFor(x => x.DestinationCountry).NotNull().NotEmpty();

            RuleFor(x => x.DeliveryMethodId).NotNull().NotEmpty();
        }
    }
}