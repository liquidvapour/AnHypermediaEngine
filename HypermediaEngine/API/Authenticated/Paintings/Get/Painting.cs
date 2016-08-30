using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Paintings.List;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Paintings.Get
{
    public class Painting: Entity
    {
        public Painting(NancyContext context, Domain.Painting painting) : base(context.Request.Url.ToString(), "painting")
        {
            Properties = new Dictionary<string, object>
            {
                { "Image", painting.Image },
                { "Name", painting.Name },
                { "Description", painting.Description }
            };

            Links = new LinksFactory(context).With(new GetPaintings(), WithLink<GetPaintings>.Property(x => x.Title = "Back to all paintings")).Build();
        }
    }
}