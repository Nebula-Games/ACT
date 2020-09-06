///-------------------------------------------------------------------------------------------------
// file:	Extensions\StringBuilderExtensions.cs
//
// summary:	Implements the string builder extensions class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// String Builder Extensions
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Append Many Strings
        /// </summary>
        /// <param name="x">String Builder</param>
        /// <param name="arguments">Arguments To Append</param>
        /// <returns></returns>
        public static StringBuilder AppendMany(this StringBuilder x, params string[] arguments)
        {
            arguments.ForEach(xx => x.Append(xx));
            return x;
        }
    }
}
