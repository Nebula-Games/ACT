// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Web.SessionState.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class SessionStateExtensions.
    /// </summary>
    public static class SessionStateExtensions
    {
#if DOTNETFRAMEWORK
        /// <summary>
        /// Converts to dictionary.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> ToDictionary(this System.Web.SessionState.HttpSessionState x)
        {
            Dictionary<string, string> _TmpReturn = new Dictionary<string, string>();

			foreach(string key in x.Keys)
            {
                try {
                    _TmpReturn.Add(key, x[key].ToString());
                }
                catch
                {
                    _TmpReturn.Add(key, "");
                }
            }

            return _TmpReturn;
        }

#endif
    }

}
