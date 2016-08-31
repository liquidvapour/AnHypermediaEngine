using System.Collections.Generic;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Public.Languages.Requests;
using HypermediaEngine.API.Public.Languages.Responses;
using Nancy;

namespace HypermediaEngine.API.Public.Languages
{
    public class LanguagesModule : NancyModule
    {
        public LanguagesModule()
        {
            Get["/api/languages"] = _ => Handle(this.BindAndValidate<LanguagesLookup>());
        }

        private dynamic Handle(LanguagesLookup request)
        {
            var languageOptions = new Dictionary<string, string>
            {
                {"en-GB", "English"},
                {"fr-FR", "Français"},
                {"es-ES", "Español"}
            };

            return new LanguageOptions(Context, languageOptions);
        }
    }
}