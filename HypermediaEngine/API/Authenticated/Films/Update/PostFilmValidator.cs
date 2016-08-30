using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Films.Update
{
    public class PostFilmValidator : ApiActionValidator<PostFilm>
    {
        public PostFilmValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}