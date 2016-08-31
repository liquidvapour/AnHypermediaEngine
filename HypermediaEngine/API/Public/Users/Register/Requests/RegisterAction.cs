using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Register.Requests
{
    public class RegisterAction : ApiAction
    {
        public string Country { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterAction() : base("Register", "POST", "/api/register/{country}", false)
        {
            Country = "gb";
        }

        public RegisterAction(string country) : this()
        {
            if (string.IsNullOrWhiteSpace(country) == false)
                Country = country;
        }
    }
}