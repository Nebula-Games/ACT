// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="JSON.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Net;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Dynamic
{
    /// <summary>
    /// Class JSONConverter.
    /// </summary>
    public static class JSON
    {

        public static Types.JSON.CommonNameValue LoadCommonNameValueData (string FilePath)
        {
            return null;
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Dynamic list to JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/27/2019. </remarks>
        ///
        /// <param name="DynamicList">  List of dynamics. </param>
        ///
        /// <returns>   A string.  Empty String if Parameter is Empty, Null If there is an Error</returns>
        ///-------------------------------------------------------------------------------------------------
        public static string DynamicListToJSON(List<dynamic> DynamicList)
        {
            if (DynamicList == null) { return ""; }

            try { return JsonConvert.SerializeObject(DynamicList); }
            catch { return null; }
        }

        /// <summary>
        /// The s
        /// </summary>
        private static readonly JsonSerializerSettings _s = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        /// <summary>
        /// Gets the get default settings.
        /// </summary>
        /// <value>The get default settings.</value>
        public static JsonSerializerSettings GetDefaultSettings
        {
            get
            {
                var _tmpReturn = new JsonSerializerSettings
                {
                    MetadataPropertyHandling = _s.MetadataPropertyHandling,
                    DateParseHandling = _s.DateParseHandling,
                    Converters = { new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal } },
                };
                return _tmpReturn;
            }
        }

        /// <summary>
        /// Converts to object from json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x.</param>
        /// <param name="optionalSettings">The optional settings.</param>
        /// <returns>T.</returns>
        public static T ConvertToObjectFromJSON<T>(this string x, JsonSerializerSettings optionalSettings = null)
        {
            var _tmpS = _s;
            if (optionalSettings != null) { _tmpS = optionalSettings; }
            return JsonConvert.DeserializeObject<T>(x, _tmpS);
        }

        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="optionalSettings">The optional settings.</param>
        /// <returns>System.String.</returns>
        public static string ToJson<T>(this T self, JsonSerializerSettings optionalSettings = null)
        {
            var _tmpS = _s;
            if (optionalSettings != null) { _tmpS = optionalSettings; }
            return JsonConvert.SerializeObject(self, _tmpS);
        }

    }
}
