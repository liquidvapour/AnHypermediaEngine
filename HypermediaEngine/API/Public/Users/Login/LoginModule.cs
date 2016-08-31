using Core.Persistency;
using Domain.Users;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;
using HypermediaEngine.API.Public.Users.Login.Requests;
using HypermediaEngine.API.Public.Users.Login.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Users.Login
{
    public class LoginModule : SecureApiModule
    {
        private readonly IRepository _repository;

        public LoginModule(IRepository repository)
        {
            _repository = repository;

            Get["/api/login"] = _ => Handle(this.BindAndValidate<LoginLink>());
            Post["/api/login"] = _ => Handle(this.BindAndValidate<LoginAction>());
        }

        private dynamic Handle(LoginLink request)
        {
            return new LoginState(Context, request);
        }

        private dynamic Handle(LoginAction request)
        {
            var user = _repository.SingleOrDefault<Sender>(x => x.Username.ToLower() == request.Username.ToLower());
            if (user == null || user.Validate(request.Password) == false)
                return Response.AsJson(new LoginActionFailed(Context))
                               .WithContentType("application/vnd.siren+json; charset=utf-8");

            var accessToken = user.IssueAccessToken();
            _repository.SaveOrUpdate(user);

            return Response.AsRedirect(Context.GetFullUrlFor(accessToken));
        }
    }
}