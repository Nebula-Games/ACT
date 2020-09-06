// ***********************************************************************
// Assembly         : ACTPlugins
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="ACT_CodeGenerator.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Enums;
using ACT.Core.Extensions;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ACT.Plugins.CodeGeneration
{
    /// <summary>
    /// Internal Code Generation Class Generates C# Code
    /// Implements the <see cref="ACT.Plugins.ACT_Core" />
    /// Implements the <see cref="ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator" />
    /// </summary>
    /// <seealso cref="ACT.Plugins.ACT_Core" />
    /// <seealso cref="ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator" />
    public partial class ACT_CodeGenerator : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.CodeGeneration.I_CodeGenerator
    {

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>List&lt;I_GeneratedCode&gt;.</returns>
        public List<I_GeneratedCode> GenerateWebFormCode(I_CodeGenerationSettings CodeSettings)
        {
            List<I_GeneratedCode> _TmpReturn = new List<I_GeneratedCode>();

            var DataAccess = ACT.Core.CurrentCore<I_DataAccess>.GetCurrent();

            string _ConnectionString = ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value;
            string _ASPTemplate = ACT.Core.SystemSettings.GetSettingByName("ASPNETTemplate").Value;
            string _ASPTemplateRow = ACT.Core.SystemSettings.GetSettingByName("ASPNETTemplateRow").Value;
            string _ASPTemplateCodeFile = ACT.Core.SystemSettings.GetSettingByName("ASPNETTemplateCodeFile").Value;

            if (_ConnectionString == "")
            {
                LogError("Error with the DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Locate Setting Name" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Critical);
            }

            if (DataAccess.Open(_ConnectionString))
            {
                var DB = DataAccess.ExportDatabase();

                foreach (var T in DB.Tables)
                {
                    string p1, p2, r;

                    p1 = _ASPTemplate;
                    p2 = _ASPTemplateCodeFile;


                    p1 = T.StandardReplaceMent(p1, RepacementStandard.UPPERCASE);
                    p2 = T.StandardReplaceMent(p2, RepacementStandard.UPPERCASE);

                    string _rowoutput = "";

                    foreach (var C in T.Columns)
                    {
                        r = _ASPTemplateRow;
                        r = C.StandardReplaceMent(r, RepacementStandard.UPPERCASE);

                        string _ControlTemplate = "";
                        if (C.DataType.IsStringType())
                        {
                            _ControlTemplate = "<asp:TextBox ID=\"#NAME#_TextBox\" runat=\"server\"></asp:TextBox>";
                        }
                        else if (C.DataType == System.Data.DbType.Guid)
                        {
                            var rel = (from x in T.AllRelationships where x.ColumnName == C.Name select x).First();

                            //rel.External_TableName



                        }

                        r = r.Replace("#INPUTCONTROL#", C.StandardReplaceMent(_ControlTemplate, RepacementStandard.UPPERCASE));
                    }

                    p1.Replace("#ROWDATA#", _rowoutput);
                }

            }
            else
            {
                LogError("Error Open DB With the ConnectionString Specified By DatabaseConnectionName: " + CodeSettings.DatabaseConnectionName + "" + Environment.NewLine, "Unable To Open DB" + Environment.NewLine, null, "" + Environment.NewLine, ErrorLevel.Critical);
                return null;
            }

            return _TmpReturn;
        }

    }
}
