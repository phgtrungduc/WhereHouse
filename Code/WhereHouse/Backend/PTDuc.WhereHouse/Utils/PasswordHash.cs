using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Utils
{
    public class PasswordHash
    {
        const int SaltSize = 16, HashSize = 20, HashIter = 10000;
        readonly byte[] _salt, _hash;
        public PasswordHash(string password)
        {
            new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
            _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
        }

        public PasswordHash(byte[] hashBytes)
        {
            Array.Copy(hashBytes, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hashBytes, SaltSize, _hash = new byte[HashSize], 0, HashSize);
        }
        public PasswordHash(byte[] salt, byte[] hash)
        {
            Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
            Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
        }
        public byte[] ToArray(byte[] hashBytes)
        {
            hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }

        public byte[] ToArray()
        {
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
            return hashBytes;
        }
        public byte[] Salt { get { return (byte[])_salt.Clone(); } }
        public byte[] Hash { get { return (byte[])_hash.Clone(); } }
        //public static bool Verify(LoginParam password)
        //{
        //    var saltByte = Encoding.ASCII.GetBytes(password.Salt);
        //    var hashPass = Encoding.ASCII.GetBytes(password.HashPassword);
        //    var pass = password.Password;
        //    byte[] test = new Rfc2898DeriveBytes(pass, saltByte, HashIter).GetBytes(HashSize);
        //    for (int i = 0; i < HashSize; i++)
        //        if (test[i] != hashPass[i])
        //            return false;
        //    return true;
        //}

        public static LoginParam HashPassword(string password)
        {
            var res = new LoginParam { Password = password };

            byte[] hashBytes = new byte[SaltSize + HashSize];

            var salt = new byte[SaltSize];

            new RNGCryptoServiceProvider().GetBytes(salt);
            res.Salt = salt.ByteArrayToBase64String();

            var hash = new Rfc2898DeriveBytes(password, salt, HashIter).GetBytes(HashSize);

            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            res.HashPassword = hashBytes.ByteArrayToBase64String(); ;

            return res;
        }

        public static bool Verify(LoginParam password)
        {
            var saltByte = password.Salt.Base64StringToByteArray();
            var hashPass = password.HashPassword.Base64StringToByteArray();
            var pass = password.Password;
            byte[] test = new Rfc2898DeriveBytes(pass, saltByte, HashIter).GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
            {
                if (test[i] != hashPass[i + SaltSize]) return false;
            }

            return true;
        }
    }
}
