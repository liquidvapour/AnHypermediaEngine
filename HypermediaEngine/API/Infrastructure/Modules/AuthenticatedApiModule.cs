using Nancy.Security;

namespace HypermediaEngine.API.Infrastructure.Modules
{
    public abstract class AuthenticatedApiModule : ApiModule
    {
        protected AuthenticatedApiModule()
        {
            this.RequiresAuthentication();
        }
    }
}