using System.Linq;
using Core.Primitives;
using FluentValidation;
using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Authenticated.Users.Claims
{
    public class PutClaimsValidator : ApiActionValidator<PutClaims>
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