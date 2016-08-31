using System;
using Core.Domain;

namespace Domain
{
    public class PayoutNetwork : IEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public string Name { get; private set; }

        public decimal? ExchangeRate { get; private set; }
        public decimal? FixedFee { get; private set; }

        private PayoutNetwork()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }

        public PayoutNetwork(string name) : this()
        {
            Name = name;
        }

        public void SetExchangeRate(decimal exchangeRate)
        {
            ExchangeRate = exchangeRate;
        }

        public void SetFixedFee(decimal fixedFee)
        {
            FixedFee = fixedFee;
        }
    }
}