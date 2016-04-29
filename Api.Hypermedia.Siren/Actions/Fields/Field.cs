namespace Hypermedia.Siren.Actions.Fields
{
    public abstract class Field
    {
        public string Prompt { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool Required { get; set; }

        protected Field(string prompt, string name, string type, bool required = true)
        {
            Prompt = prompt;
            Name = name;
            Type = type;
            Required = required;
        }
    }
}