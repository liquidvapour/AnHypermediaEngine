using Nancy;

namespace HypermediaEngine.API.Infrastructure.Modules
{
    public abstract class ApiModule : NancyModule
    {
        protected ApiModule()
        {
            //this.RequiresHttps();
        }
    }
}