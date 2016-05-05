using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.Siren.Actions.Fields
{
    public class MultiSelectField : Field
    {
        public string[] Value { get; set; }
        public KeyValuePair<string, string>[] Options { get; set; }

        public MultiSelectField(string title, string name, IEnumerable<KeyValuePair<string, string>> options, string[] value = null, bool required = false) : base(title, name, "select", required)
        {
            Value = value;
            Options = options.ToArray();
        }
    }
}