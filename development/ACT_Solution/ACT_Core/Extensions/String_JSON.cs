///-------------------------------------------------------------------------------------------------
// file:	Extensions\String_JSON.cs
//
// summary:	Implements the string JSON class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A string json. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/23/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class String_JSON
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that JSON to dynamic. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/23/2019. </remarks>
        ///
        /// <param name="x">    The x to act on. </param>
        ///
        /// <returns>   A dynamic. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static dynamic JSON_To_Dynamic(this string x)
        {
            try
            {
                var _JObj = Newtonsoft.Json.Linq.JObject.Parse(x);

                return _JObj;
            }
            catch (Exception e)
            {
                ACT.Core.Helper.ErrorLogger.VLogError("ACT.Core.Extensions.String_JSON.JSON_to_Dynamic", e.Message, e, Enums.ErrorLevel.Warning);
                return null;
            }
        }
    }
}
