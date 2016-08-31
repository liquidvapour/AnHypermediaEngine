using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.SendingReasons.Requests
{
    public class SendingReasonsLookup : ApiAction
    {
        public SendingReasonsLookup() : base("Sending Reason Lookup", "GET", "/api/sendingReasons")
        {
        }
    }
}