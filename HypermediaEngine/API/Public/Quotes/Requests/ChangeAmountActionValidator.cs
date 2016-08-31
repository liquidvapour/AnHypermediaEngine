using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Quotes.Requests
{
    public class ChangeAmountActionValidator : ApiActionValidator<ChangeAmountAction>
    {
        public ChangeAmountActionValidator()
        {
            RuleFor(x => x.OriginCountry).NotNull().NotEmpty();
            RuleFor(x => x.DestinationCountry).NotNull().NotEmpty();

            RuleFor(x => x.DeliveryMethodId).NotNull().NotEmpty();

            RuleFor(x => x.Currency).NotNull().NotEmpty();
            RuleFor(x => x.Amount).NotNull();
        }
    }
}