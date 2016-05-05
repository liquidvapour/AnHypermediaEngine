namespace Hypermedia.Siren.Actions.Fields
{
    public class PasswordField : Field
    {
        public string Value { get; set; }

        public PasswordField(string title, string name, bool required = true) : base(title, name, "password", required)
        {
        }

        public PasswordField(string title, string name, string value, bool required = true) : base(title, name, "password", required)
        {
            Value = value;
        }
    }
}