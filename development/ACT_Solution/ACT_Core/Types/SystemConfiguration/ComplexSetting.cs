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


    public class ComplexSetting
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("groupname", NullValueHandling = NullValueHandling.Ignore)]
        public string GroupName { get; set; }

        [JsonProperty("encrypted", NullValueHandling = NullValueHandling.Ignore)]
        public string Encrypted { get; set; }

        [JsonProperty("values", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Values { get; set; }
    }


}
