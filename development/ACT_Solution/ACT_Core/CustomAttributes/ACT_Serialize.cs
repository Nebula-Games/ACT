///-------------------------------------------------------------------------------------------------
// file:	CustomAttributes\ACT_Serialize.cs
//
// summary:	Implements the act serialize class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.CustomAttributes
{
    /// <summary>
    /// ACTSerializable - Serialize The Options in the Class
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ACTSerializable : System.Attribute
    {
        /// <summary>
        /// Type Of Output
        /// </summary>
        public bool SerializeMe { get; set; }

        /// <summary>
        /// Encrypt The Output
        /// </summary>
        public bool EncryptOutput { get; set; }

        /// <summary>
        /// Use System Configuration
        /// </summary>
        public bool UseSystemConfiguration { get; set; }
    }
}
