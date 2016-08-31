using System.Linq;
using Core.Persistency;
using Domain;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Public.Countries.Origin.Requests;
using HypermediaEngine.API.Public.Countries.Origin.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Countries.Origin
{
    public class OriginCountryModule : NancyModule
    {
        private readonly IRepository _repository;

        public OriginCountryModule(IRepository repository)
        {
            _repository = repository;
            Get["/api/countries"] = _ => Handle(this.BindAndValidate<OriginCountriesLookup>());
        }

        private dynamic Handle(OriginCountriesLookup request)
        {
            var originCountries = _repository.List<Corridor>().Select(x => x.Origin).Distinct().ToDictionary(x => x.Code, x => x.Name);
            return new OriginCountryOptions(Context, originCountries);
        }
    }
}