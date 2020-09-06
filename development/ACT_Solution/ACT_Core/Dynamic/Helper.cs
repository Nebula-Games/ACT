///-------------------------------------------------------------------------------------------------
// file:	Dynamic\Helper.cs
//
// summary:	Implements the helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Dynamic
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Static helper class for dynamic classes. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/23/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public static class Helper
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Export all properties of a dynamic class. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/23/2019. </remarks>
        ///
        /// <param name="d">            A dynamic to process. </param>
        /// <param name="useExisting">  True to use existing. </param>
        ///
        /// <returns>   A List&lt;(string,Type)&gt; </returns>
        ///-------------------------------------------------------------------------------------------------

        public static List<(string, Type)> ExportAllProperties(dynamic d, bool useExisting)
        {
            Type type = d.GetType();

            var _Props = type.GetProperties();

            List<(string, Type)> _tmpReturn = new List<(string, Type)>();

            foreach(var prop in _Props)
            {
                _tmpReturn.Add((prop.Name, prop.PropertyType));
            }

            return _tmpReturn;
        }
    }
}
