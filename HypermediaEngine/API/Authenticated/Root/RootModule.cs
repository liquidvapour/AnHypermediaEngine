using HypermediaEngine.API.Authenticated.Root.Get;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.Root
{
    public class RootModule : AuthenticatedApiModule
    {
        public RootModule()
        {
            Get["/api"] = _ => this.Query<GetRoot, Get.Root>(Handle);
        }

        private Get.Root Handle(GetRoot request)
        {
            return new Get.Root(Context);
        }
    }
}