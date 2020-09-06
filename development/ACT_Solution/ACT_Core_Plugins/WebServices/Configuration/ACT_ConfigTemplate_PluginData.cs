using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ACT.Core.Extensions;
using ERR = ACT.Core.Helper.ErrorLogger;

//namespace ACT.Plugins.WebServices.Configuration
//{
//    /// <summary>
//    /// Plugin Data Class - SystemConfiguration Templates
//    /// </summary>
//    public class ACT_ConfigTemplate_PluginData
//    {
//        #region Constructors

//        /// <summary>
//        /// Needed For Serialization
//        /// </summary>
//        public ACT_ConfigTemplate_PluginData() { }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="ID"></param>
//        public ACT_ConfigTemplate_PluginData(int ID)
//        {
//            // TODO
//            var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.GETTEMPLATEPLUGINDATA.BYID.Execute.Proc(ID).FirstDataTable_WithRows();

//            if (_TmpResults.Rows.Count != 1)
//            {
//                throw new Exception("Error Locating PluginData Record");
//            }
//            else
//            {
//                ID = _TmpResults.Rows[0]["ID"].ToInt();
//                Template_ID = _TmpResults.Rows[0]["Template_ID"].ToString();
//                InterfaceName = _TmpResults.Rows[0]["InterfaceName"].ToString();
//                DLLName = _TmpResults.Rows[0]["DLLName"].ToString();
//                FullClassName = _TmpResults.Rows[0]["FullClassName"].ToString();
//                PluginArguments = _TmpResults.Rows[0]["PluginArguments"].ToString();
//                StoreOnce = _TmpResults.Rows[0]["StoreOnce"].ToBool(false);
//            }
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Update The Plugin Data for The Specified Template
//        /// </summary>
//        /// <param name="MemberID"></param>
//        [OperationContract]
//        public void Update(Guid MemberID)
//        {
//            if (ID.NullOrEmpty())
//            {
//                var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATEPLUGINDATA.ADDUPDATE.Execute.Proc(Template_ID.ToGuid(), MemberID, InterfaceName, DLLName, FullClassName, PluginArguments,StoreOnce).FirstDataTable_WithRows();

//                if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Inserting PluginData"); }

//                if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
//                {
//                    ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_PluginData", "Error Inserting Plugin Data", null, Core.Enums.ErrorLevel.Informational);
//                    throw new Exception("Error Inserting PluginData");
//                }

//                this.ID = _TmpResults.Rows[0]["ReturnCode"].ToString();
//            }
//            else
//            {
//                var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATEPLUGINDATA.ADDUPDATE.Execute.Proc(Template_ID.ToGuid(), MemberID, InterfaceName, DLLName, FullClassName, PluginArguments,StoreOnce).FirstDataTable_WithRows();

//                if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Updating PluginData"); }

//                if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
//                {
//                    ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_PluginData", "Error Updating Plugin Data", null, Core.Enums.ErrorLevel.Informational);
//                    throw new Exception("Error Updating PluginData");
//                }
//            }
//        }

//        /// <summary>
//        /// Deletes This Record
//        /// </summary>
//        [OperationContract]
//        public void Delete(Guid MemberID)
//        {
//            if (ID.NullOrEmpty())
//            {
//                ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_PluginData", "Error Deleting Plugin Data - Record Doesnt Exist", null, Core.Enums.ErrorLevel.Informational);
//                throw new Exception("Error Deleting PluginData - Record Doesnt Exist");
//            }
//            else
//            {
//                var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATEPLUGINDATA.DELETE.Execute.Proc(ID.ToInt(), MemberID).FirstDataTable_WithRows();

//                if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Deleting PluginData"); }

//                if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
//                {
//                    ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_PluginData", "Error Deleting Plugin Data", null, Core.Enums.ErrorLevel.Informational);
//                    throw new Exception("Error Deleting PluginData");
//                }
//            }
//        }

//        /// <summary>
//        /// Export This Data Record
//        /// </summary>
//        /// <param name="ExportType"></param>
//        /// <param name="EncryptionString"></param>
//        /// <returns></returns>
//        [OperationContract]
//        public string Export(ACT.Core.Enums.Serialization.SerializationType ExportType, string EncryptionString = "")
//        {
//            StringBuilder _TmpReturn = new StringBuilder();
            
//            if (ExportType == Core.Enums.Serialization.SerializationType.XML)
//            {
//                _TmpReturn.AppendLine("<!-- ** START " + InterfaceName + " ** -->");
//                _TmpReturn.AppendLine("<Setting name=\"" + InterfaceName + "\" value=\"" + DLLName + "\"></Setting>");
//                _TmpReturn.AppendLine("<Setting name=\"" + InterfaceName + ".FullClassName\" value=\"" + FullClassName + "\"></Setting>");
//                _TmpReturn.AppendLine("<Setting name=\"" + InterfaceName + ".StoreOnce\" value=\"" + StoreOnce.ToString().ToLower() + "\"></Setting>");
//                _TmpReturn.AppendLine("<Setting name=\"" + InterfaceName + ".Args\" value=\"" + PluginArguments + "\"></Setting>");
//                _TmpReturn.AppendLine("<!-- ** END " + InterfaceName + " **");
//            }
//            else if (ExportType == Core.Enums.Serialization.SerializationType.JSON)
//            {
//                return "Not Supported Yet";
//            }
//            else
//            {
//                // TODO LOG ERROR
//                return "Not Supported";
//            }

//            return _TmpReturn.ToString();
//        }

//        #endregion

//        #region Properties

//        /// <summary>
//        /// ID (INT)
//        /// </summary>
//        [DataMember]
//        public string ID { get; set; }

//        /// <summary>
//        /// Template ID (GUID)
//        /// </summary>
//        [DataMember]
//        public string Template_ID { get; set; }

//        /// <summary>
//        /// Interface Name
//        /// </summary>
//        [DataMember]
//        public string InterfaceName { get; set; }

//        /// <summary>
//        /// DLL Name
//        /// </summary>
//        [DataMember]
//        public string DLLName { get; set; }

//        /// <summary>
//        /// Full Class Name
//        /// </summary>
//        [DataMember]
//        public string FullClassName { get; set; }

//        /// <summary>
//        /// Plugin Arguments
//        /// </summary>
//        [DataMember]
//        public string PluginArguments { get; set; }

//        /// <summary>
//        /// StoreOnce
//        /// </summary>
//        [DataMember]
//        public bool StoreOnce { get; set; }

//        #endregion
//    }
//}
