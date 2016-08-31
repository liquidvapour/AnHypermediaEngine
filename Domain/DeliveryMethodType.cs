using Core.Domain;

namespace Domain
{
    public class DeliveryMethodType : IValueObject
    {
        public string Code { get; private set; }
        public string Name { get; private set; }

        public DeliveryMethodType(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}