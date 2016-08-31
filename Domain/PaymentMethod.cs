using System;
using Core.Domain;

namespace Domain
{
    public class PaymentMethod : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string Name { get; private set; }

        private PaymentMethod()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public PaymentMethod(string name) : this()
        {
            Name = name;
        }
    }
}