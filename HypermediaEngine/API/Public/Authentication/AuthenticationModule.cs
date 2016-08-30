using Core.Persistency;
using Domain;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;
using HypermediaEngine.API.Public.Authentication.Requests;
using HypermediaEngine.API.Public.Authentication.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Authentication
{
    public class AuthenticationModule : SecureApiModule
    {
        private readonly IRepository _repository;

        public AuthenticationModule(IRepository repository)
        {
            _repository = repository;

            Post["/api/login"] = _ => Handle(this.BindAndValidate<PostLogin>());
            Delete["/api/logout"] = _ => Handle(this.BindAndValidate<DeleteLogout>());
        }

        private dynamic Handle(PostLogin request)
        {
            var user = _repository.SingleOrDefault<User>(x => x.Username.ToLower() == request.Username.ToLower());
            if (user == null || user.Validate(request.Password) == false)
                return Response.AsJson(new LoginFailed(Context))
                               .WithContentType("application/vnd.siren+json; charset=utf-8");

            var accessToken = user.IssueAccessToken();
            _repository.SaveOrUpdate(user);

            return new LoginSuccess(Context, accessToken);
        }

        private dynamic Handle(DeleteLogout request)
        {
            var authenticatedUser = Context.GetUser();

            var user = _repository.SingleOrDefault<User>(x => x.Id == authenticatedUser.Id);
            if (user != null)
            {
                user.RevokeAccessToken(authenticatedUser.AccessToken);
                _repository.SaveOrUpdate(user);
            }

            return new LogoutSuccess(Context);
        }
    }
}