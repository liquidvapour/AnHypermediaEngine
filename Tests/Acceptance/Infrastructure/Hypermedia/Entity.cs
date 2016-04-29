using System.Collections.Generic;

namespace Acceptance.Infrastructure.Hypermedia
{
    public class Entity
    {
        public string Href { get; set; }
        public IEnumerable<string> Class { get; set; }

        public IDictionary<string, dynamic> Properties { get; set; }

        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<Link> Links { get; set; }
        public IEnumerable<Action> Actions { get; set; }
    }
}