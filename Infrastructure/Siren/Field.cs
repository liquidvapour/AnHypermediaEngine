using Siren.Extensions;

namespace Siren
{
    public class Field
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public object Value { get; set; }
        public object Min { get; set; }
        public object Max { get; set; }
        public object Step { get; set; }

        public object Options { get; set; }
        public object OptionsLookup { get; set; }

        public bool Required { get; set; }

        public Field(string title, string name, string type, object value, bool required = true)
        {
            Title = title.FormatAsTitle();
            Name = name.FormatAsName();

            Type = type;
            Value = value;
            Required = required;
        }

        public Field(string title, string type, object value, bool required = true) : this(title, title, type, value, required)
        {
        }
    }
}