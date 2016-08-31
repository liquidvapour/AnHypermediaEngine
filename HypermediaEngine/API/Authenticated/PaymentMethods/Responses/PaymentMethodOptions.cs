using System.Collections.Generic;
using System.Linq;
using Domain;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.PaymentMethods.Responses
{
    public class PaymentMethodOptions : Entity
    {
        public PaymentMethodOptions(NancyContext context, IEnumerable<PaymentMethod> paymentMethods) : base(context.Request.Url.ToString(), new[] { "payment-method-options" })
        {
            var options = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("", "Select payment method") };
            options.AddRange(paymentMethods.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)));

            Properties = options.ToDictionary(x => x.Key, x => (object)x.Value);
        }
    }
}