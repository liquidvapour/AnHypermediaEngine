namespace Hypermedia.Siren.Actions.Fields.Hidden
{
    public class HiddenStringField : HiddenField
    {
        public string Value { get; set; }

        public HiddenStringField(string name, string value = null, bool required = true) : base(name, required)
        {
            Value = value;
        }
    }
}