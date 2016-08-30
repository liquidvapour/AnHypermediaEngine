using System;
using System.Collections.Generic;
using CsQuery.ExtensionMethods;
using Nancy.TinyIoc;
using Nancy.Testing;

namespace Acceptance.Infrastructure.Browsers
{
    internal class TestsBrowser : IBrowser
    {
        protected readonly Browser Browser;

        public TestsBrowser() 
            : this(new List<Action<BrowserContext>>())
        {
        }

        public TestsBrowser(Action<TinyIoCContainer> stubsRegistration) 
            : this(new List<Action<BrowserContext>>(), stubsRegistration)
        {
        }

        public TestsBrowser(IEnumerable<Action<BrowserContext>> defaults, Action<TinyIoCContainer> stubsRegistration = null)
        {
            Browser = new Browser(new TestBootstrapper(stubsRegistration), 
                                    http =>
                                    {
                                        http.HostName("localhost");
                                        defaults.ForEach(x => x.Invoke(http));
                                    });
        }

        public BrowserResponse Get(string endpoint)
        {
            return Browser.Get(endpoint);
        }

        public BrowserResponse Get(string endpoint, Action<BrowserContext> http)
        {
            return Browser.Get(endpoint, http);
        }

        public BrowserResponse Post(string endpoint)
        {
            return Browser.Post(endpoint);
        }

        public BrowserResponse Post(string endpoint, Action<BrowserContext> http)
        {
            return Browser.Post(endpoint, http);
        }

        public BrowserResponse Delete(string endpoint)
        {
            return Browser.Delete(endpoint);
        }

        public BrowserResponse Delete(string endpoint, Action<BrowserContext> http)
        {
            return Browser.Delete(endpoint, http);
        }
    }
}