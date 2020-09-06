// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="SystemSetting_Instance.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.Security.Encryption;


namespace ACT.Core
{

    /// <summary>
    /// Class SystemSettingsInstance.
    /// </summary>
    public class SystemSettingsInstance
    {
        /// <summary>
        /// Determines if the File Is Valid;
        /// </summary>
        public bool FileIsValid = false;

        /// <summary>
        /// Local Copies Of The Settings That Exist In The Config File
        /// </summary>
        public Dictionary<string, SystemSetting> System_Settings = new Dictionary<string, SystemSetting>();

        #region Private Variables

        /// <summary>
        /// The logger
        /// </summary>
        private I_ErrorLoggable _Logger;
        /// <summary>
        /// The settings file location
        /// </summary>
        private string _SettingsFileLocation;

        #endregion

        #region Public Properties

        /// <summary>
        /// Settings File Location
        /// </summary>
        /// <value>The settings file location.</value>
        public string SettingsFileLocation
        {
            get
            {
                return _SettingsFileLocation;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// This utilizes the Generic Static Class Methods To Load the Values Then It
        /// Copies them into local variables.  Then it reloads the Original Static Class
        /// Settings File Name
        /// </summary>
        /// <param name="SettingsFileLocation">Location Of The SystemConfiguration.xml File (Full File Path i.e. c:\test\systemconfiguration.xml)</param>
        public SystemSettingsInstance(string SettingsFileLocation)
        {

            //  string _TmpFileName = SystemSettings.CurrentSettingFileLocation;
            _SettingsFileLocation = SettingsFileLocation;


            string _XMLData;
            try { _XMLData = GetXMLData(SettingsFileLocation); }
            catch { FileIsValid = false; return; }

            if (ImportXMLData(_XMLData) == false)
            {
                FileIsValid = false;
                return;
            }

            // Settings Should Be Loaded At This Point Now Lets Test Some Core Ones
            try
            {
                _Logger = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent(this);
            }
            catch
            {
                FileIsValid = false;
                return;
            }

            FileIsValid = true;
        }

        #endregion

        /// <summary>
        /// Return a SystemSetting By Name (Returns Value="" If Not Found)
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>SystemSetting.</returns>
        public SystemSetting GetSettingByName(string Name)
        {
            SystemSetting _tmpReturn = new SystemSetting();
            _tmpReturn.Value = "";
            try
            {
                if (System_Settings.Count == 0) { return null; }
                if (System_Settings.ContainsKey(Name) == false)
                {
                    return null;
                }

                _tmpReturn = System_Settings[Name];
            }
            catch
            {
                return _tmpReturn;
            }
            return _tmpReturn;
        }


        /// <summary>
        /// Trys and Imports the DATA.  THIS METHOD IS HARD CODED TO USE DIP_TestResults!!!!
        /// </summary>
        /// <param name="XML">XML as Unicode String</param>
        /// <returns>bool</returns>
        /// <exception cref="Exception">Error Locating Encryption Key.  Possible UnOrdered Configuration File</exception>
        public bool ImportXMLData(string XML)
        {

            if (XML == "") { return false; }

            try
            {
                System.IO.TextReader _Tr = new System.IO.StringReader(XML);
                XElement _Element = XElement.Load(_Tr);
                var _query = from x in _Element.Descendants("Setting") select x;

                foreach (var q in _query)
                {
                    SystemSetting _tmpSetting = new SystemSetting();

                    _tmpSetting.Name = q.Attribute("name").Value;

                    string _Enc = "";
                    try
                    {
                        _Enc = q.Attribute("encrypted").Value.ToLower();
                    }
                    catch { }

                    if (_Enc == "true")
                    {
                        _tmpSetting.EncryptedValue = q.Attribute("value").Value;
                        _tmpSetting.Encrypted = true;
                        _tmpSetting.InternalEncryption = false;


                    }
                    else if (_Enc == "internal")
                    {
                        _tmpSetting.EncryptedValue = q.Attribute("value").Value;
                        _tmpSetting.InternalEncryption = true;
                        _tmpSetting.Encrypted = true;

                    }
                    else
                    {
                        _tmpSetting.Encrypted = false;
                        _tmpSetting.InternalEncryption = false;

                        try
                        {
                            _tmpSetting.Value = q.Attribute("value").Value;
                        }
                        catch
                        {
                            _tmpSetting.Value = q.Value;
                        }
                    }

                    if (System_Settings.ContainsKey(_tmpSetting.Name))
                    {
                        ACT.Core.Helper.ErrorLogger.LogError("DIP.SystemSetting_Instance.ImportXMLData", "Setting Already Found" + _tmpSetting.Name, "", null, ErrorLevel.Warning);
                    }
                    else
                    {
                        System_Settings.Add(_tmpSetting.Name, _tmpSetting);
                    }

                }

            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("DIP.SystemSetting_Instance.ImportXMLData", "Error Importing XML Data", "", ex, ErrorLevel.Warning);

                return false;
            }

            // NOW Unencrypt the Data If we need to
            // TODO REVIEW
            foreach (var S in System_Settings.Values)
            {
                if (S.Encrypted)
                {

                    if (S.InternalEncryption)
                    {
                        try
                        {
                            I_Encryption _Encryptor = CurrentCore<I_Encryption>.GetCurrent();
                            S.Value = _Encryptor.Decrypt(S.EncryptedValue);
                        }
                        catch (Exception ex)
                        {
                            ACT.Core.Helper.ErrorLogger.LogError("DIP.SystemSetting_Instance.ImportXMLData", "Error Importing XML Data: Unencrypting: " + S.Name, "", ex, ErrorLevel.Warning);
                            S.Value = "";
                        }
                    }
                    else
                    {
                        if (System_Settings.Keys.Contains<string>("EncryptionKey") == false)
                        {
                            ACT.Core.Helper.ErrorLogger.LogError("DIP.SystemSetting_Instance.ImportXMLData", "Error Locating Encryption Key.  Possible UnOrdered Configuration File", "Error Locating Encryption Key.  Possible UnOrdered Configuration File", null, ErrorLevel.Warning);
                            throw new Exception("Error Locating Encryption Key.  Possible UnOrdered Configuration File");
                        }
                        try
                        {
                            I_Encryption _Encryptor = CurrentCore<I_Encryption>.GetCurrent();
                            S.Value = _Encryptor.Decrypt(S.EncryptedValue, System_Settings["EncryptionKey"].Value);

                        }
                        catch (Exception ex)
                        {
                            ACT.Core.Helper.ErrorLogger.LogError("DIP.SystemSetting_Instance.ImportXMLData", "Error Decrypting Key", "Name: " + S.Name, ex, ErrorLevel.Warning);
                            S.Value = "";
                        }
                    }
                }
            }

            if (System_Settings["VerboseDebugging"].Value.ToLower() == "true")
            {
                ACT.Core.Helper.ErrorLogger.LogError("DIP.SystemSetting_Instance.ImportXMLData", "System Settings Loaded: " + System_Settings.Count().ToString(), "", null, ErrorLevel.Informational);
            }

            return true;
        }

        /// <summary>
        /// Exports The Current XML Data Into A String (XML)
        /// </summary>
        /// <returns>System.String.</returns>
        public string ExportXMLData()
        {
            string _tmpReturn = "<Settings>\n\r";

            foreach (SystemSetting _TmpSetting in System_Settings.Values)
            {
                _tmpReturn = _tmpReturn + _TmpSetting.ExportXMLData() + "\n\r";
            }

            _tmpReturn = _tmpReturn + "</Settings>";

            return _tmpReturn;
        }

        /// <summary>
        /// Reads the XML Configuration Data Into A String
        /// </summary>
        /// <param name="XMLFilePath">The XML file path.</param>
        /// <returns>XML On Success</returns>
        /// <exception cref="Exception">
        /// XML File Does Not Exist: " + XMLFilePath
        /// or
        /// Error Loading XML Data
        /// </exception>
        public string GetXMLData(string XMLFilePath)
        {
            if (System.IO.File.Exists(XMLFilePath) == false)
            {
                try
                {
                    ACT.Core.Helper.ErrorLogger.LogError("XML File Does Not Exist: " + XMLFilePath, "", "", null, ErrorLevel.Warning);
                }
                catch { }
                throw new Exception("XML File Does Not Exist: " + XMLFilePath);
            }

            string _R;

            try
            {
                var OP = System.IO.File.OpenText(XMLFilePath);
                _R = OP.ReadToEnd();
                OP.Close();
            }
            catch (Exception ex)
            {
                try
                {
                    ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.SystemSetting_Instance.GetXMLData", "Error Loading XML Data: " + XMLFilePath, "", ex, ErrorLevel.Warning);
                }
                catch { }

                throw new Exception("Error Loading XML Data", ex);
            }

            return _R;
        }

        /// <summary>
        /// Saves The Data Into The SettingsXML File
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Save()
        {
            try
            {
                if (_SettingsFileLocation != "")
                {
                    System.IO.File.Copy(_SettingsFileLocation, _SettingsFileLocation.Replace(".xml", "") + "_" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString() + ".xml", true);
                    System.IO.File.WriteAllText(_SettingsFileLocation, ExportXMLData());
                }
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.SystemSetting_Instance.Save", "Error Saving File: " + _SettingsFileLocation, "", ex, ErrorLevel.Warning);
                return false;
            }

            return true;
        }
    }
}
