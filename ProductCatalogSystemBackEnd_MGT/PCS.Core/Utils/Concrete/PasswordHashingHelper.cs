using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Linq;
using System.Security.Cryptography;

namespace PCS.Core.Utils.Concrete
{
    public static class PasswordHashingHelper
    {
        public static byte[] HashPassword(string password, byte[] salt)
        {
            //Password-Based Key Derivation Function 2
            var hashedPassword = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,// pseudo-random function
            iterationCount: 1000,// The number of iterations of the pseudo-random function to apply during the key derivation process.
            numBytesRequested: 32);
            return hashedPassword;
        }

        public static bool VerifyPassword(string incomingPassword, byte[] hashedPasswordFromDb,byte[] saltFromDb)
        {
            return hashedPasswordFromDb.SequenceEqual(HashPassword(incomingPassword, saltFromDb));
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }
    }
}
