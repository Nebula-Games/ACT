///-------------------------------------------------------------------------------------------------
// file:	BuiltInPlugins\Security\ACT_Hash.cs
//
// summary:	Implements the act hash class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Plugins.Security.Hashing
{

    /// <summary>
    /// ACT Hash Implementation
    /// </summary>
    public class ACT_Hash : ACT.Core.Interfaces.Security.Hashing.I_ACTHash
    {

        /// <summary>
        /// Get Assembly Hash Code
        /// </summary>
        /// <returns></returns>
        public byte[] GetAssemblyHashCode()
        {
            System.Security.Cryptography.SHA256CryptoServiceProvider md5 = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            System.IO.FileStream stream = new System.IO.FileStream(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            md5.ComputeHash(stream);
            stream.Close();

            byte[] _tmpReturn = md5.Hash;
            md5.Dispose();

            return md5.Hash;
        }

        /// <summary>
        /// Get Hash Code String
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public string GetHashCodeString(byte[] x)
        {
            System.Security.Cryptography.SHA256CryptoServiceProvider md5 = new System.Security.Cryptography.SHA256CryptoServiceProvider();

            md5.ComputeHash(x);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < md5.Hash.Length; i++)
                sb.Append(md5.Hash[i].ToString("x2"));

            return sb.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Get Hash Code Byte Arary
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public byte[] GetHashCode_bytearray(byte[] x)
        {
            System.Security.Cryptography.SHA256CryptoServiceProvider md5 = new System.Security.Cryptography.SHA256CryptoServiceProvider();

            md5.ComputeHash(x);

            return md5.Hash;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public ulong GetHashCode_ulong(string x)
        {
            return 0;
        }
    }
}
