using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
//using System.ServiceModel.Web;
using System.Text;
using ACT.Core.Interfaces.WebServices.Configuration;
using ACT.Core.Extensions;
using ERR = ACT.Core.Helper.ErrorLogger;

namespace ACT.Plugins.WebServices.Configuration
{
    ///// <summary>
    ///// Implements the ConfigTemplate Interface For Database Connections
    ///// </summary>
    //[DataContract]
    //public class ACT_ConfigTemplate 
    //{

    //    #region Constructors

    //    /// <summary>
    //    /// Needed For Serialization
    //    /// </summary>
    //    public ACT_ConfigTemplate() { }

    //    /// <summary>
    //    /// Constructor
    //    /// </summary>
    //    /// <param name="ID">Template ID</param>
    //    public ACT_ConfigTemplate(string ID)
    //    {
    //        var _TemplateData = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.GETTEMPLATE.BYID.Execute.Proc(ID.ToGuid()).FirstDataTable_WithRows();
    //        if (_TemplateData == null) { throw new Exception("Data Not Found"); }
    //        var dr = _TemplateData.Rows[0];

    //        this.Description = dr["description"].ToString();
    //        this.ID = dr["ID"].ToString();
    //        this.Member_ID = dr["Member_ID"].ToString();
    //        this.Name = dr["Name"].ToString();
    //        this.Tags = dr["Tags"].ToString();
    //        this.IsPublic = dr["IsPublic"].ToBool(false);
    //        this.IsDefault = dr["IsDefault"].ToBool(false);

    //        _Data = new List<ACT_ConfigTemplate_Data>();
    //        var _DataTable = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.GETTEMPLATEDATA.BYTEMPLATEID.Execute.Proc(ID.ToGuid()).FirstDataTable_WithRows();

    //        foreach (System.Data.DataRow ddr in _DataTable.Rows)
    //        {
    //            var _tmpPlugin = new ACT_ConfigTemplate_Data(ddr["ID"].ToInt(),this);
    //            _Data.Add(_tmpPlugin);
    //        }

    //        _PluginData = new List<ACT_ConfigTemplate_PluginData>();

    //        var _PluginDataTable = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.GETTEMPLATEPLUGINDATA.BYTEMPLATEID.Execute.Proc(ID.ToGuid()).FirstDataTable_WithRows();

    //        foreach (System.Data.DataRow pdr in _PluginDataTable.Rows)
    //        {
    //            var _tmpPlugin = new ACT_ConfigTemplate_PluginData(pdr["ID"].ToInt());
    //            _PluginData.Add(_tmpPlugin);
    //        }
    //    }

    //    #endregion

    //    #region Properties
    //    /// <summary>
    //    /// Description
    //    /// </summary>
    //    [DataMember]
    //    public string Description { get; set; }

    //    /// <summary>
    //    /// Member ID
    //    /// </summary>
    //    [DataMember]
    //    public string Member_ID { get; set; }

    //    /// <summary>
    //    /// Tags
    //    /// </summary>
    //    [DataMember]
    //    public string Tags { get; set; }

    //    /// <summary>
    //    /// Is Public
    //    /// </summary>
    //    [DataMember]
    //    public bool IsPublic { get; set; }

    //    /// <summary>
    //    /// Is Default
    //    /// </summary>
    //    [DataMember]
    //    public bool IsDefault { get; set; }

    //    /// <summary>
    //    /// ID
    //    /// </summary>
    //    [DataMember]
    //    public string ID { get; set; }

    //    /// <summary>
    //    /// ID
    //    /// </summary>
    //    [DataMember]
    //    public string Application_ID { get; set; }

    //    /// <summary>
    //    /// Name
    //    /// </summary>
    //    [DataMember]
    //    public string Name { get; set; }

    //    private List<ACT_ConfigTemplate_PluginData> _PluginData;
    //    private List<ACT_ConfigTemplate_Data> _Data;

    //    /// <summary>
    //    /// Plugin Data 
    //    /// </summary>
    //    [DataMember]
    //    public List<ACT_ConfigTemplate_PluginData> PluginData
    //    {
    //        get { return _PluginData.ToList<ACT_ConfigTemplate_PluginData>(); }
    //        set
    //        {
    //            _PluginData.Clear();
    //            foreach (var itm in value)
    //            {
    //                _PluginData.Add((ACT_ConfigTemplate_PluginData)itm);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Template Data
    //    /// </summary>
    //    [DataMember]
    //    public List<ACT_ConfigTemplate_Data> ConfigData
    //    {
    //        get { return _Data.ToList<ACT_ConfigTemplate_Data>(); }
    //        set
    //        {
    //            _Data.Clear();
    //            foreach (var itm in value)
    //            {
    //                _Data.Add((ACT_ConfigTemplate_Data)itm);
    //            }
    //        }
    //    }
    //    #endregion

    //    #region Methods

    //    /// <summary>
    //    /// Add / Update Template
    //    /// </summary>
    //    /// <param name="SetMemberIDToNull"></param>
    //    /// <param name="MemberID"></param>
    //    [OperationContract]        
    //    public void Update(bool SetMemberIDToNull, Guid? MemberID = null)
    //    {
    //        if (MemberID is null) { MemberID = Member_ID.ToGuid(); }

