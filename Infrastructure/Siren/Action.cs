using System.Collections.Generic;

namespace Siren
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
            Name = FormatName(name);
            Method = method;
            Href = href;
            Fields = fields;

            if (method != "GET")
                Type = "application/x-www-form-urlencoded";
        }

        public Action(string title, string method, string href, IList<Field> fields = null) : this(title, title, method, href, fields)
        {
        }

        private static string FormatName(string name)
        {
            if (name == null)
                return null;

            return name.ToLower().Replace(" ", "-");
        }
    }
}