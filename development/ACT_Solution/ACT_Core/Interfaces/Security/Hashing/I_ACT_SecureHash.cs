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
    public interface I_ACT_SecureHash
    {        
        /// <summary>
        /// Get Hash Code of the Current Assembky
        /// </summary>
        /// <returns></returns>
        byte[] GenerateHash(object o);

        /// <summary>
        /// Get String Hash Code of Desginated Byte Array
        /// </summary>
        /// <returns></returns>
        string GenerateHashString(object o);

    }
}
