using Nancy;
using Nancy.Security;

namespace HypermediaEngine.API.Infrastructure.Modules
{
    public abstract class SecureApiModule : NancyModule
    {
        protected SecureApiModule()
        {
            this.RequiresHttps();
        }
    }
}