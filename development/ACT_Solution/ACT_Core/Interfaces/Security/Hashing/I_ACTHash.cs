///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Security\Hashing\I_ACTHash.cs
//
// summary:	Declares the I_ACTHash interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Security.Hashing
{
    /// <summary>
    /// ACT Hash is a Variable Hashing Interface used for many things, verification, security, Raid. etc.
    /// </summary>
    public interface I_ACTHash
    {
        /// <summary>
        /// Get Hash Code From a String amy String
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        ulong GetHashCode_ulong(string x);
        
        /// <summary>
        /// Get Hashcode from byte array
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        byte[] GetHashCode_bytearray(byte[] x);

        /// <summary>
        /// Get Hash Code of the Current Assembky
        /// </summary>
        /// <returns></returns>
        byte[] GetAssemblyHashCode();

        /// <summary>
        /// Get String Hash Code of Desginated Byte Array
        /// </summary>
        /// <returns></returns>
        string GetHashCodeString(byte[] x);

    }
}
