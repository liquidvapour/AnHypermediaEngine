using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users.Login.Requests
{
    public class LoginAction : ApiAction
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginAction() : base("Login", "POST", "/api/login", false)
        {
        }
    }
}