using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace registerCardApi.Model
{
    public class Card
    {

        private string _cardNumber;
        private string _cvv;
        private string _customerId;
        private PaymentProvider _paymentProvider;

        public Card()
        {
            
        }

        public Card(string cardNumber, string cvv, string customerId, PaymentProvider paymentProvider)
        {
            CardNumber = cardNumber;
            Cvv = cvv;
            CustomerId = customerId;
            PaymentProvider = paymentProvider;
        }

        public string CardNumber { get => _cardNumber; set => _cardNumber = value; }
        public string Cvv { get => _cvv; set => _cvv = value; }
        public string CustomerId { get => _customerId; set => _customerId = value; }
        public PaymentProvider PaymentProvider { get => _paymentProvider; set => _paymentProvider = value; }
    }

    public enum PaymentProvider
    {
        ProviderA,
        ProviderB
    }

}
