using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ACT.Plugins.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ACT_APP_ENVIRONMENT_CONFIG_FILE : ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE
    {
        public Guid EnvironmentID { get; set; }
        public string EnvironmentName { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
        public bool ACTConfig { get; set;}
        public bool WebConfig { get; set; }
        public bool OtherConfig { get; set; }
        public string OtherDestination { get; set; }
        public int Version { get; set; }

        public DataTable ToDataTable()
        {
            DataTable _TmpReturn = new DataTable("ACT_APP_ENVIRONMENT_CONFIG_FILE");
            _TmpReturn.Columns.Add("EnvironmentName", typeof(string));
            _TmpReturn.Columns.Add("Environment_ID", typeof(Guid));
            _TmpReturn.Columns.Add("FileName", typeof(string));
            _TmpReturn.Columns.Add("FileData", typeof(string));
            _TmpReturn.Columns.Add("ACTConfig", typeof(bool));
            _TmpReturn.Columns.Add("WebConfig", typeof(bool));
            _TmpReturn.Columns.Add("OtherConfig", typeof(bool));
            _TmpReturn.Columns.Add("Version", typeof(int));

            _TmpReturn.NewRow();
            _TmpReturn.Rows[0][0] = EnvironmentName;
            _TmpReturn.Rows[0][1] = EnvironmentID;
            _TmpReturn.Rows[0][2] = FileName;
            _TmpReturn.Rows[0][3] = FileData;
            _TmpReturn.Rows[0][4] = ACTConfig;
            _TmpReturn.Rows[0][5] = WebConfig;
            _TmpReturn.Rows[0][6] = OtherConfig;
            _TmpReturn.Rows[0][7] = Version;

            return _TmpReturn;
        }
    }
}
