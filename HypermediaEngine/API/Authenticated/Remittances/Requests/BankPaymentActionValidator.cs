using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Remittances.Requests
{
    public class BankPaymentActionValidator : ApiActionValidator<BankPaymentAction>
    {
        public BankPaymentActionValidator()
        {
            RuleFor(x => x.RemittanceId).NotNull().NotEmpty();

            RuleFor(x => x.AccountHolderName).NotNull().NotEmpty();

            RuleFor(x => x.RoutingNumber).NotNull().NotEmpty();
            RuleFor(x => x.AccountNumber).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmAccountNumber).NotNull().NotEmpty();
        }
    }
}