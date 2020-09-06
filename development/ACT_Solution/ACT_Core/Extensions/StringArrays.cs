using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// String Array Extension
    /// </summary>
    public static class StringArrayExtensions
    {
        /// <summary>
        /// Converts to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> ToList<T>(this T[] x)
        {
            var _tmpReturn = new List<T>();
            _tmpReturn.AddRange(x);
            return _tmpReturn;
        }
    }
}
