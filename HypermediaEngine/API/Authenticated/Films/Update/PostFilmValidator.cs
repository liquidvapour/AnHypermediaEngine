using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Authenticated.Films.Update
{
    public class PostFilmValidator : ApiCommandValidator<PostFilm>
    {
        public PostFilmValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}