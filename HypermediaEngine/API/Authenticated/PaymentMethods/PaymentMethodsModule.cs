using Core.Persistency;
using Domain;
using HypermediaEngine.API.Authenticated.PaymentMethods.Requests;
using HypermediaEngine.API.Authenticated.PaymentMethods.Responses;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.PaymentMethods
{
    public class PaymentMethodsModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;

        public PaymentMethodsModule(IRepository repository)
        {
            _repository = repository;
            Get["/api/paymentMethods"] = _ => Handle(new PaymentMethodsLookup());
        }

        private dynamic Handle(PaymentMethodsLookup request)
        {
            var paymentMethods = _repository.List<PaymentMethod>();
            return new PaymentMethodOptions(Context, paymentMethods);
        }
    }
}