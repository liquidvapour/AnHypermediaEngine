using System.Collections.Generic;
using System.Linq;
using Domain;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Authenticated.SendingReasons.Responses
{
    public class SendingReasonOptions : Entity
    {
        public SendingReasonOptions(NancyContext context, IEnumerable<SendingReason> sendingReasons) : base(context.Request.Url.ToString(), new[] { "sending-reason-options" })
        {
            var options = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("", "Select sending reason") };
            options.AddRange(sendingReasons.Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name)));

            Properties = options.ToDictionary(x => x.Key, x => (object)x.Value);
        }
    }
}