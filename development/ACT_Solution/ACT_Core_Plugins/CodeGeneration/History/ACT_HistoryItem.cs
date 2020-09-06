using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums.Serialization;
using ACT.Core.Interfaces.CodeGeneration.History;
using ACT.Core.Extensions;

namespace ACT.Plugins.CodeGeneration.History
{
    public class ACT_HistoryItem: ACT_Core, I_HistoryItem
    {
        public string ProjectName { get; set; }
        public string FileName { get; set; }
        public string IncrementalCount { get; set; }
        public string Password { get; set; }
        public string Date { get; set; }
        public string OriginalFileLocation { get; set; }
        
        public DateTime ActualDateTime { get { return Date.ToDateTime(); } }

        public void Deserialize(SerializationType Type, object Data)
        {
            throw new NotImplementedException();
        }

        public string Serialize(SerializationType Type)
        {
            if (Type== SerializationType.XML)
            {
                string _xml = "<item projectname=\"" + ProjectName + "\" filename=\"" + FileName + "\" password=\"" + Password + "\" date=\"" + Date + "\" incrementalcount=\"" + IncrementalCount + "\" originalfilelocation=\"" + OriginalFileLocation + "\"></item>" + Environment.NewLine;
                return _xml;
            }

            return "";
        }

    }
}
