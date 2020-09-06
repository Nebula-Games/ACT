using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;

namespace ACT.Core.BuiltInPlugins.Security.Hashing
{
    /// <summary>
    /// ACT Hash is a Variable Hashing Interface used for many things, verification, security, Raid. etc.
    /// </summary>
    public class ACT_SecureHash: Interfaces.Security.Hashing.I_ACT_SecureHash
    {
        public byte[] GenerateHash(object o)
        {
            var _bytes = o.ObjectToByteArray();
            var _Create = System.Security.Cryptography.HashAlgorithm.Create("SHA512");
            return _Create.ComputeHash(_bytes);
        }

        /// <summary>
        /// Generates a Base64 String of the class
        /// </summary>
        /// <returns>Base64 String</returns>
        public string GenerateHashString(object o)
        {
            var _bytes = o.ObjectToByteArray();
            var _Create = System.Security.Cryptography.HashAlgorithm.Create("SHA512");
            return _Create.ComputeHash(_bytes).ToBase64String();
        }

    }
}
