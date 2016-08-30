using System.Collections.Generic;

namespace Siren
{
    public abstract class Entity
    {
        public string Href { get; set; }

        public IList<string> Class { get; set; }

        public IDictionary<string, object> Properties { get; set; }

        public IList<Entity> Entities { get; set; }

        public IList<Link> Links { get; set; }

        public IList<Action> Actions { get; set; }

        protected Entity(string href, string @class)
        {
            Href = href;
            Class = new List<string> { @class };
        }

        protected Entity(string href, IList<string> @class)
        {
            Href = href;
            Class = @class;
        }
    }
}