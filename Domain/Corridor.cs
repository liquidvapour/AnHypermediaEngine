using System;
using System.Collections.Generic;
using Core.Domain;

namespace Domain
{
    public class Corridor : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public Country Origin { get; private set; }
        public Country Destination { get; private set; }
        public List<DeliveryMethod> DeliveryMethods { get; private set; }

        public decimal ExchangeRate { get; private set; }
        public decimal FixedFee { get; private set; }

        private Corridor()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            DeliveryMethods = new List<DeliveryMethod>();
        }

        public Corridor(Country origin, Country destination, decimal exchangeRate, decimal fixedFee) : this()
        {
            Origin = origin;
            Destination = destination;
            ExchangeRate = exchangeRate;
            FixedFee = fixedFee;
        }

        public void AddDeliveryMethod(DeliveryMethod deliveryMethod)
        {
            DeliveryMethods.Add(deliveryMethod);
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