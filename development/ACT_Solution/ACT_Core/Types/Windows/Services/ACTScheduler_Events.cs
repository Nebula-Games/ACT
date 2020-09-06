///-------------------------------------------------------------------------------------------------
// file:	Types\Windows\Services\ACTScheduler_Events.cs
//
// summary:	Implements the act scheduler events class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ACT.Core.Dynamic;

namespace ACT.Core.Types.Windows.Services
{
        
    public class ActSchedulerEvents
    {
        [JsonProperty("events", NullValueHandling = NullValueHandling.Ignore)]
        public List<Event> Events { get; set; }

        public static ActSchedulerEvents FromJson(string json) => JsonConvert.DeserializeObject<ActSchedulerEvents>(json, Dynamic.JSON.GetDefaultSettings);
        public string ToJson() => JsonConvert.SerializeObject(this, Dynamic.JSON.GetDefaultSettings);
    }

    public partial class Event
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("is_system", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSystem { get; set; }

        [JsonProperty("schedule", NullValueHandling = NullValueHandling.Ignore)]
        public string Schedule { get; set; }

        [JsonProperty("log", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Log { get; set; }

        [JsonProperty("methods", NullValueHandling = NullValueHandling.Ignore)]
        public List<Method> Methods { get; set; }
    }

    public partial class Method
    {
        [JsonProperty("fullclassname", NullValueHandling = NullValueHandling.Ignore)]
        public string Fullclassname { get; set; }

        [JsonProperty("parameters", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Parameters { get; set; }
    }



}
