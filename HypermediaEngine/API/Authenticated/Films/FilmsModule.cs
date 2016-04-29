using System.Linq;
using Core.Persistency;
using HypermediaEngine.API.Authenticated.Films.Get;
using HypermediaEngine.API.Authenticated.Films.List;
using HypermediaEngine.API.Authenticated.Films.Update;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.Films
{
    public class FilmsModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;

        public FilmsModule(IRepository repository)
        {
            _repository = repository;

            Get["/api/films"] = _ => this.Query<GetFilms, FilmsCollection>(Handle);
            Get["/api/films/{id}"] = _ => this.Query<GetFilm, Film>(Handle);

            Post["/api/films/{id}"] = _ => this.Command<PostFilm, PostFilmSuccess>(Handle);
        }

        private FilmsCollection Handle(GetFilms request)
        {
            var films = _repository.List<Domain.Film>().OrderBy(x => x.CreatedOn);
            return new FilmsCollection(Context, films);
        }

        private Film Handle(GetFilm request)
        {
            var film = _repository.SingleOrDefault<Domain.Film>(x => x.Id == request.Id);
            if (film == null)
                throw new HypermediaEngineException(new NotFoundResponse(Context));

            return new Film(Context, film);
        }

        private PostFilmSuccess Handle(PostFilm request)
        {
            var film = _repository.SingleOrDefault<Domain.Film>(x => x.Id == request.Id);
            film.UpdateName(request.Name);
            _repository.SaveOrUpdate(film);

            return new PostFilmSuccess(Context);
        }
    }
}