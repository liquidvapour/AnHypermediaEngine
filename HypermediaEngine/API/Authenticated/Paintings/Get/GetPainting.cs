using System;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Authenticated.Paintings.Get
{
    public class GetPainting : ApiLink
    {
        public Guid Id { get; set; }

        public GetPainting(): base("Painting", "/api/paintings/{id}", new[] { "painting", "detail" }, Claim.Paintings)
        {
        }

        public GetPainting(Guid id): this()
        {
            Id = id;
        }
    }
}