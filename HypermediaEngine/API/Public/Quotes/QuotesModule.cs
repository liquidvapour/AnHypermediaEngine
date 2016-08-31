using System.Linq;
using Core.Persistency;
using Domain;
using Domain.Factories;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Public.Quotes.Requests;
using HypermediaEngine.API.Public.Quotes.Requests.PayoutNetworks;
using HypermediaEngine.API.Public.Quotes.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Quotes
{
    public class QuotesModule : NancyModule
    {
        private readonly IRepository _repository;
        private readonly IQuoteFactory _quoteFactory;

        public QuotesModule(IRepository repository, IQuoteFactory quoteFactory)
        {
            _repository = repository;
            _quoteFactory = quoteFactory;

            Get["/api"] = _ => Handle(new SelectOriginCountryAction("gb"));

            Get["/api/quote"] = _ => Handle(this.BindAndValidate<SelectOriginCountryAction>());
            Get["/api/quote/{originCountry}"] = _ => Handle(this.BindAndValidate<SelectDestinationCountryAction>());
            Get["/api/quote/{originCountry}/{destinationCountry}"] = _ => Handle(this.BindAndValidate<SelectDeliveryMethodAction>());

            Get["/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}"] = _ => Handle(this.BindAndValidate<ChangeAmountAction>());
            
            Get["/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}/payoutNetworks"] = _ => Handle(this.BindAndValidate<SelectPayoutNetworkAction>());
            Get["/api/quote/{originCountry}/{destinationCountry}/{deliveryMethodId}/payoutNetworks/{payoutNetworkId}"] = _ => Handle(this.BindAndValidate<ChangeAmountForPayoutNetworkAction>());
        }


        private dynamic Handle(SelectOriginCountryAction request)
        {
            var corridor = _repository.List<Corridor>(x => x.Origin.Code == request.OriginCountry).First();
            return new QuoteState(Context, corridor.Origin);
        }

        private dynamic Handle(SelectDestinationCountryAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            return new QuoteState(Context, corridor.Origin, corridor.Destination);
        }

        private dynamic Handle(SelectDeliveryMethodAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            var deliveryMethod = corridor.DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId);

            return new QuoteState(Context, corridor.Origin, corridor.Destination, deliveryMethod);
        }
        

        private dynamic Handle(ChangeAmountAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            var deliveryMethod = corridor.DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId);

            var quote = _quoteFactory.For(corridor, deliveryMethod, request.Currency, request.Amount);

            return new QuoteState(Context, corridor.Origin, corridor.Destination, deliveryMethod, quote);
        }


        private dynamic Handle(SelectPayoutNetworkAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            var deliveryMethod = corridor.DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId);
            var payoutNetwork = deliveryMethod.PayoutNetworks.Single(x => x.Id == request.PayoutNetworkId);

            return new QuoteState(Context, corridor.Origin, corridor.Destination, deliveryMethod, payoutNetwork);
        }

        private dynamic Handle(ChangeAmountForPayoutNetworkAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            var deliveryMethod = corridor.DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId);
            var payoutNetwork = deliveryMethod.PayoutNetworks.Single(x => x.Id == request.PayoutNetworkId);

            var quote = _quoteFactory.For(corridor, deliveryMethod, request.Currency, request.Amount);

            return new QuoteState(Context, corridor.Origin, corridor.Destination, deliveryMethod, payoutNetwork, quote);
        }
    }
}