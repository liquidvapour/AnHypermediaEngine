using Core.Domain;

namespace Domain
{
    public class Currency : IValueObject
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public Currency(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}