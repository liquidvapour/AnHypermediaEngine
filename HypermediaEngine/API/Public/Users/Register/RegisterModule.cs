using Core.Persistency;
using Domain.Users;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;
using HypermediaEngine.API.Public.Users.Register.Requests;
using HypermediaEngine.API.Public.Users.Register.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Users.Register
{
    public class RegisterModule : SecureApiModule
    {
        private readonly IRepository _repository;

        public RegisterModule(IRepository repository)
        {
            _repository = repository;

            Get["/api/register"] = _ => Handle(this.BindAndValidate<RegisterLink>());
            Post["/api/register/{country}"] = _ => Handle(this.BindAndValidate<RegisterAction>());
        }

        private dynamic Handle(RegisterLink request)
        {
            return new RegisterState(Context, request);
        }

        private dynamic Handle(RegisterAction request)
        {
            if (_repository.Any<Sender>(x => x.Username == request.Username))
                throw new HypermediaEngineException(new RegisterActionFailedWithUsernameAlreadyRegistered(Context, request));

            var user = new Sender(request.Username, request.Password);
            var accessToken = user.IssueAccessToken();

            _repository.SaveOrUpdate(user);

            return Response.AsRedirect(Context.GetFullUrlFor(accessToken));
        }
    }
}