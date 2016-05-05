namespace Hypermedia.Siren.Actions.Fields
{
    public abstract class Field
    {
        public string Title { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool Required { get; set; }

        protected Field(string title, string name, string type, bool required = true)
        {
            Title = title;
            Name = name;
            Type = type;
            Required = required;
        }
    }
}