    //        if (ID.NullOrEmpty())
    //        {
    //            var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.ADDUPDATE.Execute.Proc(ID.ToGuid(), Application_ID.ToGuid(), MemberID, Name, Description, Tags, IsPublic, IsDefault, SetMemberIDToNull).FirstDataTable_WithRows();

    //            if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Inserting Template"); }

    //            if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
    //            {
    //                ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_Template", "Error Inserting Template Data", null, Core.Enums.ErrorLevel.Informational);
    //                throw new Exception("Error Inserting Template");
    //            }

    //            this.ID = _TmpResults.Rows[0]["ReturnCode"].ToString();

    //            // Loop over children                
    //        }
    //        else
    //        {
    //            var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATES.ADDUPDATE.Execute.Proc(ID.ToGuid(), Application_ID.ToGuid(), MemberID, Name, Description, Tags, IsPublic, IsDefault, SetMemberIDToNull).FirstDataTable_WithRows();

    //            if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Updating Template"); }

    //            if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
    //            {
    //                ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_Template", "Error Updating Template", null, Core.Enums.ErrorLevel.Informational);
    //                throw new Exception("Error Inserting Template");
    //            }
    //        }

    //        foreach (var itm in PluginData)
    //        {
    //            ACT_ConfigTemplate_PluginData aCT_ConfigTemplate = (itm as ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_PluginData);
    //            try { aCT_ConfigTemplate.Update(MemberID.Value); } catch { }
    //        }

    //        foreach(var itm in ConfigData)
    //        {
    //            ACT_ConfigTemplate_Data aCT_ConfigTemplate = (itm as ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_Data);
    //            try { aCT_ConfigTemplate.Update(MemberID.Value); } catch { }
    //        }
    //    }

    //    /// <summary>
    //    /// Delete Template
    //    /// <param name="MemberID"></param>
    //    /// </summary>
    //    [OperationContract]
    //    public void Delete(Guid? MemberID = null)
    //    {
    //        if (MemberID is null) { MemberID = Member_ID.ToGuid(); }

    //        var _TmpResults = ACTCloud.Database.SYSTEMCONFIGURATION.TEMPLATE.DELETE.Execute.Proc(ID.ToGuid(), MemberID).FirstDataTable_WithRows();

    //        if (_TmpResults.Rows.Count == 0) { throw new Exception("Error Deleting Template"); }

    //        if (_TmpResults.Rows[0]["OverallSuccess"].ToBool() == false)
    //        {
    //            ERR.LogError("ACT.Plugins.WebServices.Configuration.ACT_ConfigTemplate_Template", "Error Deleting Template", null, Core.Enums.ErrorLevel.Informational);
    //            throw new Exception("Error Deleting Template");
    //        }
    //    }

    //    /// <summary>
    //    /// Export the Template
    //    /// </summary>
    //    /// <param name="ExportType"></param>
    //    /// <returns></returns>
    //    [OperationContract]
    //    public string Export(ACT.Core.Enums.Serialization.SerializationType ExportType)
    //    {
    //        // Look For Encryption Data field
    //        string _EncryptionString = "";

    //        try { _EncryptionString = ConfigData.Where(x => x.Name == "EncryptionKey").First().DefaultValue; }
    //        catch { }
            
    //        StringBuilder _TmpReturn = new StringBuilder();
            

    //        if (ExportType == Core.Enums.Serialization.SerializationType.XML)
    //        {
    //            _TmpReturn.AppendLine("<!-- LOOK FOR REMOVE FOR DISTRIBUTION BEFORE PUBLISHING VERSION-->");
    //            _TmpReturn.AppendLine("<!-- ACT CONFIGURATION FILE FORMAT - VERSION 2.0x - Copyright 2018 Mark Alicz All Rights reserved-->");
    //            _TmpReturn.AppendLine("<Settings>");

    //            foreach(var dataItem in this.ConfigData)
    //            {
    //                _TmpReturn.AppendLine(dataItem.Export(ExportType,_EncryptionString));
    //            }

    //            _TmpReturn.AppendLine("");
    //            _TmpReturn.AppendLine("<!-- ********************************************************************************************* -->");
    //            _TmpReturn.AppendLine("<!-- *********           START Interface Definitions            ********************************** -->");
    //            _TmpReturn.AppendLine("<!-- ********************************************************************************************* -->");
    //            _TmpReturn.AppendLine("");

    //            foreach (var pluginItem in this.PluginData.OrderBy(x=>x.InterfaceName))
    //            {
    //                _TmpReturn.AppendLine(pluginItem.Export(ExportType, _EncryptionString));
    //                _TmpReturn.AppendLine("");
    //            }

    //            _TmpReturn.AppendLine("");
    //            _TmpReturn.AppendLine("<!-- ********************************************************************************************* -->");
    //            _TmpReturn.AppendLine("<!-- *********           END Interface Definitions            ************************************ -->");
    //            _TmpReturn.AppendLine("<!-- ********************************************************************************************* -->");
    //            _TmpReturn.AppendLine("");

    //            _TmpReturn.AppendLine("</Settings>");
    //            return _TmpReturn.ToString();
    //        }
    //        else if (ExportType == Core.Enums.Serialization.SerializationType.JSON)
    //        {
    //            return "Not Supported Yet!";
    //        }
    //        else
    //        {
    //            // TODO LOG ERROR
    //            return "Not Supported";
    //        }
    //    }
    //    #endregion

    //}

}
