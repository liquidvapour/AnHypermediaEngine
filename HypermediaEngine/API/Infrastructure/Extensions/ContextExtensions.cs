using System.Linq;
using Core.Primitives;
using HypermediaEngine.API.Infrastructure.Users;
using Nancy;

namespace HypermediaEngine.API.Infrastructure.Extensions
{
    public static class ContextExtensions
    {
        public static UserIdentity GetUser(this NancyContext context)
        {
            return context.CurrentUser as UserIdentity;
        }

        public static bool HasUserClaim(this NancyContext context, Claim claim)
        {
            var user = context.GetUser();
            if (user == null)
                return false;

            return user.Claims.Contains(claim.ToString());
        }
    }
}