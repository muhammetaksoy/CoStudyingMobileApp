using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CoStudying
{
    public class ManageHashing
    {
        public static byte[] StringToByte(string deger)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);
        }


        public static string SHA256(string input, string salt)
        {
            string toBeHashed;
            //string salt = CreateSalt(input);

            toBeHashed = input + salt;

            SHA256Managed crypt = new SHA256Managed();
            byte[] arySifre = StringToByte(toBeHashed);
            byte[] aryHash = crypt.ComputeHash(arySifre);
            return BitConverter.ToString(aryHash);

        }

        public static string CreateSalt()
        {
            string str = Guid.NewGuid().ToString();
            return str;
        }


    }
}