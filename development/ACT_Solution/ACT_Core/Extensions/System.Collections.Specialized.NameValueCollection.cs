// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Collections.Specialized.NameValueCollection.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Container Class For Extensions
    /// </summary>
    public static class SystemCollectionsSpecializedNameValueCollection
    {
        /// <summary>
        /// Combines the Collection to a Delimited String
        /// </summary>
        /// <param name="QueryString">The query string.</param>
        /// <param name="ValueDelimeter">The value delimeter.</param>
        /// <param name="ItemDelimeter">The item delimeter.</param>
        /// <returns>System.String.</returns>
        public static string CombineToString(this System.Collections.Specialized.NameValueCollection QueryString, string ValueDelimeter = "=", string ItemDelimeter = "&")
        {
            string _TmpReturn = "";
            foreach (var N in QueryString.Keys)
            {
                _TmpReturn += N + ValueDelimeter + QueryString[N.ToString()] + ItemDelimeter;
            }
            return _TmpReturn.TrimEnd("&");
        }

        /// <summary>
        /// Finds the First Item that is not Null or Blank
        /// </summary>
        /// <param name="QueryString">The query string.</param>
        /// <param name="Keys">The keys.</param>
        /// <returns>System.String.</returns>
        public static string FindFirstNonBlankOrNull(this System.Collections.Specialized.NameValueCollection QueryString, List<string> Keys)
        {
            foreach (string k in Keys)
            {
                if (!QueryString[k].NullOrEmpty()) { return QueryString[k]; }
            }

            return "";
        }

        /// <summary>
        /// AllValues
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> AllValues(this System.Collections.Specialized.NameValueCollection x)
        {
            List<string> _tmpReturn = new List<string>();
            foreach (string k in x.Keys)
            {
                _tmpReturn.Add(x[k]);
            }
            return _tmpReturn;
        }


        /// <summary>
        /// Finds the key.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="Value">The value.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>System.String.</returns>
        public static string FindKey(this System.Collections.Specialized.NameValueCollection x, string Value, bool IgnoreCase = false)
        {
            foreach (string k in x.Keys)
            {
                if (IgnoreCase) { if (x[k].ToString().ToLower() == Value.ToLower()) { return k; } }
                else { if (x[k].ToString() == Value) { return k; } }                
            }

            return null;
        }

        /// <summary>
        /// Converts a Delimited String Into a NameValueCollection
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="ValueDelimeter">The value delimeter.</param>
        /// <param name="ItemDelimeter">The item delimeter.</param>
        /// <returns>System.Collections.Specialized.NameValueCollection.</returns>
        public static System.Collections.Specialized.NameValueCollection ToNameValueCollection(this string x, string ValueDelimeter = "=", string ItemDelimeter = "&")
        {
            System.Collections.Specialized.NameValueCollection _tmpReturn = new System.Collections.Specialized.NameValueCollection();
            var _AllObjects = x.SplitString(ItemDelimeter, StringSplitOptions.RemoveEmptyEntries);
            foreach (var obj in _AllObjects)
            {
                try
                {
                    var _data = obj.SplitString(ValueDelimeter, StringSplitOptions.RemoveEmptyEntries);
                    _tmpReturn.Add(_data[0], _data[1]);
                }
                catch
                {
                    // TODO LOG ERROR
                }
            }
            return _tmpReturn;
        }
    }
}
