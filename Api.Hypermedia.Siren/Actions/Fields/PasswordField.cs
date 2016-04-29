namespace Hypermedia.Siren.Actions.Fields
{
    public class PasswordField : Field
    {
        public string Value { get; set; }

        public PasswordField(string prompt, string name, bool required = true) : base(prompt, name, "password", required)
        {
        }

        public PasswordField(string prompt, string name, string value, bool required = true) : base(prompt, name, "password", required)
        {
            Value = value;
        }
    }
}