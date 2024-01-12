using registerCardApi.Model;
using registerCardApi.Service;
using System;
using Xunit;

namespace CardRegistrationServiceTests
{
    public class CardRegistrationServiceTests
    {
        [Fact]
        public void RegisterCard_WithInvalidCardNumber_ThrowsArgumentException()
        {
            CardRegistrationService cardService = new CardRegistrationService();
            string customerId = "123456";
            string cardNumber = "123"; // Invalid card number
            string cvv = "1234";
            PaymentProvider provider = PaymentProvider.ProviderA;

            Card card = new Card(cardNumber, cvv, customerId, provider);

            Assert.Throws<ArgumentException>(() => cardService.RegisterCard(card));
        }

        [Fact]
        public void RegisterCard_WithInvalidCVV_ThrowsArgumentException()
        {
            CardRegistrationService cardService = new CardRegistrationService();
            string customerId = "123456";
            string cardNumber = "1234567890123456";
            string cvv = "12"; // Invalid CVV
            PaymentProvider provider = PaymentProvider.ProviderA;

            Card card = new Card(cardNumber, cvv, customerId, provider);

            Assert.Throws<ArgumentException>(() => cardService.RegisterCard(card));
        }

        [Fact]
        public void RegisterCard_WithInvalidPaymentProvider_ThrowsArgumentException()
        {
            CardRegistrationService cardService = new CardRegistrationService();
            string customerId = "123456";
            string cardNumber = "1234567890123456";
            string cvv = "1234";
            PaymentProvider provider = (PaymentProvider)99; // Invalid payment provider

            Card card = new Card(cardNumber, cvv, customerId, provider);

            Assert.Throws<ArgumentException>(() => cardService.RegisterCard(card));
        }

        [Theory]
        [InlineData("1234567890123456", "1234", PaymentProvider.ProviderA, "71a54fbbae3802dfab4493db17f8a46e")]
        [InlineData("9876543210987654", "5678", PaymentProvider.ProviderB, "5476")]
        public void RegisterCard_ReturnsToken(string cardNumber, string cvv, PaymentProvider provider, string expectedToken)
        {
            CardRegistrationService cardService = new CardRegistrationService();
            string customerId = "123456";

            Card card = new Card(cardNumber, cvv, customerId, provider);

            TokenDTO token = cardService.RegisterCard(card);

            Assert.Equal(expectedToken, token.Token);
        }
    }
}
