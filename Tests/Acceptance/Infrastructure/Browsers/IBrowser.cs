using System;
using Nancy.Testing;

namespace Acceptance.Infrastructure.Browsers
{
    internal interface IBrowser
    {
        BrowserResponse Get(string endpoint);
        BrowserResponse Get(string endpoint, Action<BrowserContext> http);

        BrowserResponse Post(string endpoint);
        BrowserResponse Post(string endpoint, Action<BrowserContext> http);

        BrowserResponse Delete(string endpoint);
        BrowserResponse Delete(string endpoint, Action<BrowserContext> http);
    }
}