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
using ACT.Core.Extensions.CodeGenerator;
using ACT.Core.Interfaces.CodeGeneration;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.DataAccess;
using ACT.Core.TemplateEngine;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

        #region ENUM CODE

        /// <summary>
        /// Generates The Enums
        /// </summary>
        /// <param name="NameValues">Name And Values To Generate</param>
        /// <param name="TableName">Table Name of EnumTable</param>
        /// <returns>System.String.</returns>
        public string GenerateEnum(Dictionary<string, int> NameValues, string TableName)
        {
            string _TmpReturn = "";
            _TmpReturn = "public enum " + TableName.ToCSharpFriendlyName() + " " + Environment.NewLine + "\t{ " + Environment.NewLine;

            foreach (string k in NameValues.Keys)
            {
                _TmpReturn += Environment.NewLine + "\t\t" + k.Replace(" ", "_").ToCSharpFriendlyName() + " = " + NameValues[k] + ",";
            }
            _TmpReturn = _TmpReturn.TrimEnd(",");
            _TmpReturn += "\t}" + Environment.NewLine;

            return _TmpReturn;
        }

        /// <summary>
        /// Generates The Enums..  This is a Specific Class That Utilizes a Table Named _GeneratorEnums
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>I_GeneratedCode.</returns>
        public I_GeneratedCode GenerateEnums(I_CodeGenerationSettings CodeSettings)
        {
            bool _UseDatabaseConnectionName = false;
            if (CodeSettings.DatabaseConnectionName != "")
            {
                _UseDatabaseConnectionName = true;
            }

            string _Temp = "";
            _Temp += ACT.Core.SystemSettings.GetSettingByName("ACTCodeGenerator_UsingStatements").Value;
            _Temp += Environment.NewLine + "" + Environment.NewLine;
            _Temp += "namespace " + CodeSettings.NameSpace + Environment.NewLine + "{" + Environment.NewLine;
            _Temp += "\tpublic class Enums : ACT.Plugins.ACT_Core " + Environment.NewLine + "\t{" + Environment.NewLine;

            using (var DataAccess = ACT.Core.CurrentCore<ACT.Core.Interfaces.DataAccess.I_DataAccess>.GetCurrent())
            {
                if (_UseDatabaseConnectionName)
                {
                    DataAccess.Open(ACT.Core.SystemSettings.GetSettingByName(CodeSettings.DatabaseConnectionName).Value);
                }
                else
                {

                    DataAccess.Open();

                }

                var QR = DataAccess.RunCommand("Select * From _GeneratorEnums", null, true, System.Data.CommandType.Text);

                if (QR.Errors[0] == true)
                {
                    LogError(this.GetType().FullName, "Error Generating Enums. Missing Meta Data Table: _GeneratorEnums", QR.Exceptions[0], "", ErrorLevel.Warning);
                }
                else
                {
                    foreach (System.Data.DataRow DRow in QR.Tables[0].Rows)
                    {
                        string _tmpDataSQL = "select " + DRow["FieldName"].ToString() + ", " + DRow["ValueFieldName"].ToString() + " from " + DRow["TableName"].ToString();

                        var DQR = DataAccess.RunCommand(_tmpDataSQL, null, true, System.Data.CommandType.Text);

                        Dictionary<string, int> _TmpEnumData = new Dictionary<string, int>();
                        foreach (System.Data.DataRow DDRow in DQR.Tables[0].Rows)
                        {
                            _TmpEnumData.Add(DDRow[DRow["FieldName"].ToString()].ToString(), Convert.ToInt32(DDRow[DRow["ValueFieldName"].ToString()]));
                        }

                        _Temp += GenerateEnum(_TmpEnumData, DRow["TableName"].ToString());
                    }
                }
            }

            _Temp += "\t}" + Environment.NewLine + "}" + Environment.NewLine;  //Close Namespace and Class

            ACT_GeneratedCode _NewREturn = new ACT_GeneratedCode();
            _NewREturn.FileName = "Enums.cs";
            _NewREturn.Code = _Temp;

            return _NewREturn;
        }

        #endregion
    }
}
