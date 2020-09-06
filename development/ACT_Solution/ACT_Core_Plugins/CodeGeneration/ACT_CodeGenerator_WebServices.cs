using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.Interfaces.Security;
using ACT.Core.Interfaces.Common;
using ACT.Core.Enums;
using ACT.Core.Extensions.CodeGenerator;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CSharp;


namespace ACT.Plugins.CodeGeneration
{
    public partial class ACT_CodeGenerator : ACT.Plugins.ACT_Core, I_CodeGenerator
    {
     
        #region Web Services Code

        /// <summary>
        /// Generate All The Web Service Physical Files
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="CodeSettings"></param>
        private void GenerateWebServiceLayer(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            if (System.IO.Directory.Exists(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "WebServices\\"))
            {
                System.IO.Directory.Delete(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "WebServices\\", true);
            }

            if (System.IO.Directory.Exists(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "AppCode_CS\\"))
            {
                System.IO.Directory.Delete(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "AppCode_CS\\", true);
            }
            /// Wait For Windows to Clean Up

            System.Threading.Thread.Sleep(500);

            /*
             WebServices\TableName_Service.asmx
             WebServices\TableName_Rest.ashx
             AppCode_CS\TableName_Service.cs
             */

            /// Write The Rest Services
            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "WebServices\\");
            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "WebServices\\" + c.TableName.ToCSharpFriendlyName() + "_Rest" + ".ashx", c.WebServiceCode);
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "WebServices\\" + c.TableName.ToCSharpFriendlyName() + "_Service.asmx", c.WebServiceSoapASMX);                
            }

            /// Write the SOAP Services
            /// 
            System.IO.Directory.CreateDirectory(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "AppCode_CS\\");
            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "AppCode_CS\\" + c.TableName.ToCSharpFriendlyName() + "_Service.cs", c.WebServiceSoapCode);
            }

        }

        
        /// <summary>
        /// Generate REST Services
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        private string GenerateWebServices(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            string _Temp = "<%@ WebHandler Language=\"C#\" Class=\"" + Table.ShortName.ToCSharpFriendlyName() + "\" %>" + NL;

            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += "using System.Web.Services;" + NL;
            _Temp += "using System.Web;" + NL;

            _Temp += NL + NL;

            _Temp += "\tpublic class " + Table.ShortName.ToCSharpFriendlyName() + "_WebServiceClass : IHttpHandler " + NL + "\t";
            _Temp += "{" + NL;
            _Temp += "\tpublic bool IsReusable { get { return false; } }" + NL;

            _Temp += "\tpublic void ProcessRequest(HttpContext context)" + NL;
            _Temp += "\t{" + NL;

            _Temp += "\t}" + NL;
            _Temp += GenerateGetDataMethod(Table, CodeSettings);
            _Temp += "\t}" + NL + NL + NL;

            return _Temp;

        }
        /// <summary>
        /// Generate the WebServices Classes
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="CodeSettings"></param>
        /// <returns></returns>
        private string GenerateWebServicesSoap(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {

            string _Temp = "";  // "<%@ WebHandler Language=\"C#\" Class=\"" + Table.ShortName.ToCSharpFriendlyName() + "\" %>" + NL;

            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += "using System.Web.Services;" + NL;
            _Temp += "using System.Web;" + NL;

            _Temp += NL + NL;

            _Temp += "[WebService(Namespace = \"http://ACT.ALLMYCODE.INFO/\")]";
            _Temp += "[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]";

            _Temp += "\tpublic class " + Table.ShortName.ToCSharpFriendlyName() + "_WebService : WebService " + NL + "\t";
            _Temp += "{" + NL;


            _Temp += "\tpublic void ProcessRequest(HttpContext context)" + NL;
            _Temp += "\t{" + NL;

            _Temp += "\t}" + NL;
            _Temp += GenerateGetDataMethod(Table, CodeSettings);
            _Temp += "\t}" + NL + NL + NL;

            return _Temp;

        }
        private string GenerateGetDataMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            string _Temp = "";
            _Temp += "\t[WebMethod()]" + NL;
            _Temp += "public string GetData(";

            List<string> _PrimaryKeys = Table.GetPrimaryColumnNames;

            string _PrimaryKeyString = "";


            foreach (string _S in _PrimaryKeys)
            {
                _PrimaryKeyString = _PrimaryKeyString + "string P_" + Table.GetColumn(_S, true).ShortName.ToCSharpFriendlyName() + ", ";
            }
            _PrimaryKeyString = _PrimaryKeyString.TrimEnd(", ".ToCharArray());
            _Temp += _PrimaryKeyString;
            _Temp += ", string ExportFormat) " + NL + "\t";
            _Temp += "{" + NL;

            _Temp += "\t Dictionary<string, string> _SearchData = new Dictionary<string, string>();" + NL;

            foreach (string _S in _PrimaryKeys)
            {
                _Temp += "\t_SearchData.Add(\"[" + Table.GetColumn(_S, true).ShortName + "]\", P_" + Table.GetColumn(_S, true).ShortName.ToCSharpFriendlyName() + ");" + NL;
            }

            _Temp += "\tvar _SearchClassVar = " + CodeSettings.NameSpace + "." + Table.ShortName.ToCSharpFriendlyName() + ".Search(_SearchData);" + NL;
            _Temp += "\tstring _TmpReturn = \"\";" + NL;
            _Temp += "\tforeach (var classvar in _SearchClassVar)" + NL;
            _Temp += "\t{" + NL;
            _Temp += "\t_TmpReturn += classvar.Export(ExportFormat) + \"\\n\\rITEMBOUNDRY\\n\\r\";" + NL;
            _Temp += "\t}" + NL;
            /// Loop Through Return Data And Call The Export Method With The Appropriate Format
            _Temp += "\treturn _TmpReturn;" + NL;
            _Temp += "\t}" + NL;
            return _Temp;

        }

             #endregion
    }
}
