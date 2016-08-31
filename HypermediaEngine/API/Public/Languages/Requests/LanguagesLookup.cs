using HypermediaEngine.API.Infrastructure.Requests.Actions;

namespace HypermediaEngine.API.Public.Languages.Requests
{
    public class LanguagesLookup : ApiAction
    {
        public LanguagesLookup() : base("Language Lookup", "GET", "/api/languages", false)
        {
        }
    }
}