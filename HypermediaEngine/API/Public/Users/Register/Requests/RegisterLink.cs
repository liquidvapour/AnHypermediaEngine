using HypermediaEngine.API.Infrastructure.Requests.Links;

namespace HypermediaEngine.API.Public.Users.Register.Requests
{
    public class RegisterLink : ApiLink
    {
        public string Country { get; set; }

        public RegisterLink() : base("Register", "/api/register", new [] { "register" })
        {
            Country = "gb";
        }

        public RegisterLink(string country) : this()
        {
            if (string.IsNullOrWhiteSpace(country) == false)
                Country = country;
        }
    }
}