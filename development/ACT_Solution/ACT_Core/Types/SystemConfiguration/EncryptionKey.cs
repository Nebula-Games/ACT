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

    public class Encryptionkey
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string Identifier { get; set; }

        [JsonProperty("sub_identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string SubIdentifier { get; set; }

        [JsonProperty("keyvalue", NullValueHandling = NullValueHandling.Ignore)]
        public string Keyvalue { get; set; }

        [JsonProperty("dateadded", NullValueHandling = NullValueHandling.Ignore)]
        public string DateAdded { get; set; }

        [JsonProperty("datemodified", NullValueHandling = NullValueHandling.Ignore)]
        public string DateModified { get; set; }
    }


}
