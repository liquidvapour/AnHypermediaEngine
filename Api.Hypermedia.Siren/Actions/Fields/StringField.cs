namespace Hypermedia.Siren.Actions.Fields
{
    public class StringField : Field
    {
        public string Value { get; set; }

        public StringField(string prompt, string name, bool required = true) : base(prompt, name, "string", required)
        {
        }

        public StringField(string prompt, string name, string value, bool required = true) : base(prompt, name, "string", required)
        {
            Value = value;
        }
    }
}