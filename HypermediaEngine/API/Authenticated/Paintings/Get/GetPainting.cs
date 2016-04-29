using System;
using System.Collections.Generic;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Requests.Queries;

namespace HypermediaEngine.API.Authenticated.Paintings.Get
{
    public class GetPainting : ApiQuery
    {
        public Guid Id { get; set; }

        public GetPainting(): base("Painting", "/api/painting/{id}", Claim.Paintings, new[] { "painting" })
        {
        }

        public GetPainting(Guid id, IList<string> rel): base("Painting", "/api/paintings/{id}", Claim.Paintings, rel)
        {
            Id = id;
        }
    }
}