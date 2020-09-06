///-------------------------------------------------------------------------------------------------
// file:	Types\Data\mime_types.cs
//
// summary:	Implements the mime types class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ACT.Core.Types.Data
{

    public class MimeTypes
    {
        [JsonProperty("mimetypes", NullValueHandling = NullValueHandling.Ignore)]
        public List<Mimetype> Mimetypes { get; set; }

        public static MimeTypes FromJson(string json) => JsonConvert.DeserializeObject<MimeTypes>(json, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);
                
    }

    public  class Mimetype
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }



}
