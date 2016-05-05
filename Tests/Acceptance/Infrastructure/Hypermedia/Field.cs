namespace Acceptance.Infrastructure.Hypermedia
{
    public class Field
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public string Type { get; set; }

        public bool Required { get; set; }
        
        public dynamic Value { get; set; }
        public int? Min { get; set; }
    }
}