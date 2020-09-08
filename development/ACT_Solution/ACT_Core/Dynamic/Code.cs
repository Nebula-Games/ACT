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

namespace ACT.Core.Dynamic
{
    /// <summary>
    /// Dynamic Code
    /// </summary>
    public class DynamicCode
    {
        public DynamicCode(string FileLocation)
        {
            if (FileLocation.FileExists() == false) { throw new System.IO.FileNotFoundException(); }

            Code = FileLocation.ReadAllText();
            if (FileLocation.ToLower().EndsWith("cs")) { CodeLanguageVersion = LanguageVersion.Latest; }
            else { throw new Exception("Not Supported"); }
            Description = FileLocation;

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
                if (LangVer== LanguageVersion.Default)
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

        internal class Compiler
        {
            public byte[] Compile(string filepath)
            {
                DynamicCode _Code = new DynamicCode(filepath);

                Console.WriteLine($"Starting compilation of: '{filepath}'");

                var sourceCode = File.ReadAllText(filepath);

                using (var peStream = new MemoryStream())
                {
                    var result = GenerateCode(_Code).Emit(peStream);

                    if (!result.Success)
                    {
                        Console.WriteLine("Compilation done with error.");

                        var failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                        foreach (var diagnostic in failures)
                        {
                            Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                        }

                        return null;
                    }

                    Console.WriteLine("Compilation done without any error.");

                    peStream.Seek(0, SeekOrigin.Begin);

                    return peStream.ToArray();
                }
            }

            private static CSharpCompilation GenerateCode(DynamicCode code)
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

                return CSharpCompilation.Create("Hello.dll",
                    new[] { parsedSyntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.ConsoleApplication,
                        optimizationLevel: OptimizationLevel.Release,
                        assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
            }
        }
    }
}
