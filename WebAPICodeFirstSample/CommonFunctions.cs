using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample
{
    public class CommonFunctions
    {
        public static string HashPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes("sv7m7LDHommIfUG1lqLkyA==");

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
           password: password,
           salt: salt,
           prf: KeyDerivationPrf.HMACSHA1,
           iterationCount: 10000,
           numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
