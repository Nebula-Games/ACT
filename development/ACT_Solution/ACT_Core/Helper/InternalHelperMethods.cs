using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using ACT.Core.Extensions;

namespace ACT.Core.Helper
{
    public static class LocalComputerSpecific_HelperMethods
    {
        /// <summary>
        /// Encrypt a String that is ONLY Decryptable on the LOCAL Machine
        /// </summary>
        /// <param name="DataToEncrypt"></param>
        /// <param name="PWModifier"></param>
        /// <returns></returns>
        public static string EncryptString_ComputerSpecific(string DataToEncrypt, string PWModifier)
        {
            return DataToEncrypt.ScopeSpecific_Encrypt(PWModifier, DataProtectionScope.LocalMachine);
        }

        /// <summary>
        /// Decrypt a String that was encrypted on the LOCAL Machine
        /// </summary>
        /// <param name="DataToEncrypt"></param>
        /// <param name="PWModifier"></param>
        /// <returns></returns>
        public static string DecryptString_ComputerSpecific(string DataToDecrypt, string PWModifier)
        {
            return DataToDecrypt.ScopeSpecific_Decrypt(PWModifier, DataProtectionScope.LocalMachine);
        }
    }
}
