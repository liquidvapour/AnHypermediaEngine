using Nancy.Security;

namespace HypermediaEngine.API.Infrastructure.Modules
{
    public abstract class AuthenticatedApiModule : SecureApiModule
    {
        protected AuthenticatedApiModule()
        {
            this.RequiresAuthentication();
        }
    }
}