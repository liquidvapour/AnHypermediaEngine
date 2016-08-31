using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Register.Requests
{
    public class RegisterActionValidator : ApiActionValidator<RegisterAction>
    {
        public RegisterActionValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().Equal(x => x.Password).WithMessage("Password and Confirm Password must match...");
        }
    }
}