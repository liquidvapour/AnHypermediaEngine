using System;
using Core.Domain;

namespace Domain
{
    public abstract class Payment : IEntity
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public PaymentStatus Status { get; private set; }

        protected Payment()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;

            Status = PaymentStatus.Pending;
        }

        public void Authorise()
        {
            Status = PaymentStatus.Authorise;
        }

        public void Authorised()
        {
            Status = PaymentStatus.Authorised;
        }

        public void Capture()
        {
            Status = PaymentStatus.Capture;
        }

        public void Captured()
        {
            Status = PaymentStatus.Captured;
        }

        public void Returned()
        {
            Status = PaymentStatus.Refund;
        }

        public void Refund()
        {
            Status = PaymentStatus.Refund;
        }

        public void Refunded()
        {
            Status = PaymentStatus.Refund;
        }
    }

    public class BankPayment : Payment
    {
        public string AccountHolderName { get; private set; }
        public string RoutingNumber { get; private set; }
        public string AccountNumber { get; private set; }

        public BankPayment(string accountHolderName, string routingNumber, string accountNumber)
        {
            AccountHolderName = accountHolderName;
            RoutingNumber = routingNumber;
            AccountNumber = accountNumber;
        }
    }

    public enum PaymentStatus
    {
        Pending,

        Authorise,
        Authorised,
        AuthorisationFailed,

        Capture,
        Captured,
        CaptureFailed,

        Returned,

        Refund,
        Refunded,
        RefundFailed
    }
}