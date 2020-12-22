using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace RPS
{
    public class Crypto
    {

        public static byte[] CreateKey(int size)
        {
            var key = new byte[size];
            var num = RandomNumberGenerator.Create();
            num.GetBytes(key);
            return key;
        }

        public static byte[] GetHmac(byte[] key, string message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(StringEncode(message));
        }
        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }
        private static byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }
        
    }
}