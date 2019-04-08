using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Services
{
    public interface IPasswordHasher
    {
        (string Password, string Salt) HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string salt, string password);
    }

    public class PasswordHasher : IPasswordHasher
    {
        public (string Password, string Salt) HashPassword(string password)
        {
            var saltBytes = this.GenerateSalt();
            var salt = Convert.ToBase64String(saltBytes);
            var hashed = this.GenerateHash(password, saltBytes);
            return (hashed, salt);
        }

        public bool VerifyPassword(string hashedPassword, string salt, string password)
        {
            var saltBytes = Convert.FromBase64String(salt);
            return hashedPassword == this.GenerateHash(password, saltBytes);
        }

        private byte[] GenerateSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string GenerateHash(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)
            );
        }
    }
}