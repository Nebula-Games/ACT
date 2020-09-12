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
    public class ACT_File : ACT.Core.Interfaces.WebServices.Storage.I_File
    {
        [DataMember]
        public int DBID { get; set; }
        [DataMember]
        public byte[] RawData { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string RawLocation { get; set; }
        [DataMember]
        public int LocationID { get; set; }
               
    }
}
