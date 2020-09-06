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
//    /// Config Template Data Class 
//    /// TODO Add Logging of Error Message
//    /// </summary>
//    [DataContract]
//    public class ACT_ConfigTemplate_Data
//    {
//        /// <summary>
//        /// Needed For Serialization
//        /// </summary>
//        public ACT_ConfigTemplate_Data() { }

//        /// <summary>
//        /// just Sender
//        /// </summary>
//        public ACT_ConfigTemplate_Data(ACT_ConfigTemplate sender) { Parent = sender; }

//        /// <summary>
//        /// ACT Config Template Data Constructor
//        /// </summary>
//        /// <param name="ID"></param>
//        /// <param name="sender"></param>
//        public ACT_ConfigTemplate_Data(int ID, ACT_ConfigTemplate sender)
//        {
//            var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.GETTEMPLATEDATA.BYID.Execute.Proc(ID).FirstDataTable_WithRows();

//            if (_TmpResults.Rows.Count != 1)
//            {
//                throw new Exception("Error Locating Data Record");
//            }
//            else
//            {
//                ID = _TmpResults.Rows[0]["ID"].ToInt();
//                Template_ID = _TmpResults.Rows[0]["Template_ID"].ToString();
//                Product_ID = _TmpResults.Rows[0]["Product_ID"].ToString();
//                Name = _TmpResults.Rows[0]["Name"].ToString();
//                DefaultValue = _TmpResults.Rows[0]["DefaultValue"].ToString();
//                Encrypted = _TmpResults.Rows[0]["Encrypted"].ToBool(false);
//                InternalEncrypted = _TmpResults.Rows[0]["InternalEncryption"].ToBool(false);
//                Description = _TmpResults.Rows[0]["Description"].ToString();
//                Tags = _TmpResults.Rows[0]["Tags"].ToString();
//                BaseRequired = _TmpResults.Rows[0]["BaseRequired"].ToBool(false);
//            }
//        }

//        /// <summary>
//        /// Export This Data Record
//        /// </summary>
//        /// <param name="ExportType"></param>
//        /// <param name="EncryptionString"></param>
//        /// <returns></returns>
//        public string Export(ACT.Core.Enums.Serialization.SerializationType ExportType, string EncryptionString = "")
//        {
//            StringBuilder _TmpReturn = new StringBuilder();

//            _TmpReturn.Append("<Setting name=\"" + Name + "\"");
//            if (ExportType == Core.Enums.Serialization.SerializationType.XML)
//            {
//                // <Setting name="EncryptionKey" encrypted="internal" value="C91dpUThaJCVQEui4v24Ugv9mjyF3OW0Zst2TBWfK5pK9R9PKeRn/W0zuesMbVhj433jRvKwSllp5N9zNG0PSQ=="></Setting>
//                string _tmpValue = this.DefaultValue;

//                // Processes the Encryption Part!!
//                if (this.Encrypted)
//                {

//                    // See If Encryption Plugin Is the Same as the Loaded One
//                    var _EncryptionPlugin = ACT.Core.SystemSettings.GetSettingByName("ACT.Core.Interfaces.Security.Encryption.I_Encryption.FullClassName").Value;
//                    var _NewEncryptionPlugin = Parent.ConfigData.First(x => x.Name == "ACT.Core.Interfaces.Security.Encryption.I_Encryption.FullClassName").DefaultValue;
//                    var _NewDLL = Parent.ConfigData.First(x => x.Name == "ACT.Core.Interfaces.Security.Encryption.I_Encryption").DefaultValue;

//                    ACT.Core.Interfaces.Security.Encryption.I_Encryption _EncryptionTool = null;

//                    if (_NewEncryptionPlugin == _EncryptionPlugin)
//                    {
//                        _EncryptionTool = ACT.Core.CurrentCore<ACT.Core.Interfaces.Security.Encryption.I_Encryption>.GetCurrent();
//                    }
//                    else
//                    {
//                        _EncryptionTool = ACT.Core.CurrentCore<ACT.Core.Interfaces.Security.Encryption.I_Encryption>.GetSpecific(new Core.PluginArguments() { FullClassName = _NewEncryptionPlugin, DLLName = _NewDLL, StoreOnce = false, Arguments = null, Loaded = false });
//                    }

//                    if (_EncryptionTool == null)
//                    {
//                        //TODO Log Error
//                        _tmpValue = "Encryption Plugin Not Defined:!!";
//                    }

