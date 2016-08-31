using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class SelectRecipientActionValidator : ApiActionValidator<SelectRecipientAction>
    {
        public SelectRecipientActionValidator()
        {
            RuleFor(x => x.RemittanceId).NotNull().NotEmpty();
        }
    }
}