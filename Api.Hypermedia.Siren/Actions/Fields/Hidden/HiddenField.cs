namespace Hypermedia.Siren.Actions.Fields.Hidden
{
    public class HiddenField : Field
    {
        public HiddenField(string name, bool required = true) : base(string.Empty, name, "hidden", required)
        {
        }
    }
}