using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Countries.Destination.Requests
{
    public class DestinationCountriesLookupValidator : ApiActionValidator<DestinationCountriesLookup>
    {
        public DestinationCountriesLookupValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
        }
    }
}