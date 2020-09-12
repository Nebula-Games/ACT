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
using System;
using System.Collections.Generic;

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
        #region WRITE CODE TO FILES

        /// <summary>
        /// Supports Multiple Files
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeSettings">The code settings.</param>
        private void GenerateStoredProceduresFile(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            string _Folder = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "\\DBSP\\";

            if (!System.IO.Directory.Exists(_Folder)) { System.IO.Directory.CreateDirectory(_Folder); }
            else { if (CodeSettings.GenerateBaseLayer == false) { _Folder.DeleteAllFilesFromDirectory(false); } }

            if (CodeSettings.NamespaceDrivenProcedures == true)
            {
                if (!System.IO.Directory.Exists(_Folder)) { System.IO.Directory.CreateDirectory(_Folder); }
                else { (_Folder).DeleteAllFilesFromDirectory(false); }
            }

            foreach (var c in Code)
            {
                if (CodeSettings.NamespaceDrivenProcedures == true)
                {
                    if (c.FileName.Contains("DBSP")) { System.IO.File.WriteAllText(_Folder + c.FileName, c.Code); }
                }
                else
                {
                    if (c.FileName.Contains("DBStoredProcedures")) { System.IO.File.WriteAllText(_Folder + c.FileName.Replace(".cs", "") + "_Base.cs", c.Code); }
                }
            }
        }

        /// <summary>
        /// Generate USER Layer Code
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeSettings">The code settings.</param>
        private void GenerateUserLayer(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            string _Folder = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "User\\";
            if (_Folder.DirectoryExists()) { System.IO.Directory.Delete(_Folder, true); }

            System.Threading.Thread.Sleep(500);
            _Folder.CreateDirectoryStructure(null);

            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(_Folder + c.FileName.Replace(".cs", "") + "_User" + ".cs", c.UserCode);
            }
        }

        /// <summary>
        /// Generate the BASE Layer Code
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeSettings">The code settings.</param>
        private void GenerateBaseLayer(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            string _Folder = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\";

            try
            {
                if (_Folder.DirectoryExists()) { _Folder.DeleteAllFilesFromDirectory(true); }
            }
            catch (Exception ex)
            {
                LogError("ACT_CodeGenerator", "Error Cleaning Output Directory", ex, _Folder, ErrorLevel.Warning);
            }

            System.Threading.Thread.Sleep(500);
            _Folder.CreateDirectoryStructure(null);


            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(_Folder + c.FileName.Replace(".cs", "") + "_Base" + ".cs", c.Code);
            }
        }

        /// <summary>
        /// Generate the Solution and the Project within the Solution
        /// </summary>
        /// <param name="CodeSettings">The code settings.</param>
        private void GenerateCSSolution(I_CodeGenerationSettings CodeSettings)
        {

            string _cssolution_folder = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Solution\\";
        }

        /// <summary>
        /// Generate the CS Project Code.  Updated to VS 2017 (TODO)
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeSettings">The code settings.</param>
        private void GenerateCSProject(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            string _cssolution_folder = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Project\\";
            string _csproject_folder = _cssolution_folder + CodeSettings.DLLName.Replace(".dll", "") + "\\";

            Guid _SolutionGuid = Guid.NewGuid();
            Guid _ProjectGuid = Guid.NewGuid();
            string _Namespace = CodeSettings.NameSpace;
            string _AssemblyName = CodeSettings.DLLName.Replace(".dll", "");

            #region Generate All Folders
            /*   BASE FOLDER / Project / ProjectName / ( Bin , Properties )
             *
            */

            if (System.IO.Directory.Exists(_cssolution_folder))
            {
                _cssolution_folder.DeleteAllFilesFromDirectory(true);
                System.IO.Directory.Delete(CodeSettings.RootOutputDirectory + "Project\\", true);
            }

            System.IO.Directory.CreateDirectory(_cssolution_folder);
            System.IO.Directory.CreateDirectory(_cssolution_folder + "Base\\");
            System.IO.Directory.CreateDirectory(_cssolution_folder + "User\\");
            System.IO.Directory.CreateDirectory(_cssolution_folder + "Bin\\");

            #endregion

            string _BaseItems = "";
            string _UserItems = "";

            foreach (var c in Code)
            {
                System.IO.File.WriteAllText(_cssolution_folder + "Base\\" + c.FileName.Replace(".cs", "") + "_Base" + ".cs", c.Code);
                System.IO.File.WriteAllText(_cssolution_folder + "User\\" + c.FileName.Replace(".cs", "") + "_User" + ".cs", c.UserCode);

                _BaseItems += "\t<Compile Include=\"Base\\" + c.FileName.Replace(".cs", "") + "_Base" + ".cs" + "\" />" + Environment.NewLine;
                _UserItems += "\t<Compile Include=\"User\\" + c.FileName.Replace(".cs", "") + "_User" + ".cs" + "\" />" + Environment.NewLine;
            }

            string _ProjectCode = ACT.Core.SystemSettings.GetSettingByName("C#ProjectTemplate").Value;
            _ProjectCode = _ProjectCode.Replace("#ASSEMBLYNAME#", CodeSettings.DLLName);
            _ProjectCode = _ProjectCode.Replace("#ITEMS#", _BaseItems + _UserItems);
            _ProjectCode = _ProjectCode.Replace("#NEWGUID#", Guid.NewGuid().ToString());

            System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory + "\\Project\\CSharpProject.csproj", _ProjectCode);
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll"))
            {
                System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll", CodeSettings.RootOutputDirectory + "\\Project\\Bin\\ACT.Core.dll");
            }
            else if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll"))
            {
                System.IO.File.Copy(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll", CodeSettings.RootOutputDirectory + "\\Project\\Bin\\ACT.Core.dll");
            }

         //   System.IO.File.WriteAllText(CodeSettings.RootOutputDirectory + "\\Project\\Bin\\SystemConfiguration.xml", ACT.Core.SystemSettings.ExportXMLData());

        }

        /// <summary>
        /// TODO CHECK
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <param name="CodeSettings">The code settings.</param>
        private void GenerateViewAccessCode(List<I_GeneratedCode> Code, I_CodeGenerationSettings CodeSettings)
        {
            string _Folder = CodeSettings.RootOutputDirectory.EnsureDirectoryFormat() + "Base\\";
            if (!System.IO.Directory.Exists(_Folder)) { System.IO.Directory.CreateDirectory(_Folder); }

            foreach (var c in Code)
            {
                if (c.FileName.Contains("ViewAccess")) { System.IO.File.WriteAllText(_Folder + c.FileName.Replace(".cs", "") + "_Base.cs", c.Code); }
            }
        }

        #endregion
    }
}
