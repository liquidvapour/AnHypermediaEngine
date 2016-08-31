using System.Collections.Generic;
using System.Linq;
using Domain;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.PayoutNetworks.Responses
{
    public class PayoutNetworksOptions : Entity
    {
        public PayoutNetworksOptions(NancyContext context, IEnumerable<PayoutNetwork> payoutNetworks) : base(context.Request.Url.ToString(), new[] { "payout-network-options" })
        {
            var options = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("", "Select payout network") };
            options.AddRange(payoutNetworks.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)));

            Properties = options.ToDictionary(x => x.Key, x => (object)x.Value);
        }
    }
}