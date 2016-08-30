using System.Collections.Generic;
using Core.Persistency;
using Core.Primitives;
using Domain;
using HypermediaEngine.API.Infrastructure.Errors.Responses;
using HypermediaEngine.API.Infrastructure.Exceptions;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;
using HypermediaEngine.API.Public.Register.Requests;
using HypermediaEngine.API.Public.Register.Responses;

namespace HypermediaEngine.API.Public.Register
{
    public class RegisterModule : SecureApiModule
    {
        private readonly IRepository _repository;

        public RegisterModule(IRepository repository)
        {
            _repository = repository;

            Post["/api/register"] = _ => Handle(this.BindAndValidate<PostRegister>());
        }

        private UnauthorisedResponse Handle(PostRegister request)
        {
            if (_repository.Any<User>(x => x.Username == request.Username))
                throw new HypermediaEngineException(new RegisterFailedWithUsernameAlreadyRegistered(Context, request));

            var user = new User(request.Username, request.Password);
            user.SetClaims(new List<Claim> { Claim.Authenticated });
            _repository.SaveOrUpdate(user);

            return new UnauthorisedResponse(Context);
        }
    }
}