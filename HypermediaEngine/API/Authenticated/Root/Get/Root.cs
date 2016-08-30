using HypermediaEngine.API.Authenticated.Books.List;
using HypermediaEngine.API.Authenticated.Films.List;
using HypermediaEngine.API.Authenticated.Paintings.List;
using HypermediaEngine.API.Authenticated.Users.List;
using HypermediaEngine.API.Infrastructure.Siren.Actions;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using HypermediaEngine.API.Public.Authentication.Requests;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Root.Get
{
    public class Root: Entity
    {
        public Root(NancyContext context) : base(context.Request.Url.ToString(), "root")
        {
            Links = new LinksFactory(context)
                            .With(new GetUsers())
                            .With(new GetBooks())
                            .With(new GetPaintings())
                            .With(new GetFilms())
                            .Build();

            Actions = new ActionsFactory(context)
                                .With(new DeleteLogout())
                                .Build();
        }
    }
}