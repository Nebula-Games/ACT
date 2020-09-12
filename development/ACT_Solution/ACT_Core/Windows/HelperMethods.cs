///-------------------------------------------------------------------------------------------------
// file:	Windows\HelperMethods.cs
//
// summary:	Implements the helper methods class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Windows
{
    /// <summary>
    /// Helper Methods
    /// </summary>
    public static class HelperMethods
    {
        /// <summary>
        /// Get Current Windows Logged In User
        /// </summary>
        /// <param name="OnlyUserName">Display Only UserName or Full Path</param>
        /// <returns>string</returns>
        public static string GetCurrentWindowsLoggedInUser(bool OnlyUserName)
        {
            var _tmpReturn = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (OnlyUserName == false) { _tmpReturn = _tmpReturn.Split('\\').Last(); }

            return _tmpReturn;
        }
    }
}
