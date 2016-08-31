using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class AddRecipientActionValidator : ApiActionValidator<AddRecipientAction>
    {
        public AddRecipientActionValidator()
        {
            RuleFor(x => x.RemittanceId).NotNull().NotEmpty();

            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleFor(x => x.Country).NotNull().NotEmpty();
            RuleFor(x => x.City).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Postcode).NotNull().NotEmpty();

            RuleFor(x => x.SendingReasonId).NotNull().NotEmpty();
        }
    }
}