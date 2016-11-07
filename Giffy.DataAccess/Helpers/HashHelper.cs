using System;
using System.Security.Cryptography;

namespace Giffy.DataAccess.Helpers
{
    public class HashHelper
    {
        public static string GetSHA256(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}