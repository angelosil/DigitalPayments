using registerCardApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace registerCardApi.Service
{
    public class CardRegistrationService
    {
        public TokenDTO RegisterCard(Card card)
        {
            
            if (string.IsNullOrEmpty(card.CardNumber) || card.CardNumber.Length < 4 || string.IsNullOrEmpty(card.Cvv) || card.Cvv.Length != 4)
            {
                throw new ArgumentException("Invalid card information");
            }

            // Call the respective payment provider based on configuration
            string token;

            switch (card.PaymentProvider)
            {
                case PaymentProvider.ProviderA:
                    token = GenerateTokenProviderA(card.CardNumber, card.Cvv);
                    break;
                case PaymentProvider.ProviderB:
                    token = GenerateTokenProviderB(card.CardNumber, card.Cvv);
                    break;
                default:
                    throw new ArgumentException("Invalid payment provider");
            }

            TokenDTO tokendto = new TokenDTO() { Token = token };

                        
            return tokendto;
        }

        private string GenerateTokenProviderA(string cardNumber, string cvv)
        {
            
            string input = $"{cardNumber}{cvv}";
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private string GenerateTokenProviderB(string cardNumber, string cvv)
        {
         
            int rotations = int.Parse(cvv) % 4; // Assuming cvv is a 4-digit number
         
            string lastFourDigits = cardNumber.Substring(cardNumber.Length - 4);

            
            int[] array = lastFourDigits.Select(c => int.Parse(c.ToString())).ToArray();

            
            for (int i = 0; i < rotations; i++)
            {
                int temp = array[array.Length - 1];
                Array.Copy(array, 0, array, 1, array.Length - 1);
                array[0] = temp;
            }

            
            string rotatedDigits = string.Join("", array);

            return rotatedDigits;
        }
        
    }
}
