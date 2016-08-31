using System.Linq;
using Core.Persistency;
using Domain;
using Domain.Factories;
using Domain.Users;
using HypermediaEngine.API.Authenticated.Remittances.Requests;
using HypermediaEngine.API.Authenticated.Remittances.Requests.PayoutNetworks;
using HypermediaEngine.API.Authenticated.Remittances.Responses;
using HypermediaEngine.API.Infrastructure.Extensions;
using HypermediaEngine.API.Infrastructure.Modules;

namespace HypermediaEngine.API.Authenticated.Remittances
{
    public class RemittancesModule : AuthenticatedApiModule
    {
        private readonly IRepository _repository;
        private readonly IQuoteFactory _quoteFactory;

        public RemittancesModule(IRepository repository, IQuoteFactory quoteFactory)
        {
            _repository = repository;
            _quoteFactory = quoteFactory;

            Get["/api/remittances"] = _ => Handle(this.BindAndValidate<ListRemittances>());

            Post["/api/remittances"] = _ => Handle(this.BindAndValidate<SaveRemittanceAction>());
            Post["/api/remittances/payoutNetworks"] = _ => Handle(this.BindAndValidate<SaveRemittanceForPayoutNetworkAction>());

            Get["/api/remittances/{remittanceId}/recipients"] = _ => Handle(this.BindAndValidate<SelectRecipientAction>());
            Post["/api/remittances/{remittanceId}/recipients"] = _ => Handle(this.BindAndValidate<AddRecipientAction>());
            Put["/api/remittances/{remittanceId}/recipients/{recipientId}"] = _ => Handle(this.BindAndValidate<UpdateRecipientAction>());

            Patch["/api/remittances/{remittanceId}/paymentMethod"] = _ => Handle(this.BindAndValidate<SelectPaymentMethodAction>());
            Post["/api/remittances/{remittanceId}/payment/bank"] = _ => Handle(this.BindAndValidate<BankPaymentAction>());
        }


        private dynamic Handle(ListRemittances request)
        {
            var remittances = _repository.List<Remittance>(x => x.Sender.Id == Context.GetUser().Id);
            return new RemittancesListState(Context, remittances);
        }


        private dynamic Handle(SaveRemittanceAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            var deliveryMethod = corridor.DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId);

            var quote = _quoteFactory.For(corridor, deliveryMethod, request.Currency, request.Amount);

            var sender = _repository.SingleOrDefault<Sender>(x => x.Id == Context.GetUser().Id);
            var recipients = sender.Recipients.Where(x => x.Country == request.DestinationCountry).ToList();

            var remittance = new Remittance(sender, corridor, deliveryMethod, quote);
            _repository.SaveOrUpdate(remittance);

            return new RemittanceState(Context, remittance, recipients);
        }

        private dynamic Handle(SaveRemittanceForPayoutNetworkAction request)
        {
            var corridor = _repository.SingleOrDefault<Corridor>(x => x.Origin.Code == request.OriginCountry && x.Destination.Code == request.DestinationCountry);
            var deliveryMethod = corridor.DeliveryMethods.Single(x => x.Id == request.DeliveryMethodId);
            var payoutNetwork = deliveryMethod.PayoutNetworks.Single(x => x.Id == request.PayoutNetworkId);

            var quote = _quoteFactory.For(corridor, deliveryMethod, request.Currency, request.Amount);

            var sender = _repository.SingleOrDefault<Sender>(x => x.Id == Context.GetUser().Id);
            var recipients = sender.Recipients.Where(x => x.Country == request.DestinationCountry).ToList();

            var remittance = new Remittance(sender, corridor, deliveryMethod, payoutNetwork, quote);
            _repository.SaveOrUpdate(remittance);

            return new RemittanceState(Context, remittance, recipients);
        }


        private dynamic Handle(SelectRecipientAction request)
        {
            var remittance = _repository.SingleOrDefault<Remittance>(x => x.Id == request.RemittanceId);

            var sender = _repository.SingleOrDefault<Sender>(x => x.Id == Context.GetUser().Id);
            var recipients = sender.Recipients.Where(x => x.Country == remittance.Corridor.Destination.Code).ToList();
            var recipient = sender.Recipients.SingleOrDefault(x => x.Id == request.RecipientId);

            if (recipient == null)
                return new RemittanceState(Context, remittance, recipients);

            return new RemittanceState(Context, remittance, recipients, recipient);
        }

        private dynamic Handle(AddRecipientAction request)
        {
            var remittance = _repository.SingleOrDefault<Remittance>(x => x.Id == request.RemittanceId);

            var sender = _repository.SingleOrDefault<Sender>(x => x.Id == Context.GetUser().Id);

            var recipient = new Recipient(remittance.Corridor.Destination.Code, request.City, request.Postcode, request.Address, request.Name, request.SendingReasonId);

            sender.AddRecipient(recipient);
            remittance.SetRecipient(recipient);

            return new RemittanceState(Context, remittance, recipient);
        }

        private dynamic Handle(UpdateRecipientAction request)
        {
            var remittance = _repository.SingleOrDefault<Remittance>(x => x.Id == request.RemittanceId);

            var sender = _repository.SingleOrDefault<Sender>(x => x.Id == Context.GetUser().Id);
            
            var recipient = sender.Recipients.Single(x => x.Id == request.RecipientId);
            recipient.Update(remittance.Corridor.Destination.Code, request.City, request.Postcode, request.Address,
                                request.Name, request.SendingReasonId);

            remittance.SetRecipient(recipient);

            return new RemittanceState(Context, remittance, recipient);
        }


        private dynamic Handle(SelectPaymentMethodAction request)
        {
            var remittance = _repository.SingleOrDefault<Remittance>(x => x.Id == request.RemittanceId);
            var paymentMethod = _repository.SingleOrDefault<PaymentMethod>(x => x.Id == request.PaymentMethodId);

            remittance.SetPaymentMethod(paymentMethod);

            return new RemittanceState(Context, remittance, remittance.Recipient, paymentMethod);
        }

        private dynamic Handle(BankPaymentAction request)
        {
            var remittance = _repository.SingleOrDefault<Remittance>(x => x.Id == request.RemittanceId);
            remittance.Pay(request.AccountHolderName, request.RoutingNumber, request.AccountNumber);

            return new RemittanceState(Context, remittance, remittance.Recipient, remittance.PaymentMethod, remittance.Payment);
        }
    }
}