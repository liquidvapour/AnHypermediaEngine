using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Public.Register.Requests
{
    public class PostRegisterValidator : ApiCommandValidator<PostRegister>
    {
        public PostRegisterValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().Equal(x => x.Password).WithMessage("Password and Confirm Password must match...");
        }
    }
}