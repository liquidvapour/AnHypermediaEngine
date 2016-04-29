using System;
using System.Collections.Generic;
using HypermediaEngine;
using HypermediaEngine.API.Infrastructure.Users;
using Moq;
using Nancy.Testing;
using Nancy.TinyIoc;

namespace Acceptance.Infrastructure.Browsers
{
    internal class AuthenticatedBrowser : SecureBrowser
    {
        public AuthenticatedBrowser() 
            : base(new List<Action<BrowserContext>> { http => http.Header("authorization", "accessToken") }, 
                                                        container =>
                                                        {
                                                            var statelessAuthenticatorStub = new Mock<IStatelessAuthenticator>();
                                                            statelessAuthenticatorStub.Setup(x => x.GetUserIdentityBy(It.IsAny<Guid>())).Returns(new UserIdentity());

                                                            container.Register(statelessAuthenticatorStub.Object);
                                                        })
        {
        }

        public AuthenticatedBrowser(Action<TinyIoCContainer> stubsRegistration) 
            : base(new List<Action<BrowserContext>> { http => http.Header("authorization", "accessToken") },
                                                        container =>
                                                        {
                                                            stubsRegistration.Invoke(container);

                                                            var statelessAuthenticatorStub = new Mock<IStatelessAuthenticator>();
                                                            statelessAuthenticatorStub.Setup(x => x.GetUserIdentityBy(It.IsAny<Guid>())).Returns(new UserIdentity());

                                                            container.Register(statelessAuthenticatorStub.Object);
                                                        })
        {
        }

        public AuthenticatedBrowser(IEnumerable<Action<BrowserContext>> defaults, Action<TinyIoCContainer> stubsRegistration)
            : base(new List<Action<BrowserContext>>(defaults) { http => http.Header("authorization", "accessToken") },
                                                        container =>
                                                        {
                                                            stubsRegistration.Invoke(container);

                                                            var statelessAuthenticatorStub = new Mock<IStatelessAuthenticator>();
                                                            statelessAuthenticatorStub.Setup(x => x.GetUserIdentityBy(It.IsAny<Guid>())).Returns(new UserIdentity());

                                                            container.Register(statelessAuthenticatorStub.Object);
                                                        })
        {
        }
    }
}