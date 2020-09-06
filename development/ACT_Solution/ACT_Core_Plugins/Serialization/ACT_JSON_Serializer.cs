using System;
using System.Collections.Generic;
using System.Linq;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Plugins.Serialization
{
    /// <summary>
    /// ACT JSON Serialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ACT_JSON_Serializer<T> : ACT.Core.Interfaces.Serialization.I_JSON_Serializer<T>
    {
        /// <summary>
        /// From JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public T FromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json,ACT.Core.Serialization.JsonSerializationSettings.Settings);
        }

        /// <summary>
        /// TO JSON
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);
        }
    }
}
