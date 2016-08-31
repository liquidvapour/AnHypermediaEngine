using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Users
{
    public class ChangeLanguageAction : ApiAction
    {
        public string Language { get; set; }

        public ChangeLanguageAction() : base("Language", "GET", "/api/languages", false)
        {
        }

        public ChangeLanguageAction(string language) : this()
        {
            Language = language;
        }
    }
}