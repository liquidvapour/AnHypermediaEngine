using System.Linq;
using Core.Primitives;
using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Commands;

namespace HypermediaEngine.API.Authenticated.Users.Claims
{
    public class PutClaimsValidator : ApiCommandValidator<PutClaims>
    {
        public PutClaimsValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Claims).Must(claims => claims == null 
                                                    || claims.All(claim =>
                                                        {
                                                            Claim temp;
                                                            return Claim.TryParse(claim, out temp);
                                                        }));
        }
    }
}