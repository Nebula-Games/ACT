// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="NameValueCollection.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class NameValueCollectionExtentions.
    /// </summary>
    public static class NameValueCollectionExtentions
    {
        /// <summary>
        /// Converts to dictionary.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> ToDictionary(this NameValueCollection x)
        {
            Dictionary<string, string> _TmpReturn = new Dictionary<string, string>();
            
            foreach (string k in x.Keys)
            {
                try
                {
                    _TmpReturn.Add(k, x[k]);
                }
                catch { }
            }

            return _TmpReturn;
        }
    }
}
