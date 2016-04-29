using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Public.Authentication.Requests
{
    public class PostLoginValidator : ApiCommandValidator<PostLogin>
    {
        public PostLoginValidator()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}