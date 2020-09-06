// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Collections.Generic.Dictionary.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class ExtensionsForDictionaries.
    /// </summary>
    public static class ExtensionsForDictionaries
    {

        /// <summary>
        /// Converts to delimitedarray.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="x">The x.</param>
        /// <param name="Delimiter">The delimiter.</param>
        /// <param name="RowDelimeter">The row delimeter.</param>
        /// <returns>System.String.</returns>
        public static string ToDelimitedArray<T, F>(this Dictionary<T, F> x, string Delimiter, string RowDelimeter)
        {
            string _TmpReturn = "";
            foreach (var i in x.Keys)
            {
                _TmpReturn += i.ToString() + Delimiter + x[i].ToString() + RowDelimeter;
            }
            _TmpReturn = _TmpReturn.TrimEnd(RowDelimeter);
            return _TmpReturn;
        }

        /// <summary>
        /// Add Update
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="F"></typeparam>
        /// <param name="x">The x.</param>
        /// <param name="key">The key.</param>
        /// <param name="val">The value.</param>
        public static void AddUpdate<T, F>(this Dictionary<T, F> x, T key, F val)
        {            
            if (x.ContainsKey(key)) { x[key] = val; }
            else { x.Add(key, val); }
        }

        /// <summary>
        /// Add Update Increment
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        public static void AddUpdateIncrement<T>(this Dictionary<T, int> x, T key, int defaultValue = 0)
        {
            if (x.ContainsKey(key)) { x[key] = x[key]++; }
            else { x.Add(key, defaultValue); }
        }

        /// <summary>
        /// Lower The Dictionary Keys
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="x">The x.</param>
        /// <returns>Dictionary&lt;System.String, V&gt;.</returns>
        public static Dictionary<string,V> LowerKeys<V>(this Dictionary<string,V> x)
        {
            Dictionary<string, V> _tmpDic = new Dictionary<string,V>();
            x.ForEach(xx => _tmpDic.Add(xx.Key.ToLower(), xx.Value));            
            return _tmpDic;
        }

       
    }
}
