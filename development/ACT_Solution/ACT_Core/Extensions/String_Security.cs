using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace ACT.Core.Extensions
{
    public static class String_Security
    {
        public static string ScopeSpecific_Encrypt(this string clearText, string optionalEntropy = null, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (clearText == null) { throw new ArgumentNullException("clearText"); }

            byte[] clearBytes = System.Text.Encoding.UTF8.GetBytes(clearText);
            byte[] entropyBytes = string.IsNullOrEmpty(optionalEntropy) ? null : System.Text.Encoding.UTF8.GetBytes(optionalEntropy);
            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropyBytes, scope);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string ScopeSpecific_Decrypt(this string encryptedText, string optionalEntropy = null, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (encryptedText == null) { throw new ArgumentNullException("encryptedText"); }

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] entropyBytes = string.IsNullOrEmpty(optionalEntropy) ? null : System.Text.Encoding.UTF8.GetBytes(optionalEntropy);
            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, entropyBytes, scope);

            return System.Text.Encoding.UTF8.GetString(clearBytes);
        }
    }
}
