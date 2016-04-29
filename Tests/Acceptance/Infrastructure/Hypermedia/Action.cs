using System.Collections.Generic;

namespace Acceptance.Infrastructure.Hypermedia
{
    public class Action
    {
        public string Name { get; set; }
        public string Title { get; set; }

        public string Method { get; set; }
        public string Href { get; set; }
        public IEnumerable<Field> Fields { get; set; }

        public string Type { get; set; }
    }
}