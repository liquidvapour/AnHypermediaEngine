using Core.Domain;

namespace Domain
{
    public class Quote : IValueObject
    {
        public string RequestedCurrency { get; private set; }
        public decimal RequestedAmount { get; private set; }

        public string OriginCurrency { get; private set; }
        public decimal OriginAmount { get; private set; }

        public decimal Fees { get; private set; }
        public decimal Payment { get; private set; }

        public decimal ExchangeRate { get; private set; }

        public string DestinationCurrency { get; private set; }
        public decimal DestinationAmount { get; private set; }

        public Quote(string requestedCurrency, decimal requestedAmount,
                            string originCurrency, decimal originAmount, 
                            decimal fees, decimal payment, decimal exchangeRate,
                            string destinationCurrency, decimal destinationAmount)
        {
            RequestedCurrency = requestedCurrency;
            RequestedAmount = requestedAmount;
            OriginCurrency = originCurrency;
            OriginAmount = originAmount;

            Fees = fees;
            Payment = payment;

            ExchangeRate = exchangeRate;

            DestinationCurrency = destinationCurrency;
            DestinationAmount = destinationAmount;
        }
    }
}