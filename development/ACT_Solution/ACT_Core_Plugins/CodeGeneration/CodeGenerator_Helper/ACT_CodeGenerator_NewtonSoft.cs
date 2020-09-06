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
        /// Generate The Export Method Currently Supports XML and JSON
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <param name="CodeSettings">The code settings.</param>
        /// <returns>System.String.</returns>
        private string GenerateExportMethod(I_DbTable Table, I_CodeGenerationSettings CodeSettings)
        {
            string _TmpReturn = "";
            _TmpReturn = "\tpublic string Export(string Format)" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t{" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    if (Format.ToLower() == \"xml\")" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    {" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType() );" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t        System.IO.StringWriter _Output = new System.IO.StringWriter();" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t        x.Serialize(_Output, this);" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t        return _Output.ToString();" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    }" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    else if (Format.ToLower() == \"json\")" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    {" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t        return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    }" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t    return \"\";" + Environment.NewLine;
            _TmpReturn = _TmpReturn += "\t}" + Environment.NewLine;
            return _TmpReturn;
        }
    }
}
