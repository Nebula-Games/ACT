using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.CustomAttributes;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.Security.Encryption;
using ACT.Core.Extensions;

namespace ACT.Plugins.CodeGeneration
{
    /// <summary>
    /// This is the ACT Implementation of the Code Generation Settings File
    /// </summary>
    public class ACT_CodeGenerationSettings : I_CodeGenerationSettings
    {
        /// <summary>
        /// Location of the settings file to USE When generating CODE
        /// This contains all of the information in this file as well as all of the templates
        /// connections and everything .  THIS WILL OVERRIDE THE Configuration file in the Current
        /// Base Directory.  LEAVE BLANK IF YOU WANT TO USE THE FILE IN THE BIN FOLDER WITH THIS
        /// DLL
        /// </summary>
        public string SettingsFileLocation { get; set; }

        #region Database Connection String

        /// <summary>
        /// Internal Encrypted Connection String
        /// </summary>
        [Encrypted(true)]
        protected internal string _ConnectionString;

        /// <summary>
        /// Database Connection String Resolved From Database Connection Name
        /// </summary>
        [Encrypted(true)]
        public string DatabaseConnectionString
        {
            get
            {
                if (_ConnectionString.NullOrEmpty())
                {
                    var _ConnectionSetting = ACT.Core.SystemSettings.GetSettingByName(DatabaseConnectionName);
                    if (_ConnectionSetting.Encrypted == false || _ConnectionSetting.InternalEncryption == false)
                    {
                        ACT.Core.Helper.ErrorLogger.LogError(this, "Error - Connection Is Not Encrypted", null, Core.Enums.ErrorLevel.Warning);
                        _ConnectionString = _ConnectionSetting.Value;
                    }
                    else
                    {
                        if (_ConnectionSetting.InternalEncryption == false)
                        {
                            ACT.Core.Helper.ErrorLogger.LogError(this, "Error - Connection Is Encrypted Using Non Supported Encryption (NOT INTERNAL)", null, Core.Enums.ErrorLevel.Warning);
                            throw new System.Exception("Unsupported Encryption Method on Connection String");
                        }

                        _ConnectionString = _ConnectionSetting.EncryptedValue;
                    }

                    if (_ConnectionSetting.InternalEncryption == true)
                    {
                        var Encryption = ACT.Core.CurrentCore<I_Encryption>.GetCurrent();
                        return Encryption.Decrypt(_ConnectionString);
                    }
                    else
                    {
                        return _ConnectionString;
                    }
                }

                return _ConnectionString;             
            }
            set
            {
                var Encryption = ACT.Core.CurrentCore<I_Encryption>.GetCurrent();
                _ConnectionString = Encryption.Encrypt(value);
            }
        }

        /// <summary>
        /// Database Connection Name
        /// </summary>
        public string DatabaseConnectionName { get; set; }

        #endregion

        /// <summary>
        /// Language to Generate The code Into (Search Plugins For Matching Language)
        /// ACT Built in ONLY Supports CSharp
        /// </summary>
        public ACT.Core.Enums.Code.Language OutputLanguage { get { return Core.Enums.Code.Language.CSharp; } set { throw new Exception("Not supported"); } }

        #region Code Generation Boolean Options

        /// <summary>
        /// Compile the Code into the DLLName Specified
        /// </summary>
        public bool CompileCode { get; set; }

        /// <summary>
        /// Output a CS Solution and Project
        /// </summary>
        public bool OutputSolutionWithProject { get; set; }

        /// <summary>
        /// Generate the BASE LAYER CODE
        /// </summary>
        public bool GenerateBaseLayer { get; set; }

        /// <summary>
        /// Generate USER LAYER
        /// </summary>
        public bool GenerateUserLayer { get; set; }

        /// <summary>
        /// Generate Stored Procedures
        /// </summary>
        public bool GenerateStoredProcedures { get; set; }

        /// <summary>
        /// Generate Web Services Code
        /// </summary>
        public bool GenerateWebServices { get; set; }

        /// <summary>
        /// Generate View Access
        /// </summary>
        public bool GenerateViewAccess { get; set; }

        /// <summary>
        /// Notify Specified Administrators when Code Is Generated 
        /// Uses ADMINS Setting in the SystemSettings Administrators Delimited by the default delimiter
        /// </summary>
        public bool NotifyWhenCodeIsGenerated { get; set; }

        /// <summary>
        /// Notify Specified Administrators when ProjectCode Is Updated
        /// Uses ADMINS Setting in the SystemSettings Administrators Delimited by the default delimiter
        /// </summary>
        public bool NotifyWhenProjectCodeIsUpdated { get; set; }

        /// <summary>
        /// Backup the project Code before Deployment 
        /// </summary>
        public bool BackupProjectCodeBeforeDeployment { get; set; }

        /// <summary>
        /// Namespace Driven Procedures. Creates Multiple Files to Help Organize Stored Procedures
        /// </summary>
        public bool NamespaceDrivenProcedures { get; set; }

        #endregion

        /// <summary>
        /// Delimiter for Creating Additional Name Spaces 
        /// </summary>
        public string NamespaceDrivenProceduresDelimiter { get; set; }

        /// <summary>
        /// DLL Name to Compile the DLL to
        /// </summary>
        public string DLLName { get; set; }

        /// <summary>
        /// Default Namespace
        /// </summary>
        public string NameSpace { get; set; }

        /// <summary>
        /// Base Code Namespace
        /// </summary>
        public string BaseCodeNameSpace { get; set; }

        /// <summary>
        /// Default Namespace
        /// </summary>
        public string UserCodeNameSpace { get; set; }

        /// <summary>
        /// Output directory
        /// </summary>
        public string RootOutputDirectory { get; set; }

        /// <summary>
        /// Location of Visual Studio Project (ROOT PATH)
        /// </summary>
        public string ProjectUpdateDirectory { get; set; }

        /// <summary>
        /// Base Folder PATH to use when writing generated BASE files
        /// </summary>
        public string BaseFolderPath { get; set; }

        /// <summary>
        /// User Folder PATH to use when writing generated USER files
        /// </summary>
        public string UserFolderPath { get; set; }

        /// <summary>
        /// Folder Path To 
        /// </summary>
        public string StoredProcedureNamespaceMethodPath { get; set; }

        /// <summary>
        /// Name of the Project Used for Displaying in the System and Generating the Code Project
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Name of the Solution
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// Max Age In Days Since Last Edit
        /// </summary>
        public int ObjectAgeMax { get; set; }
    }
}
