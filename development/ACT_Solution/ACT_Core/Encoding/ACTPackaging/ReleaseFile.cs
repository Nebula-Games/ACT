// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-10-2019
// ***********************************************************************
// <copyright file="ReleaseFile.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;
using System.IO;

namespace ACT.Core.Encoding.ACTPackaging
{
    /// <summary>
    /// Release File Holds information about a Single File 
    /// </summary>
    /// <summary>
    /// Class Release_File.
    /// </summary>
    public class Release_File
    {
        /// <summary>
        /// The Root Folder common to all Files
        /// </summary>
        /// <summary>
        /// The file root folder
        /// </summary>
        public string FileRootFolder;
        /// <summary>
        /// Base Folder Name (Holds the Directory After the Root Folder
        /// i.e.    Root Folder     = c:\temp\directory\
        ///         FullPath        = c:\temp\directory\somedir\dir\file.txt
        ///         BaseFolderName  = somedir\dir\
        /// </summary>
        /// <summary>
        /// The base folder name
        /// </summary>
        public string BaseFolderName;
        /// <summary>
        /// File Name = File Name Only
        /// </summary>
        public string FileName;
        /// <summary>
        /// Full Original File Path Including Root Folder
        /// </summary>
        public string FullPath;
        /// <summary>
        /// Hash Value of the File
        /// </summary>
        public string FileHash;
        /// <summary>
        /// Duplicated True/False
        /// </summary>
        public bool Duplicated;
        /// <summary>
        /// Pointer to the File That Holds the Data
        /// </summary>
        public string PointerFile;
        /// <summary>
        /// File Size in Bytes
        /// </summary>
        public uint FileSize;
        /// <summary>
        /// Error True / False
        /// True Means Unable To Add File
        /// </summary>
        public bool Error = false;

        /// <summary>
        /// Data ID Used for Package Extraction
        /// </summary>
        public uint DataID = 0;

        /// <summary>
        /// Empty Constructor used when unpacking
        /// </summary>
        public Release_File() { }

        /// <summary>
        /// Constructor with FilePath and RootFolder
        /// </summary>
        /// <param name="FilePath">The file path.</param>
        /// <param name="RootFolder">The root folder.</param>
        public Release_File(string FilePath, string RootFolder)
        {
            FileRootFolder = RootFolder;
            FullPath = FilePath;
            BaseFolderName = FullPath.GetDirectoryFromFileLocation().EnsureDirectoryFormat().Replace(RootFolder, "");
            FileName = FilePath.GetFileNameFromFullPath();

            using (System.IO.FileStream _stream = new FileStream(FilePath, FileMode.Open))
            {
                _stream.Position = 0;
                FileHash = _stream.GetSHA512Hash();
            }

            try { FileSize = (uint)FilePath.GetFileSize(); } catch { Error = true; }
            if (FileSize == (uint)0)
            {
                Error = true;
            }
        }
    }
}
