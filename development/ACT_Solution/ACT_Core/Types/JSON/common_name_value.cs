using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ACT.Core.Types.JSON
{

    public partial class CommonNameValue
    {
        [JsonProperty("act_json_type")]
        public string ActJsonType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sid")]
        public string Sid { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }

        public static CommonNameValue FromJson(string json) => JsonConvert.DeserializeObject<CommonNameValue>(json, ACT.Core.Dynamic.JSON.GetDefaultSettings);

        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Dynamic.JSON.GetDefaultSettings);
    }

    public partial class Datum
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class CommonNameValue
    {

    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
