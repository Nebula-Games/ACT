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

    public class Environment
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("modifieddate", NullValueHandling = NullValueHandling.Ignore)]
        public string Modifieddate { get; set; }

        [JsonProperty("modifiedby", NullValueHandling = NullValueHandling.Ignore)]
        public string Modifiedby { get; set; }

        [JsonProperty("encryptionkeys", NullValueHandling = NullValueHandling.Ignore)]
        public List<Encryptionkey> Encryptionkeys { get; set; }

        [JsonProperty("basicsettings", NullValueHandling = NullValueHandling.Ignore)]
        public List<BasicSetting> Basicsettings { get; set; }

        [JsonProperty("complexsettings", NullValueHandling = NullValueHandling.Ignore)]
        public List<ComplexSetting> Complexsettings { get; set; }

        [JsonProperty("plugins", NullValueHandling = NullValueHandling.Ignore)]
        public List<Plugin> Plugins { get; set; }
    }

}
