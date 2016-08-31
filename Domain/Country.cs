using Core.Domain;

namespace Domain
{
    public class Country : IValueObject
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public Currency Currency { get; private set; }

        public Country(string code, string name, Currency currency)
        {
            Code = code;
            Name = name;
            Currency = currency;
        }
    }
}