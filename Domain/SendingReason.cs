using System;
using Core.Domain;

namespace Domain
{
    public class SendingReason : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string Name { get; private set; }

        private SendingReason()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public SendingReason(string name) : this()
        {
            Name = name;
        }
    }
}