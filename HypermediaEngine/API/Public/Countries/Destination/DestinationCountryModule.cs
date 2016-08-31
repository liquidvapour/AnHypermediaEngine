using System.Linq;
using Core.Persistency;
using Domain;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Public.Countries.Destination.Requests;
using HypermediaEngine.API.Public.Countries.Destination.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Countries.Destination
{
    public class DestinationCountryModule : NancyModule
    {
        private readonly IRepository _repository;

        public DestinationCountryModule(IRepository repository)
        {
            _repository = repository;
            Get["/api/countries/{originCountry}"] = _ => Handle(this.BindAndValidate<DestinationCountriesLookup>());
        }

        private DestinationCountryOptions Handle(DestinationCountriesLookup request)
        {
            var destinationCountries = _repository.List<Corridor>(x => x.Origin.Code == request.OriginCountry).Select(x => x.Destination);
            return new DestinationCountryOptions(Context, destinationCountries);
        }
    }
}