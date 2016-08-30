using System.Linq;
using Acceptance.Infrastructure.Browsers;
using Acceptance.Infrastructure.Hypermedia;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace Acceptance.Authentication
{
    [TestFixture]
    public class AuthenticationTests
    {
        private readonly IBrowser _browser = new SecureBrowser();
        private Entity _unauthorised;
        private string _accessToken;

        [TestFixtureSetUp]
        public void given_unauthorised()
        {
            var unauthorisedResponse = _browser.Get("/api");
            Assert.That(unauthorisedResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            _unauthorised = unauthorisedResponse.Body.DeserializeJson<Entity>();
            Assert.That(_unauthorised.Class, Contains.Item("login"));
        }

        [Test]
        public void it_logs_in_and_out()
        {
            it_logs_out(it_logs_in());
        }

        private Entity it_logs_in()
        {
            var loginAction = _unauthorised.Actions.Single(x => x.Name == "login");
            loginAction.Fields.Single(x => x.Name == "username").Value = "admin";
            loginAction.Fields.Single(x => x.Name == "password").Value = "pass";

            var loginResponse = _browser.Post(loginAction.Href, http =>
            {
                foreach (var field in loginAction.Fields)
                {
                    http.FormValue(field.Name, field.Value);
                }
            });

            Assert.That(loginResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var loginResponseBody = loginResponse.Body.DeserializeJson<Entity>();

            _accessToken = loginResponseBody.Properties["Access Token"];
            var authenticatedResponse = _browser.Get("/api", http => http.Header("Authorization", loginResponseBody.Properties["Access Token"]));

            return authenticatedResponse.Body.DeserializeJson<Entity>();
        }

        private void it_logs_out(Entity authenticated)
        {
            var logoutAction = authenticated.Actions.Single(x => x.Name == "log-out");

            var logoutResponse = _browser.Delete(logoutAction.Href, http => http.Header("Authorization", _accessToken));

            Assert.That(logoutResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
