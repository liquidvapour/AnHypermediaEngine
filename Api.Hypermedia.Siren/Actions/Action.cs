using System.Collections.Generic;
using Hypermedia.Siren.Actions.Fields;

namespace Hypermedia.Siren.Actions
{
    public class Action
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Method { get; set; }

        public string Href { get; set; }

        public IList<Field> Fields { get; set; }

        public string Type { get; set; }

        public Action(string title, string name, string method, string href, IList<Field> fields = null)
        {
            Title = title;
            Name = name;
            Method = method;
            Href = href;
            Fields = fields;
            Type = "application/vnd.siren+json";
        }
    }
}