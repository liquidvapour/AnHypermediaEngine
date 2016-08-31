using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class SelectOriginCountryActionValidator : ApiActionValidator<SelectOriginCountryAction>
    {
        public SelectOriginCountryActionValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
        }
    }
}