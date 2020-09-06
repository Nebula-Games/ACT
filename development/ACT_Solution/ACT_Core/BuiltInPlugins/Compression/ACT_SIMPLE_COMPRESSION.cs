// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_SIMPLE_COMPRESSION.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.IO.Compression;

using ACT.Core.Extensions;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.BuiltInPlugins.Compression
{
    /*
    
    /// <summary>
    /// SIMPLE VERSION Uses
    /// ZipSettings.ZipFileLocation, ZipSettings.ZipFileName
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Compression" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Compression" />
    public class ACT_SIMPLE_COMPRESSION : ACT.Core.Interfaces.Common.I_Compression
    {
        /// <summary>
        /// Add File To Zip
        /// </summary>
        /// <param name="ZipSettings">Zip Location + Zip FileName + FilesToAdd</param>
        /// <returns>I_TestResultExpanded.</returns>
        public I_TestResultExpanded AddFileToZip(Compressed_File_Settings ZipSettings)
        {
            Common.ACT_TestResultExpanded _tmpReturn = new Common.ACT_TestResultExpanded();

            if (ZipSettings.FullPathToZip.FileExists() == false)
            {
                _tmpReturn.Success = false;
                _tmpReturn.Messages.Add("Unable To Find Zip File");
                return _tmpReturn;
            }

            if (ZipSettings.FilesToAddUpdate == null || ZipSettings.FilesToAddUpdate.Count == 0)
            {
                _tmpReturn.Success = false;
                _tmpReturn.Messages.Add("No Files Specified");
                return _tmpReturn;
            }

            try
            {
                using (var zipF = ZipFile.Open(ZipSettings.FullPathToZip, ZipArchiveMode.Update))
                {
                    foreach (string file in ZipSettings.FilesToAddUpdate)
                    {
                        var fInfo = new FileInfo(file);
                        zipF.CreateEntryFromFile(fInfo.FullName, fInfo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError(this, "Error Adding File To Archive", ex, Enums.ErrorLevel.Warning);
                _tmpReturn.Success = false;
                _tmpReturn.Messages.Add("Error Adding Files To Archive");
                _tmpReturn.Exceptions.Add(ex);
                return _tmpReturn;
            }

            _tmpReturn.Success = true;
            return _tmpReturn;
        }

        /// <summary>
        /// Compress Data
        /// </summary>
        /// <param name="ZipSettings">Settings</param>
        /// <returns>Compressed_Data_Structure.</returns>
        /// <exception cref="Exception">Not Implemented</exception>
        public Compressed_Data_Structure CompressData(Compressed_File_Settings ZipSettings)
        {
            throw new Exception("Not Implemented");
        }

        /// <summary>
        /// Compess the Files Into a Zip File
        /// </summary>
        /// <param name="ZipSettings">Settings</param>
        /// <returns>Compressed_Data_Structure.</returns>
        /// <exception cref="Exception">
        /// Unable To Find Output Location File
        /// or
        /// No Files Specified
        /// or
        /// Error Adding Files To Archive
        /// </exception>
        public Compressed_Data_Structure CompressFile(Compressed_File_Settings ZipSettings)
        {
            ulong _OSize = 0;

            if (ZipSettings.ZipFileLocation.DirectoryExists() == false)
            {
                throw new Exception("Unable To Find Output Location File");
            }
            if (ZipSettings.FilesToAddUpdate == null || ZipSettings.FilesToAddUpdate.Count == 0)
            {
                throw new Exception("No Files Specified");
            }
            try
            {
                using (var zipF = ZipFile.Open(ZipSettings.FullPathToZip, ZipArchiveMode.Update))
                {
                    foreach (string file in ZipSettings.FilesToAddUpdate)
                    {
                        var fInfo = new FileInfo(file);
                        _OSize += (ulong)fInfo.Length;
                        zipF.CreateEntryFromFile(fInfo.FullName, fInfo.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError(this, "Error Adding File To Archive", ex, Enums.ErrorLevel.Warning);
                throw new Exception("Error Adding Files To Archive");
            }

            return new Compressed_Data_Structure() { OutputSize = _OSize, FolderName = ZipSettings.ZipFileLocation, FileName = ZipSettings.ZipFileName };
        }

        /// <summary>
        /// Compress Folder
        /// </summary>
        /// <param name="ZipSettings">Settings</param>
        /// <returns>I_TestResultExpanded.</returns>
        public I_TestResultExpanded CompressFolder(Compressed_File_Settings ZipSettings)
        {
            I_TestResultExpanded _tmpReturn = new ACT.Core.BuiltInPlugins.Common.ACT_TestResultExpanded();

            try
            {
                ZipFile.CreateFromDirectory(ZipSettings.SourceFolder, ZipSettings.FullPathToZip);
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError(this, "Error Creating Zip File From Directory", ex, Enums.ErrorLevel.Informational);
                _tmpReturn.Success = false;
                _tmpReturn.Messages.Add("Unable To Create Zip File");
                _tmpReturn.Exceptions.Add(ex);
                return _tmpReturn;
            }

            _tmpReturn.Success = true;
            _tmpReturn.Messages.Add("Added Folder to Zip File");
            return _tmpReturn;
        }

        /// <summary>
        /// Decompress The Data In a Zip File
        /// </summary>
        /// <param name="ZipSettings">Settings</param>
        /// <returns>Compressed_Data_Structure.</returns>
        /// <exception cref="Exception">Not Implemented Use UnZipFile</exception>
        public Compressed_Data_Structure DeCompressData(Compressed_File_Settings ZipSettings)
        {
            throw new Exception("Not Implemented Use UnZipFile");
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="ZipSettings">Settings</param>
        /// <returns>Compressed_Data_Structure.</returns>
        /// <exception cref="Exception">Not Implemented Use UnZipFile</exception>
        public Compressed_Data_Structure DeCompressFile(Compressed_File_Settings ZipSettings)
        {
            throw new Exception("Not Implemented Use UnZipFile");
        }

        /// <summary>
        /// Unzip File
        /// </summary>
        /// <param name="ZipSettings">The zip settings.</param>
        /// <returns>I_TestResultExpanded.</returns>
        public I_TestResultExpanded UnzipFile(Compressed_File_Settings ZipSettings)
        {
            I_TestResultExpanded _tmpReturn = new ACT.Core.BuiltInPlugins.Common.ACT_TestResultExpanded();
            
            try
            {
                using (var _ZipArchive = ZipFile.OpenRead(ZipSettings.FullPathToZip))
                {
                    var _Entry = _ZipArchive.GetEntry(ZipSettings.FilesToExtract[0]);
                    _Entry.ExtractToFile(ZipSettings.SourceFolder.EnsureDirectoryFormat() + _Entry.FullName);
                }
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError(this, "Error Unzipping Zip File", ex, Enums.ErrorLevel.Informational);
                _tmpReturn.Success = false;
                _tmpReturn.Messages.Add("Unable To Extract Data From Zip File");
                _tmpReturn.Exceptions.Add(ex);
                return _tmpReturn;
            }

            _tmpReturn.Success = true;
            _tmpReturn.Messages.Add("File Extracted");            
            return _tmpReturn;
        }

        /// <summary>
        /// Unzip Files
        /// </summary>
        /// <param name="ZipSettings">The zip settings.</param>
        /// <returns>I_TestResultExpanded.</returns>
        public I_TestResultExpanded UnzipFiles(Compressed_File_Settings ZipSettings)
        {
            I_TestResultExpanded _tmpReturn = new ACT.Core.BuiltInPlugins.Common.ACT_TestResultExpanded();

            try
            {
                using (var _ZipArchive = ZipFile.OpenRead(ZipSettings.FullPathToZip))
                {
                    _ZipArchive.ExtractToDirectory(ZipSettings.SourceFolder);                    
                }
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError(this, "Error Unzipping Zip File", ex, Enums.ErrorLevel.Informational);
                _tmpReturn.Success = false;
                _tmpReturn.Messages.Add("Unable To Extract Data From Zip File");
                _tmpReturn.Exceptions.Add(ex);
                return _tmpReturn;
            }

            _tmpReturn.Success = true;
            _tmpReturn.Messages.Add("Files Extracted");
            return _tmpReturn;
        }
    }
    */
}
