///-------------------------------------------------------------------------------------------------
// file:	Windows\Security\Helper.cs
//
// summary:	Implements the helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Windows.Security
{
    /// <summary>
    /// Helper Method
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the executing Account Name
        /// </summary>
        public static string GetExecutingAccountName
        {
            get
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            }
        }
    }
}
