using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Login.Requests
{
    public class LoginActionValidator : ApiActionValidator<LoginAction>
    {
        public LoginActionValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}