using System;
using System.Collections.Generic;
using Nancy.TinyIoc;
using Nancy.Testing;

namespace Acceptance.Infrastructure.Browsers
{
    internal class JsonBrowser : TestsBrowser
    {
        public JsonBrowser() 
            : this(new List<Action<BrowserContext>> { http => http.Accept("application/json") })
        {
        }

        public JsonBrowser(Action<TinyIoCContainer> stubsRegistration) 
            : this(new List<Action<BrowserContext>> { http => http.HttpsRequest() }, stubsRegistration)
        {
        }

        public JsonBrowser(IEnumerable<Action<BrowserContext>> defaults, Action<TinyIoCContainer> stubsRegistration = null) 
            : base(new List<Action<BrowserContext>>(defaults) { http => http.Accept("application/json") }, stubsRegistration)
        {
        }
    }
}