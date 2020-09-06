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

    public class Dependancy
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public string Filename { get; set; }

        [JsonProperty("fileversion", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Version { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }

        [JsonProperty("updatesource", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdateSource { get; set; }

        [JsonProperty("corerequired", NullValueHandling = NullValueHandling.Ignore)]
        public bool CoreRequired { get; set; }

        [JsonProperty("dbversion", NullValueHandling = NullValueHandling.Ignore)]
        public int DBVersion { get; set; }
    }

}
