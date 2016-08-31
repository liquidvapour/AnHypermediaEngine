using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class SelectDestinationCountryActionValidator : ApiActionValidator<SelectDestinationCountryAction>
    {
        public SelectDestinationCountryActionValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
            RuleFor(x => x.DestinationCountry).NotNull().NotEmpty();
        }
    }
}