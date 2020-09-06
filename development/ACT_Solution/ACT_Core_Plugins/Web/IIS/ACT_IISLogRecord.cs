using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Web.IIS;

namespace ACT.Plugins.Web.IIS
{
    /// <summary>
    /// 
    /// </summary>
    public class ACT_IISLogRecord : I_IISLogRecord
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        public ACT_IISLogRecord(string FileName)
        {
            if (System.IO.File.Exists(FileName) == true)
            {
                foreach (string ln in System.IO.File.ReadLines(FileName))
                {
                    ACT_IISHit _HData = new ACT_IISHit(ln);
                    HitData.Add(_HData);
                }
            }
            else
            {
                throw new System.IO.FileNotFoundException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<I_IISHit> HitData { get; set; }
        
        /// <summary>
        /// Saves the Data to the Database
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="ACT_ApplicationID"></param>
        /// <param name="ACT_DomainID"></param>
        public void SaveToDatabase(string connectionName, Guid ACT_ApplicationID, Guid ACT_DomainID)
        {
            using (var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent())
            {
                _DataAccess.Open(ACT.Core.SystemSettings.GetSettingByName("connectionName").Value);

                System.Data.DataTable _DataInsertTo = new System.Data.DataTable();

                _DataInsertTo.TableName = "ACT_IIS_LOG_DATA";
                _DataInsertTo.Columns.Add("ID", typeof(Guid));
                _DataInsertTo.Columns.Add("Application_ID", typeof(Guid));
                _DataInsertTo.Columns.Add("Domain_ID", typeof(Guid));
                _DataInsertTo.Columns.Add("HitDate", typeof(DateTime));
                _DataInsertTo.Columns.Add("SiteName", typeof(String));
                _DataInsertTo.Columns.Add("ServerName", typeof(String));
                _DataInsertTo.Columns.Add("ServerIP", typeof(String));
                _DataInsertTo.Columns.Add("Method", typeof(String));
                _DataInsertTo.Columns.Add("UriStem", typeof(String));
                _DataInsertTo.Columns.Add("Resource", typeof(String));
                _DataInsertTo.Columns.Add("UriQuery", typeof(String));
                _DataInsertTo.Columns.Add("Port", typeof(String));
                _DataInsertTo.Columns.Add("UserName", typeof(String));
                _DataInsertTo.Columns.Add("ClientIP", typeof(String));
                _DataInsertTo.Columns.Add("CSVersion", typeof(String));
                _DataInsertTo.Columns.Add("UserAgent", typeof(String));
                _DataInsertTo.Columns.Add("Cookie", typeof(String));
                _DataInsertTo.Columns.Add("Referrer", typeof(String));
                _DataInsertTo.Columns.Add("HostHeaderName", typeof(String));
                _DataInsertTo.Columns.Add("ScStatus", typeof(String));
                _DataInsertTo.Columns.Add("ScSubstatus", typeof(String));
                _DataInsertTo.Columns.Add("ScWin32Status", typeof(String));
                _DataInsertTo.Columns.Add("BytesSent", typeof(String));
                _DataInsertTo.Columns.Add("BytesReceived", typeof(String));
                _DataInsertTo.Columns.Add("TimeTaken", typeof(String));
                _DataInsertTo.Columns.Add("DateAdded", typeof(String));
                _DataInsertTo.Columns.Add("DateModified", typeof(String));


                foreach (var HD in HitData)
                {
                    _DataInsertTo.Rows.Add(new object[]
                    {
                    Guid.NewGuid(), ACT_ApplicationID, ACT_DomainID, HD.HitDate, HD.SiteName, HD.ServerName, HD.ServerIP, HD.Method, HD.UriRoot, HD.Resource, HD.UriQuery, HD.PortNumber, HD.UserName, HD.ClientIPAddress,
                    HD.CSVersion, HD.UserAgent, HD.Cookie, HD.Referrer, HD.HostHeaderName, HD.ScStatus, HD.ScSubstatus, HD.ScWin32Status, HD.BytesSent, HD.BytesReceived, HD.TimeTaken,DateTime.Now, DateTime.Now
                    });
                }

                var _TmpReturn = _DataAccess.ExecuteBulkInsert(_DataInsertTo, _DataInsertTo.TableName, System.Data.SqlClient.SqlBulkCopyOptions.TableLock);
                _DataAccess.Dispose();
            }
        }

    }
}
