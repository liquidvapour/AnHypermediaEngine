using System;
using System.Collections.Generic;
using Core.Domain;

namespace Domain
{
    public class DeliveryMethod : IEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public DeliveryMethodType Type { get; private set; }
        public IList<PayoutNetwork> PayoutNetworks { get; private set; }

        public decimal? ExchangeRate { get; private set; }
        public decimal? FixedFee { get; private set; }

        private DeliveryMethod()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            PayoutNetworks = new List<PayoutNetwork>();
        }

        public DeliveryMethod(DeliveryMethodType type) : this()
        {
            Type = type;
        }

        public void AddPayoutNetwork(PayoutNetwork payoutNetwork)
        {
            PayoutNetworks.Add(payoutNetwork);
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