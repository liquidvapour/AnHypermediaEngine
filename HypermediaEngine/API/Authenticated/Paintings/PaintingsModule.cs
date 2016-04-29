using System.Linq;
using Core.Persistency;
using HypermediaEngine.API.Authenticated.Paintings.Get;
using HypermediaEngine.API.Authenticated.Paintings.List;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.Paintings
{
    public class PaintingsModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;

        public PaintingsModule(IRepository repository)
        {
            _repository = repository;

            Get["/api/paintings"] = _ => this.Query<GetPaintings, PaintingsCollection>(Handle);
            Get["/api/paintings/{id}"] = _ => this.Query<GetPainting, Painting>(Handle);
        }

        private PaintingsCollection Handle(GetPaintings request)
        {
            var paintings = _repository.List<Domain.Painting>().OrderBy(x => x.CreatedOn);
            return new PaintingsCollection(Context, paintings);
        }

        private Painting Handle(GetPainting request)
        {
            var film = _repository.SingleOrDefault<Domain.Painting>(x => x.Id == request.Id);
            if (film == null)
                throw new HypermediaEngineException(new NotFoundResponse(Context));

            return new Painting(Context, film);
        }
    }
}