using System;
using System.Collections.Generic;
using System.Globalization;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces.Security.Authentication;
using Newtonsoft.Json;


/*
 Classes To HOLD The Data Loaded From The System Configuration File
*/

namespace ACT.Core.Types.SystemConfiguration
{

    public class Interface
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("fullclassname", NullValueHandling = NullValueHandling.Ignore)]
        public string FullClassName { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("is_core", NullValueHandling = NullValueHandling.Ignore)]
        public bool Is_Core { get; set; }

        [JsonProperty("dateadded", NullValueHandling = NullValueHandling.Ignore)]
        public string DateAdded { get; set; }

        [JsonProperty("datemodified", NullValueHandling = NullValueHandling.Ignore)]
        public string DateModified { get; set; }

        [JsonProperty("plugins", NullValueHandling = NullValueHandling.Ignore)]
        public List<Plugin> Plugins { get; set; }
    }



}
