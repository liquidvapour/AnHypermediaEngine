using Core.Persistency;
using Domain;
using HypermediaEngine.API.Authenticated.SendingReasons.Requests;
using HypermediaEngine.API.Authenticated.SendingReasons.Responses;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.SendingReasons
{
    public class SendingReasonsModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;

        public SendingReasonsModule(IRepository repository)
        {
            _repository = repository;
            Get["/api/sendingReasons"] = _ => Handle(this.BindAndValidate<SendingReasonsLookup>());
        }

        private dynamic Handle(SendingReasonsLookup request)
        {
            var sendingReasons = _repository.List<SendingReason>();
            return new SendingReasonOptions(Context, sendingReasons);
        }
    }
}