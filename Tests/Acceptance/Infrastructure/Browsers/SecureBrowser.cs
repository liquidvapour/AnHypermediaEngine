using System;
using System.Collections.Generic;
using Nancy.TinyIoc;
using Nancy.Testing;

namespace Acceptance.Infrastructure.Browsers
{
    internal class SecureBrowser : JsonBrowser
    {
        public SecureBrowser() 
            : this(new List<Action<BrowserContext>> { http => http.HttpsRequest() })
        {
        }

        public SecureBrowser(Action<TinyIoCContainer> stubsRegistration) 
            : this(new List<Action<BrowserContext>> { http => http.HttpsRequest() }, stubsRegistration)
        {
        }

        public SecureBrowser(IEnumerable<Action<BrowserContext>> defaults, Action<TinyIoCContainer> stubsRegistration = null) 
            : base(new List<Action<BrowserContext>>(defaults) { http => http.HttpsRequest() }, stubsRegistration)
        {
        }
    }
}