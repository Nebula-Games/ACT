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

        /// <summary>
        /// Generates the generic calls.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <returns>System.String.</returns>
        public string GenerateGenericCalls(I_DbTable Table, I_CodeGenerationSettings CodeSettings, string FileName = "")
        {
            string _TmpReturn = "";

            string _TmpPath = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Data\\";
            _TmpPath.CreateDirectoryStructure();

            string[] _AllFiles = System.IO.Directory.GetFiles(_TmpPath);

            var _codeGenFiles = _AllFiles.Where(x => x.GetFileNameFromFullPath().ToLower().StartsWith("codegen"));

            if (FileName.NullOrEmpty() == false)
            {
                var _foundGenFiles = _codeGenFiles.Where(x => x.GetFileNameFromFullPath().ToLower().StartsWith(FileName.ToLower()));

                var _MainGenFile = _codeGenFiles.Where(x => x.GetFileNameFromFullPath().ToLower() == FileName.ToLower() + "_1").First();

                string _FileText = System.IO.File.ReadAllText(_MainGenFile);

                _TmpReturn += Environment.NewLine + ParseFile(_FileText, _MainGenFile.GetFileNameFromFullPath(), Table, CodeSettings) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                foreach (string file in _codeGenFiles.Where(x => x.GetFileNameFromFullPath().ToLower().EndsWith("_1.txt")))
                {
                    _TmpReturn += Environment.NewLine + ParseFile(System.IO.File.ReadAllText(file), file, Table, CodeSettings) + Environment.NewLine + Environment.NewLine;
                }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// The template path
        /// </summary>
        private static string _TemplatePath = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Resources\\CodeGeneration\\Templates\\";

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <param name="TEXT">The text.</param>
        /// <param name="BaseFileName">Name of the base file.</param>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        public string ParseFile(string TEXT, string BaseFileName, I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            TEXT = TEXT.Replace("###TYPE_NAME###", Table.ShortName.ToCSharpFriendlyName());
            TEXT = TEXT.Replace("###SHORT_TABLE_NAME###", Table.ShortName);
            TEXT = TEXT.Replace("###FULL_TABLE_NAME###", Table.ShortName);
            bool _ContinueFound = true;
            while (_ContinueFound)
            {
                if (TEXT.Contains("###PARSEFILE-"))
                {
                    if (TEXT.Contains("###PARSEFILE-LOOPPROP"))
                    {
                        System.Text.RegularExpressions.Regex _LoopReg = new System.Text.RegularExpressions.Regex(@"###PARSEFILE-LOOPPROP-(\d)*###");
                        var _LoopMatches = _LoopReg.Match(TEXT);
                        int _FileNum = _LoopMatches.Groups[1].Value.ToInt(-1);
                        if (_FileNum == -1)
                        {
                            return "ERROR PROCESSING FILE";
                        }
                        else
                        {
                            string _FileName = BaseFileName.Substring(0, BaseFileName.LastIndexOf("_"));
                            _FileName = _FileName + "_" + _FileNum.ToString() + ".txt";
                            if (System.IO.File.Exists(_FileName) == false) { return "ERROR SUB FILE NOT FOUND: " + _FileName; }

                            TEXT = TEXT.Left(_LoopMatches.Index) + ParseColumnLoopAdditive(System.IO.File.ReadAllText(_FileName), Table, CodeSettings) + TEXT.Substring(_LoopMatches.Index + _LoopMatches.Length);
                        }
                    }
                    else
                    {
                        System.Text.RegularExpressions.Regex _LoopReg = new System.Text.RegularExpressions.Regex(@"###PARSEFILE-(\d)*###");

                        foreach (System.Text.RegularExpressions.Match match in _LoopReg.Matches(TEXT))
                        {
                            int _FileNum = match.Groups[1].Value.ToInt(-1);
                            if (_FileNum == -1)
                            {
                                return "ERROR PROCESSING FILE";
                            }
                            else
                            {
                                string _FileName = BaseFileName.Substring(0, BaseFileName.LastIndexOf("_"));
                                _FileName = _FileName + "_" + _FileNum.ToString() + ".txt";
                                if (System.IO.File.Exists(_FileName) == false) { return "ERROR SUB FILE NOT FOUND: " + _FileName; }
                                TEXT = TEXT.Left(match.Index) + ParseFile(System.IO.File.ReadAllText(_FileName), BaseFileName, Table, CodeSettings) + TEXT.Substring(match.Index + match.Length);
                            }
                        }
                    }
                }
                else
                {
                    _ContinueFound = false;
                }
            }

            return TEXT;
        }

        /// <summary>
        /// Parses the column loop additive.
        /// </summary>
        /// <param name="AdditiveText">The additive text.</param>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        public string ParseColumnLoopAdditive(string AdditiveText, I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            string _TmpReturn = "";

            foreach (var col in Table.Columns)
            {
                string property = col.ShortName.ToCSharpFriendlyName();
                string _TmpAdd = AdditiveText;

                _TmpAdd = _TmpAdd.Replace("###PROPERTYNAME###", property);
                _TmpAdd = _TmpAdd.Replace("###SQLDATATYPE###", col.DataType.ToSQLDataType().ToString());
                _TmpAdd = _TmpAdd.Replace("###CSHARPDATATYPE###", col.DataType.ToCSharpStringNullable());
                _TmpAdd = _TmpAdd.Replace("###CSHARPDATATYPENOTNULL###", col.DataType.ToCSharpString());

                _TmpReturn += _TmpAdd;
            }

            return _TmpReturn;
        }

    }
}