//                    if (this.InternalEncrypted)
//                    {
//                        _TmpReturn.Append(" encrypted=\"internal\"");
//                        _tmpValue = _EncryptionTool.Encrypt(_tmpValue);
//                    }
//                    else
//                    {
//                        _TmpReturn.Append(" encrypted=\"true\"");
//                        if (EncryptionString == "")
//                        {
//                            //TODO Log Error
//                            _tmpValue = "ERROR_NO_ENCRYPTION_KEY_DEFINED (Need Data With Name: EncryptionKey)";
//                        }
//                        else
//                        {
//                            _tmpValue = _EncryptionTool.Encrypt(_tmpValue, EncryptionString);
//                        }
//                    }
//                }

//                _TmpReturn.Append("<![CDATA[" + _tmpValue + "]]>");

//                _TmpReturn.AppendLine("</Settings>");
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
        
//        /// <summary>
//        /// Update The Plugin Data for The Specified Template
//        /// </summary>
//        /// <param name="MemberID"></param>
//        [OperationContract]
//        public void Update(Guid MemberID)
//        {
//            // TODO ADD ERROR MESSAGE FROM SQL TO LOG
//            if (ID.NullOrEmpty())
//            {
//                var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATEDATA.ADDUPDATE.Execute.Proc(Template_ID.ToGuid(), Product_ID.ToGuid(), MemberID, Name, DefaultValue, Encrypted, InternalEncrypted, Description, Tags, BaseRequired).FirstDataTable_WithRows();

//                if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Inserting Template Data"); }

//                if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
//                {
//                    ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_TemplateData", "Error Inserting Template Data", null, Core.Enums.ErrorLevel.Informational);
//                    throw new Exception("Error Inserting TemplateData");
//                }

//                this.ID = _TmpResults.Rows[0]["ReturnCode"].ToString();
//            }
//            else
//            {
//                var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATEDATA.ADDUPDATE.Execute.Proc(Template_ID.ToGuid(), Product_ID.ToGuid(), MemberID, Name, DefaultValue, Encrypted, InternalEncrypted, Description, Tags, BaseRequired).FirstDataTable_WithRows();

//                if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Updating Template Data"); }

//                if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
//                {
//                    ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_TemplateData", "Error Updating Plugin Data", null, Core.Enums.ErrorLevel.Informational);
//                    throw new Exception("Error Updating TemplateData");
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
//                ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_Data", "Error Deleting Template Data - Record Doesnt Exist", null, Core.Enums.ErrorLevel.Informational);
//                throw new Exception("Error Deleting TemplateData - Record Doesnt Exist");
//            }
//            else
//            {
//                var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATEDATA.DELETE.Execute.Proc(ID.ToInt(), MemberID).FirstDataTable_WithRows();

//                if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Deleting TemplateData"); }

//                if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
//                {
//                    ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_TemplateData", "Error Deleting Template Data", null, Core.Enums.ErrorLevel.Informational);
//                    throw new Exception("Error Deleting TemplateData");
//                }
//            }
//        }

//        #region Private Properties
//        private ACT_ConfigTemplate Parent { get; set; }

//        #endregion

//        #region Properties

//        /// <summary>
//        /// ID
//        /// </summary>
//        [DataMember]
//        public string ID { get; set; }

//        /// <summary>
//        /// Template ID
//        /// </summary>
//        [DataMember]
//        public string Template_ID { get; set; }

//        /// <summary>
//        /// Product ID
//        /// </summary>
//        [DataMember]
//        public string Product_ID { get; set; }

//        /// <summary>
//        /// Name
//        /// </summary>
//        [DataMember]
//        public string Name { get; set; }

//        /// <summary>
//        /// Default Value
//        /// </summary>
//        [DataMember]
//        public string DefaultValue { get; set; }

//        /// <summary>
//        /// Encrypted
//        /// </summary>
//        [DataMember]
//        public bool Encrypted { get; set; }

//        /// <summary>
//        /// Description
//        /// </summary>
//        [DataMember]
//        public string Description { get; set; }

//        /// <summary>
//        /// Tags
//        /// </summary>
//        [DataMember]
//        public string Tags { get; set; }

//        /// <summary>
//        /// Internal Encrypted
//        /// </summary>
//        [DataMember]
//        public bool InternalEncrypted { get; set; }

//        /// <summary>
//        /// Base Required
//        /// </summary>
//        [DataMember]
//        public bool BaseRequired { get; set; }

//        #endregion
//    }

//}
