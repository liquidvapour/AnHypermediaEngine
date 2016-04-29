using Nancy;

namespace HypermediaEngine.Website
{
    public class WebsiteModule : NancyModule
    {
        public WebsiteModule()
        {
            //this.RequiresHttps(true);
            Get["/"] = _ => View["Index"];
        }
    }
}