namespace Hypermedia.Siren.Actions.Fields
{
    public class StringField : Field
    {
        public string Value { get; set; }

        public StringField(string title, string name, bool required = true) : base(title, name, "string", required)
        {
        }

        public StringField(string title, string name, string value, bool required = true) : base(title, name, "string", required)
        {
            Value = value;
        }
    }
}