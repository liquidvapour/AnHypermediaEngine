using Core.Persistency;
using Domain.Users;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;
using Nancy;

namespace HypermediaEngine.API.Public.Users.Logout
{
    public class LogoutModule : SecureApiModule
    {
        private readonly IRepository _repository;

        public LogoutModule(IRepository repository)
        {
            _repository = repository;

            Delete["/api/logout"] = _ =>
            {
                var authenticatedUser = Context.GetUser();

                var user = _repository.SingleOrDefault<User>(x => x.Id == authenticatedUser.Id);
                if (user != null)
                {
                    user.RevokeAccessToken(authenticatedUser.AccessToken);
                    _repository.SaveOrUpdate(user);
                }

                Context.Request.Query["accessToken"] = null;
                return Response.AsRedirect(Context.GetFullUrlFor("/api"));
            };
        }
    }
}