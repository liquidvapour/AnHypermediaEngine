namespace Domain.Factories
{
    public interface IQuoteFactory
    {
        Quote For(Corridor corridor, DeliveryMethod deliveryMethod, string currency, decimal amount);
    }

    public class QuoteFactory : IQuoteFactory
    {
        public Quote For(Corridor corridor, DeliveryMethod deliveryMethod, string currency, decimal amount)
        {
            var originAmount = amount;
            var destinationAmount = amount;

            var fees = corridor.FixedFee;
            var payment = originAmount + fees;

            if (corridor.Origin.Currency.Code == currency)
                destinationAmount = amount * corridor.ExchangeRate;
            else
                originAmount = amount * (1 + (1 - corridor.ExchangeRate));

            return new Quote(currency, amount,
                                corridor.Origin.Currency.Code, originAmount,
                                fees, payment, corridor.ExchangeRate,
                                corridor.Destination.Currency.Code, destinationAmount);
        }
    }
}