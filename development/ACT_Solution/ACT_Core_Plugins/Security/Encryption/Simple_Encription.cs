using System;
using System.Security.Cryptography;
using System.Text;

namespace ACT.Plugins.Security.Encryption
{
    public static class EncryptorExample
    {
        public static string quote =
            "Things may come to those who wait, but only the " +
            "things left by those who hustle. -- Abraham Lincoln";

        public static (string,string) Test()
        {
            AesCryptoServiceProvider aesCSP = new AesCryptoServiceProvider();

            aesCSP.GenerateKey();
            aesCSP.GenerateIV();
            byte[] encQuote = EncryptString(aesCSP, quote);
            string _tmpEnc = Convert.ToBase64String(encQuote);
            string _tmpDec = DecryptBytes(aesCSP, encQuote);
            return (_tmpEnc, _tmpDec);
        }

        public static byte[] EncryptString(SymmetricAlgorithm symAlg, string inString)
        {
            byte[] inBlock = UnicodeEncoding.Unicode.GetBytes(inString);
            ICryptoTransform xfrm = symAlg.CreateEncryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outBlock;
        }

        public static string DecryptBytes(SymmetricAlgorithm symAlg, byte[] inBytes)
        {
            ICryptoTransform xfrm = symAlg.CreateDecryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(inBytes, 0, inBytes.Length);

            return UnicodeEncoding.Unicode.GetString(outBlock);
        }
    }
}