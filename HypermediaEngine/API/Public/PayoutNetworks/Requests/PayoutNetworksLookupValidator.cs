using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.PayoutNetworks.Requests
{
    public class PayoutNetworksLookupValidator : ApiActionValidator<PayoutNetworksLookup>
    {
        public PayoutNetworksLookupValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
            RuleFor(x => x.DestinationCountry).NotNull().NotEmpty();

            RuleFor(x => x.DeliveryMethodId).NotNull().NotEmpty();
        }
    }
}