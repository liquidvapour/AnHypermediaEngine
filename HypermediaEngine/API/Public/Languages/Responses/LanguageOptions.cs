using System.Collections.Generic;
using System.Linq;
using Nancy;
using Siren;

namespace HypermediaEngine.API.Public.Languages.Responses
{
    public class LanguageOptions : Entity
    {
        public LanguageOptions(NancyContext context, IDictionary<string, string> options) : base(context.Request.Url.ToString(), new[] { "language-options" })
        {
            Properties = options.ToDictionary(x => x.Key, x => (object) x.Value);
        }
    }
}