using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Register.Requests
{
    public class SelectRegisterCountryAction : ApiAction
    {
        public string Country { get; set; }

        public SelectRegisterCountryAction() : base("Registration Country", "GET", "/api/register", false)
        {
        }

        public SelectRegisterCountryAction(string country) : this()
        {
            Country = country;
        }
    }
}