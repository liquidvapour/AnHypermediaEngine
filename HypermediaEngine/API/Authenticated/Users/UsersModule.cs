using System.Linq;
using Core.Persistency;
using Core.Primitives;
using HypermediaEngine.API.Authenticated.Users.Claims;
using HypermediaEngine.API.Authenticated.Users.Delete;
using HypermediaEngine.API.Authenticated.Users.Get;
using HypermediaEngine.API.Authenticated.Users.List;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.Users
{
    public class UsersModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;

        public UsersModule(IRepository repository)
        {
            _repository = repository;

            Get["/api/users"] = _ => this.Query<GetUsers, UsersCollection>(Handle);
            Get["/api/users/{id}"] = _ => this.Query<GetUser, User>(Handle);

            Put["/api/users/{id}/claims"] = _ => this.Command<PutClaims, User>(Handle);

            Delete["/api/users/{id}"] = parameters => Handle(new DeleteUser { Id = parameters.Id });
        }

        private UsersCollection Handle(GetUsers request)
        {
            var users = _repository.List<Domain.User>().OrderBy(x => x.CreatedOn);
            return new UsersCollection(Context, users);
        }

        private User Handle(GetUser request)
        {
            var user = _repository.SingleOrDefault<Domain.User>(x => x.Id == request.Id);
            if (user == null)
                throw new HypermediaEngineException(new NotFoundResponse(Context));

            return new User(Context, user);
        }

        private User Handle(PutClaims request)
        {
            var user = _repository.SingleOrDefault<Domain.User>(x => x.Id == request.Id);
            user.SetClaims(request.Claims.Select(x =>
                                                     {
                                                         Claim result;
                                                         Claim.TryParse(x, out result);

                                                         return result;
                                                     }).ToList());
            _repository.SaveOrUpdate(user);

            return new User(Context, user);
        }

        private DeleteUserSuccess Handle(DeleteUser request)
        {
            var user = _repository.SingleOrDefault<Domain.User>(x => x.Id == request.Id);
            _repository.Remove(user);

            return new DeleteUserSuccess(Context);
        }
    }
}