using Core.Persistency;
using Domain;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Public.DeliveryMethods.Requests;
using HypermediaEngine.API.Public.DeliveryMethods.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.DeliveryMethods
{
    public class DeliveryMethodModule : NancyModule
    {
        private readonly IRepository _repository;

        public DeliveryMethodModule(IRepository repository)
        {
            _repository = repository;
            Get["/api/deliveryMethods/{originCountry}/{destinationCountry}"] = _ => Handle(this.BindAndValidate<DeliveryMethodsLookup>());
        }

        private DeliveryMethodOptions Handle(DeliveryMethodsLookup request)
        {
            var deliveryMethods = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry).DeliveryMethods;
            return new DeliveryMethodOptions(Context, deliveryMethods);
        }
    }
}