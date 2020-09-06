///-------------------------------------------------------------------------------------------------
// file:	Security\Licensing\I_License.cs
//
// summary:	Declares the I_License interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Security.Licensing
{
    /// <summary>
    /// Represents a License
    /// </summary>
    public interface I_License
    {
        /// <summary>
        /// Contains all the License Data
        /// </summary>
        byte[] LicenseData { get; set; }
    }
}
