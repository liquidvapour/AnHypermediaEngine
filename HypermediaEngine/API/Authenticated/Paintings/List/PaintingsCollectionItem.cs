using System.Collections.Generic;
using HypermediaEngine.API.Authenticated.Paintings.Get;
using HypermediaEngine.API.Infrastructure.Siren.Links;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.Paintings.List
{
    public class PaintingsCollectionItem: Entity
    {
        public PaintingsCollectionItem(NancyContext context, Domain.Painting painting)
            : base(string.Format("{0}{1}{2}/{3}", context.Request.Url.SiteBase, context.Request.Url.BasePath, context.Request.Url.Path, painting.Id), "painting")
        {
            Properties = new Dictionary<string, object>
            {
                { "Thumbnail", painting.ImageThumbnail },
                { "Name", painting.Name }
            };
                
            Links = new LinksFactory(context).With(new GetPainting(painting.Id)).Build();
        }
    }
}