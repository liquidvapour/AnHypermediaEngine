using System;
using Core.Domain;
using Domain.Users;

namespace Domain
{
    public class Remittance : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public RemittanceStatus Status { get; private set; } 

        public Sender Sender { get; private set; }
        public Corridor Corridor { get; private set; }
        public DeliveryMethod DeliveryMethod { get; private set; }
        public PayoutNetwork PayoutNetwork { get; set; }

        public Quote Quote { get; private set; }

        public Recipient Recipient { get; private set; }

        public PaymentMethod PaymentMethod { get; private set; }
        public Payment Payment { get; private set; }

        private Remittance()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            Status = RemittanceStatus.Pending;
        }

        public Remittance(Sender sender, Corridor corridor, DeliveryMethod deliveryMethod, Quote quote) : this()
        {
            Sender = sender;
            Corridor = corridor;
            DeliveryMethod = deliveryMethod;
            Quote = quote;
        }

        public Remittance(Sender sender, Corridor corridor, DeliveryMethod deliveryMethod, PayoutNetwork payoutNetwork, Quote quote)
            : this()
        {
            Sender = sender;
            Corridor = corridor;
            DeliveryMethod = deliveryMethod;
            PayoutNetwork = payoutNetwork;
            Quote = quote;
        }

        public void SetRecipient(Recipient recipient)
        {
            Recipient = recipient;
        }

        public void SetPaymentMethod(PaymentMethod paymentMethod)
        {
            PaymentMethod = paymentMethod;
        }

        public void Pay(string accountHolderName, string routingNumber, string accountNumber)
        {
            Status = RemittanceStatus.Accepted;
            Payment = new BankPayment(accountHolderName, routingNumber, accountNumber);
        }
    }

    public enum RemittanceStatus
    {
        Pending,
        Accepted,
        Processing,
        Completed
    }
}