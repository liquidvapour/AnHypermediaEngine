using System.Linq;
using Core.Persistency;
using Domain;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Public.PayoutNetworks.Requests;
using HypermediaEngine.API.Public.PayoutNetworks.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.PayoutNetworks
{
    public class PayoutNetworksModule : NancyModule
    {
        private readonly IRepository _repository;

        public PayoutNetworksModule(IRepository repository)
        {
            _repository = repository;
            Get["/api/payoutNetworks/{originCountry}/{destinationCountry}/{deliveryMethodId}"] = _ => Handle(this.BindAndValidate<PayoutNetworksLookup>());
        }

        private PayoutNetworksOptions Handle(PayoutNetworksLookup request)
        {
            var payoutNetworks = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry)
                                            .DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId).PayoutNetworks;

            return new PayoutNetworksOptions(Context, payoutNetworks);
        }
    }
}