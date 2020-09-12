using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ServiceModel.Web;
using System.Text;

namespace ACT.Plugins.WebServices.Storage
{
    [DataContract]
    public class ACT_Location : ACT.Core.Interfaces.WebServices.Storage.I_Location
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string RawLocation { get; set; }
        [DataMember]
        public List<string> Files { get; set; }
        [DataMember]
        public List<string> ChildLocationNames { get; set; }
        [DataMember]
        public List<string> ChildLocationRaw { get; set; }
        [DataMember]
        public bool VersionControl { get; set; }
    }
}
