using System.Collections.Generic;
using System.Linq;
using Domain;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.DeliveryMethods.Responses
{
    public class DeliveryMethodOptions : Entity
    {
        public DeliveryMethodOptions(NancyContext context, IEnumerable<DeliveryMethod> deliveryMethods) : base(context.Request.Url.ToString(), new[] { "delivery-method-options" })
        {
            var options = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("", "Select delivery method") };
            options.AddRange(deliveryMethods.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Type.Name)));

            Properties = options.ToDictionary(x => x.Key, x => (object)x.Value);
        }
    }
}