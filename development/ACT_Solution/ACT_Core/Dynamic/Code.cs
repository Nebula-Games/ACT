// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Code.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using ACT.Core.Extensions;
using System.Threading;
using System.Reflection;

namespace ACT.Core.Dynamic
{
    /// <summary>
    /// Dynamic Code
    /// </summary>
    public class DynamicCode
    {
        /// <summary>
        /// Dynamic Code
        /// </summary>
        /// <param name="FileLocation"></param>
        public DynamicCode(string FileLocation)
        {
            if (FileLocation.FileExists() == false) { throw new System.IO.FileNotFoundException(); }
            Code = FileLocation.ReadAllText();
            if (FileLocation.ToLower().EndsWith("cs")) { CodeLanguageVersion = LanguageVersion.Latest; }
            else { throw new Exception("Not Supported"); }
            Name = FileLocation.GetFileNameWithoutExtension(); Description = "From Library Compile Code";
            CodeLanguageVersion = LanguageVersion.Latest;
            Language = CodeLanguageVersion.GetEnumName_FromValue<LanguageVersion>();
            Dependancies = new List<string>();
        }

        /// <summary>
        /// Dynamic Code From String
        /// </summary>
        /// <param name="CodeToCompile"></param>
        /// <param name="CodeLanguage"></param>
        /// <param name="AdditionalReferences"></param>
        public DynamicCode(string CodeToCompile, Microsoft.CodeAnalysis.CSharp.LanguageVersion CodeLanguage = LanguageVersion.Default, List<string>? AdditionalReferences = null)
        {
            Name = ""; Description = "From Library Compile Code"; Language = CodeLanguage.GetEnumName_FromValue<LanguageVersion>();
            CodeLanguageVersion = CodeLanguage;
            Code = CodeToCompile;
            if (AdditionalReferences != null) { Dependancies = AdditionalReferences; }
            else { Dependancies = new List<string>(); }

        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        private string _LanguageVersion = "";

        /// <summary>
        /// Gets or sets the language version.
        /// </summary>
        /// <value>The language version.</value>
        public void SetLanguageVersion(string Ver, Microsoft.CodeAnalysis.CSharp.LanguageVersion LangVer = LanguageVersion.Default)
        {
            if (Ver.NullOrEmpty())
            {
                _LanguageVersion = LangVer.GetEnumName_FromValue<LanguageVersion>();
                CodeLanguageVersion = LangVer;
            }
            else
            {
                if (LangVer == LanguageVersion.Default)
                {
                    try { CodeLanguageVersion = (LanguageVersion)LangVer.FromString(Ver); return; } catch { }
                }
            }

            CodeLanguageVersion = LangVer;
        }

        private Microsoft.CodeAnalysis.CSharp.LanguageVersion CodeLanguageVersion;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code { get; set; }
        /// <summary>
        /// Gets or sets the dependancies.
        /// </summary>
        /// <value>The dependancies.</value>
        public List<string> Dependancies { get; set; }


        public static Assembly? GenerateCode(DynamicCode code)
        {
            var codeString = SourceText.From(code.Code);
            var options = CSharpParseOptions.Default.WithLanguageVersion(code.CodeLanguageVersion);

            var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

            var references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).Assembly.Location),

            };

            var _cSComp = CSharpCompilation.Create(code.Name.EnsureEndsWith(".dll"),
                new[] { parsedSyntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.ConsoleApplication,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));

            string P = "";
            var _P = "".GetExecutingAssemblyPathDirectory();
            if (_P != null) { P = _P.EnsureDirectoryFormat(); }

            var _BaseResult = _cSComp.Emit(P, null, null, P, null);
            if (_BaseResult.Success) { return Assembly.LoadFile(P); }
            else { return null; }

        }

    }
}
