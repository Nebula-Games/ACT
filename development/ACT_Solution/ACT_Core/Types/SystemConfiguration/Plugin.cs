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

    public class Plugin
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        [JsonProperty("application_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Application_ID { get; set; }

        [JsonProperty("application_environment_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Application_Environment_ID { get; set; }

        [JsonProperty("system_interface_id", NullValueHandling = NullValueHandling.Ignore)]
        public string System_Interface_ID { get; set; }

        [JsonProperty("dependancy_id", NullValueHandling = NullValueHandling.Ignore)]
        public string Dependancy_ID { get; set; }

        [JsonProperty("fullclassname", NullValueHandling = NullValueHandling.Ignore)]
        public string FullClassName { get; set; }

        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("storeonce", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Storeonce { get; set; }

        [JsonProperty("arguments", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Arguments { get; set; }

        [JsonProperty("checksum", NullValueHandling = NullValueHandling.Ignore)]
        public string Checksum { get; set; }

        [JsonProperty("licensekey", NullValueHandling = NullValueHandling.Ignore)]
        public string Licensekey { get; set; }

        [JsonProperty("order", NullValueHandling = NullValueHandling.Ignore)]
        public long? Order { get; set; }

        [JsonProperty("dateadded", NullValueHandling = NullValueHandling.Ignore)]
        public string DateAdded { get; set; }

        [JsonProperty("datemodified", NullValueHandling = NullValueHandling.Ignore)]
        public string DateModified { get; set; }
    }






}
