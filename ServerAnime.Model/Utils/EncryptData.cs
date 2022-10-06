using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerAnime.Model.Utils
{
    public class EncryptData
    {
        private static readonly string HASH = "Protected Data";
        
        public static string Encrypt(string msg)
        {
          byte[] data = UTF8Encoding.UTF8.GetBytes(msg);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HASH));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform cryptoTransform = tripleDES.CreateEncryptor();
            byte[] resul = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(resul);
        }

        public static string Decrypt(string msgEncrypt)
        {
            byte[] data = Convert.FromBase64String(msgEncrypt);

            MD5 md5 = MD5.Create();
            TripleDES tripleDES = TripleDES.Create();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(HASH));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform cryptoTransform = tripleDES.CreateDecryptor();
            byte[] resul = cryptoTransform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(resul);
        }
    }
}
