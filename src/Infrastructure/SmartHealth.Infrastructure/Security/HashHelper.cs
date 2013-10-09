using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VinaSale.Infrastructure.Security
{
    public static class HashHelper
    {
        public static string ComputeHash(string text, HashType hashType)
        {
            byte[] bytes = ToByteArray(text);
            byte[] hash = ComputeHash(bytes, hashType);
            return ToReadableString(hash);
        }

        public static string ComputeHash(string text, string salt, HashType hashType)
        {
            byte[] data = ToByteArray(text);
            byte[] salts = ToByteArray(salt);
            byte[] hash = ComputeHash(data, salts, hashType);
            return ToReadableString(hash);
        }

        public static HashAndSalt ComputeHashAndSalt(string text, int saltLength, HashType hashType)
        {
            var salt = new byte[saltLength];
            var random = new RNGCryptoServiceProvider();
            random.GetNonZeroBytes(salt);

            var saltString = ToReadableString(salt);
            var hash = ComputeHash(text, saltString, hashType);

            return new HashAndSalt
                {
                    Hash = hash,
                    Salt = saltString
                };
        }

        private static byte[] ComputeHash(byte[] data, HashType hashType)
        {
            HashAlgorithm algorithm;
            switch (hashType)
            {
                case HashType.MD5:
                    algorithm = MD5.Create();
                    break;
                case HashType.SHA1:
                    algorithm = SHA1.Create();
                    break;
                case HashType.SHA256:
                    algorithm = SHA256.Create();
                    break;
                case HashType.SHA512:
                    algorithm = SHA512.Create();
                    break;
                default:
                    throw new ArgumentException("Invalid hash type", "hashType");
            }
            return algorithm.ComputeHash(data);
        }

        private static byte[] ComputeHash(byte[] data, byte[] salt, HashType hashType)
        {
            // Allocate memory to store both the Data and Salt together
            var dataAndSalt = new byte[data.Length + salt.Length];
            // Copy both the data and salt into the new array
            Array.Copy(data, dataAndSalt, data.Length);
            Array.Copy(salt, 0, dataAndSalt, data.Length, salt.Length);
            return ComputeHash(dataAndSalt, hashType);
        }

        private static byte[] ToByteArray(string text)
        {
            return Encoding.Unicode.GetBytes(text);
        }

        private static string ToReadableString(byte[] bytes)
        {
            return bytes.Aggregate(string.Empty, (current, x) => string.Concat(current, String.Format("{0:x2}", x)));
        }
    }
}