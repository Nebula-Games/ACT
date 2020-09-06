// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-25-2019
// ***********************************************************************
// <copyright file="TemplateEngine.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using ACT.Core.Exceptions;

using ACT.Core.Extensions;

namespace ACT.Core.TemplateEngine
{

    /// <summary>
    /// Class TemplateEngine.
    /// </summary>
    public static class TemplateEngine
    {
        #region Internal Variables
        /// <summary>
        /// The connected
        /// </summary>
        internal static bool _Connected;
        /// <summary>
        /// The connection name
        /// </summary>
        internal static string _ConnectionName;
        /// <summary>
        /// The base directory
        /// </summary>
        internal static string _BaseDirectory;
        #endregion

        /// <summary>
        /// All The Template Data Loaded Into Memory
        /// </summary>
        public static List<Template> AllTemplateData = new List<Template>();

        /// <summary>
        /// Loaded Packages
        /// </summary>
        public static Dictionary<string, TemplatePackage> LoadedPackages = new Dictionary<string, TemplatePackage>();

        
        /// <summary>
        /// Get the Text Template
        /// </summary>
        /// <param name="Path">The path.</param>
        /// <param name="File">The file.</param>
        /// <returns>System.String.</returns>
        public static string GetTextTemplate(string Path, string File)
        {
            try
            {
                return System.IO.File.ReadAllText(Path.EnsureDirectoryFormat() + File);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Allows Values To Be Parsed Using ACT Notation #VARIABLE#.
        /// Session = SES-
        /// Querystring = QES-
        /// Example: "/login/default.aspx?LoginID=#SES-LoginID#
        /// would be like "/login/default.aspx?LoginID=" + Session["LoginID"].ToString()
        /// </summary>
        /// <param name="InputString">The input string.</param>
        /// <param name="SessionState">State of the session.</param>
        /// <param name="QueryStringData">The query string data.</param>
        /// <returns>System.String.</returns>
        /// <value> The navigation URL. </value>
        public static string SimpleParse(string InputString, Dictionary<string,string> SessionState, System.Collections.Specialized.NameValueCollection QueryStringData)
        {
            string _InputString = InputString;

            int _SearchIndex = -1;

            try
            {
                _SearchIndex = _InputString.IndexOf("#SES-");

                while (_SearchIndex > -1)
                {
                    int _EndIndex = _InputString.IndexOf("#", _SearchIndex + 5);
                    string _SessionVarName = _InputString.Substring(_SearchIndex + 5, _EndIndex - _SearchIndex - 5);
                    _InputString = _InputString.Replace("#SES-" + _SessionVarName + "#", SessionState[_SessionVarName].ToString());
                    _SearchIndex = _InputString.IndexOf("#SES-");
                }
            }
            catch { }

            try
            {
                _SearchIndex = _InputString.IndexOf("#QES-");

                while (_SearchIndex > -1)
                {
                    int _EndIndex = _InputString.IndexOf("#", _SearchIndex + 5);
                    string _QueryStringVarName = _InputString.Substring(_SearchIndex + 5, _EndIndex - _SearchIndex - 5);
                    _InputString = _InputString.Replace("#QES-" + _QueryStringVarName + "#", QueryStringData[_QueryStringVarName]);
                    _SearchIndex = _InputString.IndexOf("#QES-");
                }
            }
            catch { }

            return _InputString;
        }


        #region Database Methods


        /// <summary>
        /// Updates The Database With The Latest Version Of The Templates
        /// </summary>
        /// <param name="DBConnectionName">Name of the database connection.</param>
        /// <param name="BaseDirectory">The base directory.</param>
        public static void UpdateDatabase(string DBConnectionName, string BaseDirectory)
        {
            AllTemplateData.Clear();
            AllTemplateData = new List<Template>();

            var _BaseDirectories = System.IO.Directory.GetDirectories(BaseDirectory);
            string _ConnString = ACT.Core.SystemSettings.GetSettingByName(DBConnectionName).Value;

            foreach (string _SubType in _BaseDirectories)
            {
                foreach (string Name in System.IO.Directory.GetFiles(_SubType.EnsureDirectoryFormat()))
                {
                    List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();
                    _Params.Add(new System.Data.SqlClient.SqlParameter("Name", Name.GetFileNameFromFullPath()));
                    _Params.Add(new System.Data.SqlClient.SqlParameter("TypeName", _SubType.Substring(_SubType.LastIndexOf("\\") + 1)));
                    _Params.Add(new System.Data.SqlClient.SqlParameter("BaseDirectory", BaseDirectory.EnsureDirectoryFormat()));
                    _Params.Add(new System.Data.SqlClient.SqlParameter("Data", System.IO.File.ReadAllText(Name)));

                    var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
                    _DataAccess.Open(_ConnString);

                    var _Results = _DataAccess.RunCommand("TEMPLATE_WEB_ADDUPDATE", _Params.ToList<System.Data.IDataParameter>(), false, System.Data.CommandType.StoredProcedure);

                    if (_Results.FirstQueryHasExceptions == true)
                    {
                        ACT.Core.Helper.ErrorLogger.LogError(null, "Error Adding Web Template To Database", _Results.Exceptions[0], Enums.ErrorLevel.Warning);
                    }
                    _DataAccess.Dispose();
                }
            }
        }

        /// <summary>
        /// Gets the template data from the Database Connection Name
        /// </summary>
        /// <param name="DBConnectionName">Name of the database connection.</param>
        /// <param name="TemplateType">Type of the template.</param>
        /// <param name="TemplateName">Name of the template.</param>
        /// <param name="Version">The version.</param>
        /// <returns>Template.</returns>
        public static Template GetTemplateDataDB(string DBConnectionName, ACT.Core.Enums.TemplateEngine.TemplateType TemplateType, string TemplateName, int Version = 0)
        {
            var _AllCachedData = AllTemplateData.Find(x => x.Template_Type == TemplateType && x.FileName == TemplateName);

            if (_AllCachedData == null)
            {
                string _ConnString = ACT.Core.SystemSettings.GetSettingByName(DBConnectionName).Value;

                string type = TemplateType.ToString();

                List<System.Data.SqlClient.SqlParameter> _Params = new List<System.Data.SqlClient.SqlParameter>();
                _Params.Add(new System.Data.SqlClient.SqlParameter("Name", TemplateName));
                _Params.Add(new System.Data.SqlClient.SqlParameter("SubType", type));
                _Params.Add(new System.Data.SqlClient.SqlParameter("VersionNumber", Version));

                var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
                _DataAccess.Open(_ConnString);

                var _Results = _DataAccess.RunCommand("TEMPLATE_WEB_GETBYNAMETYPE", _Params.ToList<System.Data.IDataParameter>(), true, System.Data.CommandType.StoredProcedure);

                if (_Results.FirstQueryHasExceptions == true) { return null; }
                if (_Results.FirstDataTable_WithRows() == null) { return null; }
                _DataAccess.Dispose();
                _DataAccess = null;

                _AllCachedData = new Template()
                {
                    BaseDirectory = _Results.GetValue("BaseDirectory").ToString(),
                    FileName = _Results.GetValue("Name").ToString(),
                    Data = _Results.GetValue("Data").ToString(),
                    Template_Type = (ACT.Core.Enums.TemplateEngine.TemplateType)Enum.Parse(typeof(ACT.Core.Enums.TemplateEngine.TemplateType), _Results.GetValue("TypeName").ToString(), true)// == "SurveyStandard") ? ACT.Core.Enums.TemplateEngine.TemplateType.SIMPLE : ACT.Core.Enums.TemplateEngine.TemplateType.NGT
                };
            }

            AllTemplateData.Add(_AllCachedData);
            return _AllCachedData;
        }

        /// <summary>
        /// TESTS For ACT Integration Status
        /// </summary>
        /// <param name="ConnectionName">Name of the connection.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">Error Opening Database.</exception>
        public static bool TestACTIntegration(string ConnectionName)
        {
            var _CurrentDataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(ConnectionName).Value;

            if (_CurrentDataAccess.Open(_ConnectionString) == false)
            {
                throw new Exception("Error Opening Database.");
            }

            var _Results = _CurrentDataAccess.RunCommand("Select * From ACT_FEATURES", new List<System.Data.IDataParameter>(), true, System.Data.CommandType.Text);

            if (_Results.Exceptions[0] != null) { _CurrentDataAccess.Dispose(); return false; }
            else
            {
                if (_Results.FirstDataTable_WithRows() != null)
                {
                    if (_Results.FirstDataTable_WithRows().Rows.Count == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// Deploy the database and Stored Procedures
        /// </summary>
        /// <param name="ConnectionName">Name of the connection.</param>
        /// <param name="DropAndRecreate">if set to <c>true</c> [drop and recreate].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">Error Opening Database.</exception>
        public static bool DeployDatabase(string ConnectionName, bool DropAndRecreate)
        {
            string _CreateCommand = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Data\\WebTemplatesSCHEMA.txt");
            string _ProcA = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Data\\ACT_GET_WEB_TEMPLATE.txt");
            string _ProcB = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Data\\TEMPLATE_WEB_ADDUPDATE.txt");


            string[] _CreateCommands = _CreateCommand.SplitString("###GOSTATEMENT###", StringSplitOptions.RemoveEmptyEntries);

            var _CurrentDataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(ConnectionName).Value;

            if (_CurrentDataAccess.Open(_ConnectionString) == false)
            {
                throw new Exception("Error Opening Database.");
            }

            for (int x = 0; x < _CreateCommands.Length; x++)
            {
                _CurrentDataAccess.RunCommand(_CreateCommands[x], new List<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);
            }

            _CurrentDataAccess.RunCommand(_ProcA, new List<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);
            _CurrentDataAccess.RunCommand(_ProcB, new List<System.Data.IDataParameter>(), false, System.Data.CommandType.Text);

            _CurrentDataAccess.Dispose();
            return false;

        }

        #endregion

        #region Initialization Methods (CALL FIRST)

        /// <summary>
        /// This configures the template manager to be used with a website.
        /// </summary>
        /// <param name="BaseDirectory">Physical Path To Template Directory</param>
        /// <param name="TemplateType">Sub Directory Name Under Template Directory.</param>
        /// <param name="ForceReload">Reload the Template Regardless of CACHE</param>
        /// <exception cref="System.IO.FileNotFoundException">
        /// Error Locating Base Directory
        /// or
        /// Error Locating Template Type Directory
        /// </exception>
        public static void InitTemplateManager(string BaseDirectory, string TemplateType, bool ForceReload = false)
        {
            _BaseDirectory = BaseDirectory;

            if (AllTemplateData.Where(x => x.BaseDirectory == BaseDirectory && x.TypeName == TemplateType).Count() > 0)
            {
                if (ForceReload == false)
                {
                    return;
                }
            }

            if (System.IO.Directory.Exists(BaseDirectory) == false)
            {
                throw new System.IO.FileNotFoundException("Error Locating Base Directory");
            }

            if (System.IO.Directory.Exists(BaseDirectory.EnsureDirectoryFormat() + TemplateType) == false)
            {
                throw new System.IO.FileNotFoundException("Error Locating Template Type Directory");
            }

            if (AllTemplateData.Where(x => x.BaseDirectory == BaseDirectory && x.TypeName == TemplateType).Count() > 0)
            {
                foreach (var template in AllTemplateData.Where(x => x.BaseDirectory == BaseDirectory && x.TypeName == TemplateType))
                {
                    //foreach (var tmplate in template.Templates)
                    //{
                    //    var _ReadAllText = System.IO.File.ReadAllText(BaseDirectory.EnsureDirectoryFormat() + TemplateType + "\\" + tmplate.FileName);
                    //    tmplate.TemplateData = _ReadAllText;
                    //}
                }
            }
            else
            {
                AddTemplateData(BaseDirectory, TemplateType);
            }

        }

        /// <summary>
        /// Init The Template Manager For Database Usage
        /// </summary>
        /// <param name="DBConnectionName">Name of the database connection.</param>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <param name="ForceReload">Reload the Template Regardless of CACHE</param>
        /// <exception cref="Exception">Base Directory Not Found: " + BaseDirectory</exception>
        /// <exception cref="DatabaseConnectionError">Database Access Error</exception>
        public static void InitTemplateManagerDB(string DBConnectionName, string BaseDirectory = null, bool ForceReload = false)
        {
            if (_Connected == true) { return; }

            _ConnectionName = DBConnectionName;
            _BaseDirectory = BaseDirectory;

            if (AllTemplateData.Count() == 0)
            {
                ForceReload = true;
            }

            if (ForceReload)
            {
                if (System.IO.Directory.Exists(BaseDirectory) == false) { throw new Exception("Base Directory Not Found: " + BaseDirectory); }

                // reads Files From The Base Directory and the Children
                UpdateDatabase(DBConnectionName, BaseDirectory);

                // Clear All Template data
                AllTemplateData.Clear();

                var _DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
                string _ConnString = ACT.Core.SystemSettings.GetSettingByName(DBConnectionName).Value;

                _DataAccess.Open(_ConnString);

                if (_DataAccess.Connected == false) { throw new DatabaseConnectionError("Database Access Error"); }

                var _Results = _DataAccess.RunCommand("TEMPLATES_WEB_GETALL", new List<System.Data.IDataParameter>(), true, System.Data.CommandType.StoredProcedure);

                if (_Results.FirstQueryHasExceptions == true)
                {
                    ACT.Core.Helper.ErrorLogger.LogError(null, "Error Getting Web Templates From Database", _Results.Exceptions[0], Enums.ErrorLevel.Warning);
                }

                AllTemplateData = new List<Template>();

                foreach (System.Data.DataRow DR in _Results.FirstDataTable_WithRows().Rows)
                {
                    var template = new Template();

                    if (AllTemplateData.Where(x => x.FileName == DR["Name"].ToString()).Count() == 0)
                    {
                        var type = (DR["TypeName"].ToString() == "Standard") ? ACT.Core.Enums.TemplateEngine.TemplateType.SIMPLE : ACT.Core.Enums.TemplateEngine.TemplateType.NGT;

                        template.BaseDirectory = DR["BaseDirectory"].ToString();
                        template.FileName = DR["Name"].ToString();
                        template.Data = DR["Data"].ToString();
                        template.Template_Type = type;
                        AllTemplateData.Add(template);
                    }
                    //AllTemplateData.Where( x => x.Template_Type == DR["TypeName"].ToString() ).First().Templates.Add( _NewT );
                }

                _DataAccess.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// LOCAL CACHE ONLY
        /// </summary>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <param name="TemplateType">Type of the template.</param>
        /// <returns>Template.</returns>
        internal static Template AddTemplateData(string BaseDirectory, string TemplateType)
        {
            Template _New = new Template();
            _New.TypeName = TemplateType;
            _New.BaseDirectory = BaseDirectory;
            //_New.Templates = new List<Template>();

            AllTemplateData.Add(_New);
            return _New;
        }

        //public static Template GetTemplateData(string BaseDirectory, string TemplateType, string TemplateName, bool ForceReload = false)
        //{
        //    if (AllTemplateData.Where(x => x.BaseDirectory == BaseDirectory && x.TemplateType == TemplateType).Count() > 0)
        //    {
        //        var _TemplateData = AllTemplateData.Where(x => x.BaseDirectory == BaseDirectory && x.TemplateType == TemplateType).First();

        //        if (_TemplateData.Templates.Where(x => x.FileName == TemplateName).Count() > 0)
        //        {
        //            if (ForceReload == false)
        //            {
        //                return _TemplateData.Templates.Where(x => x.FileName == TemplateName).First();
        //            }
        //            else
        //            {
        //                string _RawTemplateData = System.IO.File.ReadAllText(BaseDirectory.EnsureDirectoryFormat() + TemplateType + "\\" + TemplateName.EnsureEndsWith(".html"));
        //                _TemplateData.Templates.Where(x => x.FileName == TemplateName).First().TemplateData = _RawTemplateData;
        //                return _TemplateData.Templates.Where(x => x.FileName == TemplateName).First();
        //            }
        //        }
        //        else
        //        {
        //            var _NewTemplate = new Template();
        //            _NewTemplate.FileName = TemplateName;
        //            _NewTemplate.BaseDirectory = BaseDirectory;
        //            _NewTemplate.TemplateType = TemplateType;
        //            string _RawTemplateData = System.IO.File.ReadAllText(BaseDirectory.EnsureDirectoryFormat() + TemplateType + "\\" + TemplateName.EnsureEndsWith(".html"));
        //            _NewTemplate.TemplateData = _RawTemplateData;
        //            _TemplateData.Templates.Add(_NewTemplate);
        //            return _NewTemplate;
        //        }
        //    }
        //    else
        //    {
        //        var _TemplateData = AddTemplateData(BaseDirectory, TemplateType);
        //        var _NewTemplate = new Template();
        //        _NewTemplate.FileName = TemplateName;
        //        _NewTemplate.BaseDirectory = BaseDirectory;
        //        _NewTemplate.TemplateType = TemplateType;
        //        string _RawTemplateData = System.IO.File.ReadAllText(BaseDirectory.EnsureDirectoryFormat() + TemplateType + "\\" + TemplateName.EnsureEndsWith(".html"));
        //        _NewTemplate.TemplateData = _RawTemplateData;
        //        _TemplateData.Templates.Add(_NewTemplate);
        //        return _NewTemplate;
        //    }
        //}

        ///// <summary>
        ///// Gets the template data from the CACHE
        ///// </summary>
        ///// <param name="SubType"></param>
        ///// <param name="Name"></param>
        ///// <returns></returns>
        //public static Template GetTemplateData(string SubType, string Name)
        //{
        //    try
        //    {
        //        return AllTemplateData.Where(x => x.TemplateType == SubType).First().Templates.Where(x => x.FileName == Name).First();
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// This processes a template executing replacement data
        /// </summary>
        /// <param name="TemplateClass">Template To Process</param>
        /// <param name="DR">Current DataRow To Process</param>
        /// <param name="QueryStringData">QueryString Data</param>
        /// <param name="SessionData">Session Data</param>
        /// <returns>Parsed Template</returns>
        public static string ParseTemplate(Template TemplateClass, Dictionary<string, string> DR, NameValueCollection QueryStringData, Dictionary<string, string> SessionData)
        {
            List<Dictionary<string, string>> _TmpList = new List<Dictionary<string, string>>();
            _TmpList.Add(DR);
            var _DT = _TmpList.ToDataTable();
            return ParseTemplate(TemplateClass, _DT.Rows[0], _DT.Columns, QueryStringData, SessionData);
        }

        /// <summary>
        /// A TEMPLATE REPLACEMENT CAN BE ONE OF THE FOLLOWING
        /// #COLUMNNAME# - Direct Replacement For a Column Name in the DR Passed To IT
        /// #PARSEFILE_FILENAME_(STOREDPROC|DATAROW)_PARAMA,PARAMB,PARAMC
        /// </summary>
        /// <param name="TemplateClass">The template class.</param>
        /// <param name="DR">The dr.</param>
        /// <param name="DC">The dc.</param>
        /// <param name="QueryStringData">The query string data.</param>
        /// <param name="SessionData">The session data.</param>
        /// <returns>System.String.</returns>
        public static string ParseTemplate(Template TemplateClass, System.Data.DataRow DR, System.Data.DataColumnCollection DC, NameValueCollection QueryStringData, Dictionary<string, string> SessionData)
        {
            string _TempReturn = "";
            _TempReturn = TemplateClass.Data;
            _TempReturn = ProcessReplacementLogicMany(_TempReturn, TemplateClass, DR, DC, QueryStringData, SessionData);
            return _TempReturn;
        }

        /// <summary>
        /// Processes the replacement logic many.
        /// </summary>
        /// <param name="TemplateData">The template data.</param>
        /// <param name="TemplateClass">The template class.</param>
        /// <param name="DR">The dr.</param>
        /// <param name="DC">The dc.</param>
        /// <param name="QueryStringData">The query string data.</param>
        /// <param name="SessionData">The session data.</param>
        /// <returns>System.String.</returns>
        internal static string ProcessReplacementLogicMany(string TemplateData, Template TemplateClass, System.Data.DataRow DR, System.Data.DataColumnCollection DC, NameValueCollection QueryStringData, Dictionary<string, string> SessionData)
        {
            string _TempReturn = TemplateData;
            /// REPLACE ALL THE DIRECT COLUMN NAMES
            foreach (System.Data.DataColumn DCN in DC)
            {
                _TempReturn = _TempReturn.Replace("###" + DCN.ColumnName.ToUpper() + "###", DR[DCN.ColumnName].ToString());
            }

            // FIND ANY OTHER TEMPLATE PARTS
            int IndexPos = _TempReturn.IndexOf("###");

            while (IndexPos > 0)
            {
                // FIND OTHER REPLACEMENT CODES LOOK FOR END POUNDS
                int EndPos = _TempReturn.IndexOf("###", IndexPos + 3);
                // EXIT IF NO END POUNDS FOUND
                if (EndPos == -1) { break; }
                string _ReplacementName = _TempReturn.Substring(IndexPos, EndPos - IndexPos);

                // CALCULATE REPLACEMENT NAME WITHOUT ### STATEMENTS
                _ReplacementName = _ReplacementName.Replace("###", "");
                // PROCESS REPLACEMENTS USING REPLACEMENT LOGIC METHOD
                IndexPos = ProcessReplacementLogic(TemplateClass, _ReplacementName, ref _TempReturn, DR, DC, QueryStringData, SessionData);
            }

            return _TempReturn;
        }

        /// <summary>
        /// Parses The Stored Proc
        /// </summary>
        /// <param name="TemplateClass">The template class.</param>
        /// <param name="_ReplacementName">Name of the replacement.</param>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <param name="TemplateType">Type of the template.</param>
        /// <param name="DR">The dr.</param>
        /// <param name="DC">The dc.</param>
        /// <param name="QueryStringData">The query string data.</param>
        /// <param name="SessionData">The session data.</param>
        /// <returns>System.String.</returns>
        internal static string ParseStoredProc(Template TemplateClass, string _ReplacementName, string BaseDirectory, ACT.Core.Enums.TemplateEngine.TemplateType TemplateType, System.Data.DataRow DR, System.Data.DataColumnCollection DC, NameValueCollection QueryStringData, Dictionary<string, string> SessionData)
        {
            //             REPLACEMENT NAME ! TEMPLATE NAME ! DB PROCESS ! (STORED PROC NAME|SQL STATEMENT) ! SQL PARAMETERS (Comma Seperated)    
            // EXAMPLE: ###PARSEFILE!TestSurveyCategories!STOREDPROC!sp_EDIT_SURVEY_GET_CATEGORIES!@ClientID=CLIENT_ID,IsSTAMP=1,@Industry_ID=Industry_ID###

            string _TmpReturn = "";

            // EXECUTE STORED PROC AS DEFINED
            // GET STORED PROC REPLACEMENT DATA ! is THE SEPERATOR
            var _Replacements = _ReplacementName.SplitString("!", StringSplitOptions.RemoveEmptyEntries);

            // TEMPLATE NAME FROM DEFINED STORED PROCEDURE LINE
            string _TemplateName = _Replacements[1];
            string _DBProcess = _Replacements[2];
            string _DBCommand = _Replacements[3];
            string _DBParameters = _Replacements[4];

            // GET THE SUB TEMPLATE FROM THE TEMPLATENAME AND BASE DIRECTORY
            string _SubTemplate = GetTemplateDataDB(_ConnectionName, TemplateType, _TemplateName, 0).Data;

            // IF THE REPLACEMENT IS A STORED PROCEDURE RUN CODE
            if (_Replacements[2] == "STOREDPROC")
            {
                // GET STORED PROCEDURE PARAMETERS
                string[] _Params = _DBParameters.SplitString(",", StringSplitOptions.RemoveEmptyEntries);
                List<System.Data.IDataParameter> _ProcParams = new List<System.Data.IDataParameter>();

                //REPLACE STORED PROCEDURE PARAMETERS WITH VALUES
                #region Param Values
                foreach (string param in _Params)
                {
                    string[] paramvalues = param.SplitString("=", StringSplitOptions.RemoveEmptyEntries);
                    if (paramvalues[1].StartsWith("QUERYSTRINGENC"))
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], QueryStringData[paramvalues[1].Replace("QUERYSTRINGENC", "")].DecryptString()));
                    }
                    else if (paramvalues[1].StartsWith("QUERYSTRING"))
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], QueryStringData[paramvalues[1].Replace("QUERYSTRING", "")]));
                    }
                    else if (paramvalues[1].StartsWith("SESSION"))
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], SessionData[paramvalues[1].Replace("SESSION", "")]));
                    }
                    else if (paramvalues[1].StartsWith("INT"))
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], paramvalues[1].Replace("INT", "").ToInt()));
                    }
                    else if (paramvalues[1].StartsWith("BOOL"))
                    {
                        if (paramvalues[1].Replace("BOOL", "").ToUpper() == "TRUE")
                        {
                            _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], true));
                        }
                        else
                        {
                            _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], false));
                        }
                    }
                    else if (paramvalues[1].StartsWith("STR"))
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], paramvalues[1].Replace("STR", "")));
                    }
                    else if (DR.Table.Columns.Contains(paramvalues[1]))
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], DR[paramvalues[1]]));
                    }
                    else
                    {
                        _ProcParams.Add(new System.Data.SqlClient.SqlParameter(paramvalues[0], paramvalues[1]));
                    }
                }
                #endregion

                ACT.Core.Interfaces.DataAccess.I_DataAccess _CurrentDataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent();
                string _ConnString = ACT.Core.SystemSettings.GetSettingByName(_ConnectionName).Value;
                _CurrentDataAccess.Open(_ConnString);

                /// EXECUTE THE STORED PROCEDURE CALLER
                
                var _ProcResults = _CurrentDataAccess.RunCommand(_DBCommand, _ProcParams, true, System.Data.CommandType.StoredProcedure);
                _CurrentDataAccess.Dispose();
                _CurrentDataAccess = null;

                /// IF THE PROC RESULTS ARE NOT NULL
                if (_ProcResults.FirstDataTable_WithRows() != null)
                {
                    string _SubText = "";

                    foreach (System.Data.DataRow _DR in _ProcResults.FirstDataTable_WithRows().Rows)
                    {
                        string _TmpAddition = _SubTemplate;
                        foreach (System.Data.DataColumn CName in _ProcResults.FirstDataTable_WithRows().Columns)
                        {
                            _TmpAddition = _TmpAddition.Replace("###" + CName.ColumnName.ToUpper() + "###", _DR[CName].ToString());
                        }

                        int IndexPos = _TmpAddition.IndexOf("###");
                        while (IndexPos > 0)
                        {
                            // FIND OTHER REPLACEMENT CODES
                            int EndPos = _TmpAddition.IndexOf("###", IndexPos + 3);
                            if (EndPos == -1) { break; }
                            string _ReplacementName2 = _TmpAddition.Substring(IndexPos, EndPos - IndexPos);

                            _ReplacementName2 = _ReplacementName2.Replace("###", "");
                            IndexPos = ProcessReplacementLogic(TemplateClass, _ReplacementName2, ref _TmpAddition, _DR, _ProcResults.FirstDataTable_WithRows().Columns, QueryStringData, SessionData);
                        }

                        _SubText += _TmpAddition;
                    }
                    _TmpReturn = _SubText;
                }
                else
                {
                    _TmpReturn = "";
                    // TODO LOG ERROR
                }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Process Replacement LOGIC
        /// </summary>
        /// <param name="TemplateClass">Template Class Being Replaced</param>
        /// <param name="ReplacementName">Replacment Name WITHOUT POOUNDS ###SOMEDATA### = SOMEDATA</param>
        /// <param name="TemplateStringData">REF TO CURRENT OUTPUT DATA</param>
        /// <param name="DR">DATA ROW</param>
        /// <param name="DC">The dc.</param>
        /// <param name="QueryStringData">QUERYSTRING DATA</param>
        /// <param name="SessionData">SESSION DATA</param>
        /// <returns>System.Int32.</returns>
        internal static int ProcessReplacementLogic(Template TemplateClass, string ReplacementName, ref string TemplateStringData, System.Data.DataRow DR, System.Data.DataColumnCollection DC, NameValueCollection QueryStringData, Dictionary<string, string> SessionData)
        {
            //TODO MAKE THIS SHIT WORK  IMPORTANT
            string _ReplacementName = ReplacementName;
            int IndexPos = 0;

            if (_ReplacementName.StartsWith("PARSEFILE"))
            {
                var _ParseFileDetails = _ReplacementName.SplitString("!", StringSplitOptions.RemoveEmptyEntries);
                if (_ParseFileDetails[2] == "STOREDPROC")
                {
                    string _ProcParsed = ParseStoredProc(TemplateClass, _ReplacementName, TemplateClass.BaseDirectory, TemplateClass.Template_Type, DR, DC, QueryStringData, SessionData);
                    TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", _ProcParsed);
                    IndexPos = TemplateStringData.IndexOf("###");
                }
                else if (_ParseFileDetails[2] == "STANDARD")
                {
                    Template _TemplateFile = GetTemplateDataDB(_ConnectionName, TemplateClass.Template_Type, _ParseFileDetails[1]);
                    string _TemplateValue = ParseTemplate(_TemplateFile, DR, DC, QueryStringData, SessionData);
                    TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", _TemplateValue);
                    IndexPos = TemplateStringData.IndexOf("###");
                }
            }
            else if (_ReplacementName.StartsWith("QUERYSTRINGENC"))
            {
                string _Var = _ReplacementName.Replace("QUERYSTRINGENC", "");
                TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", QueryStringData[_Var].DecryptString());
                IndexPos = TemplateStringData.IndexOf("###");
            }
            else if (_ReplacementName.StartsWith("QUERYSTRING"))
            {
                string _Var = _ReplacementName.Replace("QUERYSTRING", "");
                TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", QueryStringData[_Var]);
                IndexPos = TemplateStringData.IndexOf("###");
            }
            else if (_ReplacementName.StartsWith("SESSION"))
            {
                string _Var = _ReplacementName.Replace("SESSION", "");
                TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", SessionData[_Var]);
                IndexPos = TemplateStringData.IndexOf("###");
            }
            else if (_ReplacementName.StartsWith("CHECKBOXCHECKED"))
            {
                string _Var = _ReplacementName.Replace("CHECKBOXCHECKED", "");
                if (DR[_Var].ToBool() != null)
                {
                    if (DR[_Var].ToBool().Value == true)
                    {
                        TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", "checked");
                    }
                }
                TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", "");
                IndexPos = TemplateStringData.IndexOf("###");
            }
            else if (_ReplacementName.StartsWith("IF"))
            {
                IndexPos = ProcessIFStatement(IndexPos, _ReplacementName, ref TemplateStringData, DR, DC, TemplateClass, QueryStringData, SessionData);
            }
            else
            {
                TemplateStringData = TemplateStringData.Replace("###" + _ReplacementName + "###", "");
                IndexPos = TemplateStringData.IndexOf("###");
            }

            return IndexPos;
        }

        /// <summary>
        /// PROCESS A DETECTED IF STATEMENT
        /// </summary>
        /// <param name="StartIndex">Starting Index Relative to TemplateStringData and Current IF Position</param>
        /// <param name="ReplacementName">REPLACEMENT NAME (ONLY THINGS INBETWEEN ###IF xxxxx ### = xxxxx</param>
        /// <param name="TemplateStringData">Current Template Data (Pass as Ref)</param>
        /// <param name="DR">Current Data Row</param>
        /// <param name="DC">The dc.</param>
        /// <param name="TemplateClass">The template class.</param>
        /// <param name="QueryStringData">The query string data.</param>
        /// <param name="SessionData">The session data.</param>
        /// <returns>New Index Position</returns>
        internal static int ProcessIFStatement(int StartIndex, string ReplacementName, ref string TemplateStringData, System.Data.DataRow DR, System.Data.DataColumnCollection DC, Template TemplateClass, NameValueCollection QueryStringData, Dictionary<string, string> SessionData)
        {
            //    ###IF CUSTOM=TRUE&&APPROVED=TRUE###
            // GET Original Replacement For Length Reasons
            string _OriginalReplacementName = "###" + ReplacementName + "###";
            int _OriginalReplacementLineLength = _OriginalReplacementName.Length;
            // Adjust the Start Index
            StartIndex = TemplateStringData.IndexOf(_OriginalReplacementName);

            // START OF END IF STATEMENT
            int ENDIFData = TemplateStringData.IndexOf("###END IF###", StartIndex);

            // GET REPLACEMENT NAME MINUS THE IF AND ## PURE EVAL DATA
            string _NewReplacementName = ReplacementName.Replace("IF ", "").Trim();

            /// GET The IF Condition Data
            string _IfData = TemplateStringData.Substring(StartIndex + _OriginalReplacementLineLength, ENDIFData - (StartIndex + _OriginalReplacementLineLength));

            /// SEPERATE THE CONDITIONS (CURRENTLY ONLY SUPPORTS AND (&&)
            string[] _IfConditions = _NewReplacementName.SplitString("&&", StringSplitOptions.RemoveEmptyEntries);

            // Meets Condition Flag
            bool _MeetsCondition = true;

            //Loop Through All The If Statement Conditions
            foreach (string _IfStatment in _IfConditions)
            {
                // GET Condition Test Data
                string[] _ValueData = _IfStatment.SplitString("=", StringSplitOptions.RemoveEmptyEntries);

                if (_ValueData.Length == 2)
                {
                    // PERFORM TEST
                    // IF TRUE IS VALUE DO BOOL TEST WHERE !VAL = TRUE
                    if (_ValueData[1] == "TRUE")
                    {
                        if (DR[_ValueData[0]].ToBool().Value == false) { _MeetsCondition = false; break; }
                    }
                    // IF FALSE IS VALUE DO BOOL TEST WHERE !VAL = FALSE
                    else if (_ValueData[1] == "FALSE")
                    {
                        if (DR[_ValueData[0]].ToBool().Value == true) { _MeetsCondition = false; break; }
                    }
                    // IF Value is an Integer Perform and Integer Test
                    else if (_ValueData[1].ToInt(-10101010) != -10101010)
                    {
                        if (DR[_ValueData[0]].ToInt(-10101010) != _ValueData[1].ToInt()) { _MeetsCondition = false; break; }
                    }
                    // ELSE DO A String Comparison (NON CASE SENSITIVE)
                    else
                    {
                        if (DR[_ValueData[0]].ToString().ToLower() != _ValueData[1].ToString().ToLower()) { _MeetsCondition = false; break; }
                    }
                }
                else if (_ValueData.Length > 2)
                {
                    _ValueData = _IfStatment.SplitString("!", StringSplitOptions.RemoveEmptyEntries);
                    // PERFORM TEST
                    // IF TRUE IS VALUE DO BOOL TEST WHERE !VAL = TRUE
                    if (_ValueData[1] == "TRUE")
                    {
                        if (DR[_ValueData[0]].ToBool().Value != false) { _MeetsCondition = false; break; }
                    }
                    // IF FALSE IS VALUE DO BOOL TEST WHERE !VAL = FALSE
                    else if (_ValueData[1] == "FALSE")
                    {
                        if (DR[_ValueData[0]].ToBool().Value != true) { _MeetsCondition = false; break; }
                    }
                    // IF Value is an Integer Perform and Integer Test
                    else if (_ValueData[1].ToInt(-10101010) != -10101010)
                    {
                        if (DR[_ValueData[0]].ToInt(-10101010) == _ValueData[1].ToInt()) { _MeetsCondition = false; break; }
                    }
                    // ELSE DO A String Comparison (NON CASE SENSITIVE)
                    else
                    {
                        if (DR[_ValueData[0]].ToString().ToLower() == _ValueData[1].ToString().ToLower()) { _MeetsCondition = false; break; }
                    }
                }
            }

            // Calculate End IF Statement Location
            int _ENDIFPlacement = TemplateStringData.IndexOf("###END IF###", StartIndex) + 12;

            // IF THE CONDITIONS WERE MET
            if (_MeetsCondition)
            {
                // GET FULL IF STATEMENT TEXT INCLUDING END IF AND VALUE IN MIDDLE
                string _BASEReplacement = TemplateStringData.Substring(StartIndex, _ENDIFPlacement - StartIndex);
                // REPLACE THE IF STATEMENT GETTING RID OF IT
                _BASEReplacement = _BASEReplacement.Replace(_OriginalReplacementName, "");
                // REPLACE THE END IF STATEMENT GETTING RID OF IT
                _BASEReplacement = _BASEReplacement.Replace("###END IF###", "");

                _BASEReplacement = ProcessReplacementLogicMany(_BASEReplacement, TemplateClass, DR, DC, QueryStringData, SessionData);


                // REMOVE THE TEMPLATE REPLACEMENT WITH NOTHING
                TemplateStringData = TemplateStringData.Remove(StartIndex, _ENDIFPlacement - StartIndex);
                // INSERT THE NEW IF DATA DEFINED
                TemplateStringData = TemplateStringData.Insert(StartIndex, _BASEReplacement);
                return TemplateStringData.IndexOf("###", StartIndex);
            }
            // IF The Conditions WERE Not MET
            else
            {
                // REMOVE THE TEMPLATE REPLACEMENT WITH NOTHING
                TemplateStringData = TemplateStringData.Remove(StartIndex, _ENDIFPlacement - StartIndex);
                return TemplateStringData.IndexOf("###", StartIndex);
            }
        }

    }
}
