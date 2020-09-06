// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="MSSQL_HelperMethods.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;

namespace ACT.Core.Helper.MSSQL
{
    /// <summary>
    /// Class MSSQL_HelperMethods.
    /// </summary>
    public static class MSSQL_HelperMethods
    {
        /// <summary>
        /// Validates the parameters for injection possibility.
        /// </summary>
        /// <param name="SQL">The SQL.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ValidateParametersForInjectionPossibility(string SQL)
        {
            string[] _Keywords = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Data\\MSSQLCommands.txt");

            if (_Keywords.Select(x => SQL.ToLower().Contains(x)).Count() > 1)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Execuite Large "Sql Script
        /// </summary>
        /// <param name="ConnectionInfo"></param>
        /// <param name="ScriptInfo"></param>
        /// <returns>"" On No File Output Specified, File Contents On Success, and NULL on Error</returns>
        public static string ExecuteLargeSQLScript(Types.Database.GenericConnectionInformation ConnectionInfo, Types.Database.MSSQL_LargeScript ScriptInfo)
        {
            string _CommandLineExecutionString = "sqlcmd ";
            //- j - S sg - app - 01 - d SGI_CORE - U sa - P MandS1122 - i "c:\temp\RandomData.sql"

            if (ScriptInfo.SQLFileLocation.FileExists() == false) { return null; }
            string _OutFile = ScriptInfo.OutputFileLocation.EnsureDirectoryFormat() + ScriptInfo.SQLFileLocation.GetFileNameWithoutExtension() + DateTime.Now.ToUnixTime().ToString() + ".txt";
            if (ScriptInfo.OutputFileLocation.NullOrEmpty() == false) { _CommandLineExecutionString += "-o \"" + _OutFile + "\" "; }

            _CommandLineExecutionString += "-S " + ConnectionInfo.Server + " -d " + ConnectionInfo.DatabaseName + " ";

            if (ScriptInfo.UseTrustedConnection == false) { _CommandLineExecutionString += "-U " + ConnectionInfo.UserName + "-P " + ConnectionInfo.EncryptedPassword.DecryptString() + " -i \"" + ScriptInfo.SQLFileLocation + "\""; }

            try { System.Diagnostics.Process.Start("sqlcmd ", _CommandLineExecutionString); }
            catch { }

            if (ScriptInfo.OutputFileLocation.NullOrEmpty()) { return ""; }
            else { try { return _OutFile.ReadAllText(); } catch { return null; } }

        }

        // Information Only
        public class CommandLineSwitches
        {
            public string a = "packet_size";
            public string A = "(dedicated administrator connection)";
            public string b = "(terminate batch job if there is an error)";
            public string c = "batch_terminator";
            public string C = "(trust the server certificate)";
            public string d = "db_name";
            public string e = "(echo input)";
            public string E = "(use trusted connection)";
            public string f = "codepage | i:codepage[o:codepage] | o:codepage[i:codepage]";
            public string g = "(enable column encryption)";
            public string G = "(use Azure Active Directory for authentication)";
            public string h = "rows_per_header";
            public string H = "workstation_name";
            public string i = "input_file";
            public string I = "(enable quoted identifiers)";
            public string j = "(Print raw error messages)";
            public string k = "[1 | 2] (remove or replace control characters)";
            public string K = "application_intent";
            public string l = "login_timeout";
            public string L = "[c] (list servers optional clean output)";
            public string m = "error_level";
            public string M = "multisubnet_failover";
            public string N = "(encrypt connection)";
            public string o = "output_file";
            public string p = "[1] (print statistics optional colon format)";
            public string P = "password";
            public string q = "cmdline query";
            public string Q = "cmdline query (and exit)";
            public string r = "[0 | 1] (msgs to stderr)";
            public string R = "(use client regional settings)";
            public string s = "col_separator";
            public string S = "[protocol:]server[instance_name][port]";
            public string t = "query_timeout";
            public string u = "(unicode output file)";
            public string U = "login_id";
            public string v = "var = value";
            public string V = "error_severity_level";
            public string w = "column_width";
            public string W = "(remove trailing spaces)";
            public string x = "(disable variable substitution)";
            public string X = "[1] (disable commands startup script environment variables optional exit)";
            public string y = "variable_length_type_display_width";
            public string Y = "fixed_length_type_display_width";
            public string z = "new_password ";
            public string Z = "new_password (and exit)";
        }
    }
}
