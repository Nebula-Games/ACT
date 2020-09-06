// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Collections.Generic.List.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Extensions for List objects
    /// </summary>
    public static class ExtensionsForLists
    {

        /// <summary>
        /// Convert The List T To a String
        /// </summary>
        /// <typeparam name="T">Type Of List Objects</typeparam>
        /// <param name="x">List To Convert</param>
        /// <param name="Delimiter">Delimieter To Use</param>
        /// <returns>A Delimited String.  I.e. listfirstobject,listsecondobject   DOES NOT CHECK FOR VALID DELIMETERS YOU NEED TO ENSURE THAT YOU PASS IN A UNIQUE DELIMETER</returns>
        public static string ToDelimitedArray<T>(this List<T> x, string Delimiter)
        {
            string _TmpReturn = "";
            foreach (var i in x)
            {
                _TmpReturn += i.ToString() + Delimiter;
            }
            _TmpReturn = _TmpReturn.TrimEnd(Delimiter);
            return _TmpReturn;
        }

        /// <summary>
        /// Clones the specified list to clone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone">The list to clone.</param>
        /// <returns>IList&lt;T&gt;.</returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Taks a List. (i.E of Directories) and minifies them to remove the most common string in all.
        /// </summary>
        /// <param name="listToMinify">The list to minify.</param>
        /// <returns>System.ValueTuple&lt;System.String, List&lt;System.String&gt;&gt;.</returns>
        public static (string, List<string>) ReduceToLeastCommon(this List<string> listToMinify)
        {
            string _MostCommon = "";
            for (int inc = 0; inc < listToMinify.Select(x => x.Length).Min(); inc++)
            {
                string _currentChar = "";
                foreach (var itm in listToMinify)
                {
                    if (_currentChar == "") { _currentChar = itm[inc].ToString(); }
                    else
                    {
                        if (itm[inc].ToString() == _currentChar) { continue; }
                        else
                        {
                            _currentChar = "exit";
                            break;
                        }
                    }
                }
                if (_currentChar == "exit")
                {
                    break;
                }
                else
                {
                    _MostCommon = _MostCommon + _currentChar;
                }
            }
            List<string> _tmpReturn = new List<string>();
            foreach(var itm in listToMinify)
            {
                _tmpReturn.Add(itm.Replace(_MostCommon, ""));
            }
            return (_MostCommon, _tmpReturn);
        }

        /// <summary>
        /// Sort the List with Nulls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToSort">The list to sort.</param>
        /// <param name="Asc">if set to <c>true</c> [asc].</param>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>List&lt;T&gt;.</returns>
        /// <exception cref="Exception">Please Complete Me</exception>
        public static List<T> SortWithNull<T>(this List<T> listToSort, bool Asc, string PropertyName)
        {
            throw new Exception("Please Complete Me");
        }
    }
}
