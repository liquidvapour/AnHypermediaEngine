using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.PaymentMethods.Requests
{
    public class PaymentMethodsLookup : ApiAction
    {
        public PaymentMethodsLookup() : base("Payment Method Lookup", "GET", "/api/paymentMethods")
        {
        }
    }
}