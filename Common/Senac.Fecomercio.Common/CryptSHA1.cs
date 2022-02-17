using System;
using System.Text;

namespace Senac.Fecomercio.Common
{
    public static class CryptSHA1
    {
        // ou se você não quiser usar a encoding como parâmetro.
        public static string CalculateSHA1(string text)
        {
            try
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                System.Security.Cryptography.SHA1CryptoServiceProvider cryptoTransformSHA1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
                return hash.ToLower();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}