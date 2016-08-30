namespace Siren
{
    public class Link
    {
        public string Title { get; set; }
        public string Href { get; set; }
        public string[] Rel { get; set; }

        public Link(string title, string href, string[] rel)
        {
            Title = title;
            Href = href;
            Rel = rel;
        }
    }
}