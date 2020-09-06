// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_Box.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Storage
{
    /// <summary>
    /// Interface I_ACT_Box
    /// </summary>
    public interface I_ACT_Box
    {
        /// <summary>
        /// Initializes the storage.
        /// </summary>
        void InitializeStorage();
        /// <summary>
        /// Gets the file count.
        /// </summary>
        /// <value>The file count.</value>
        uint FileCount { get; }
        /// <summary>
        /// Gets the size of the disk.
        /// </summary>
        /// <value>The size of the disk.</value>
        uint DiskSize { get; }
        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="fileData">The file data.</param>
        void ReadFile(byte[] fileData);
        /// <summary>
        /// Exports to file.
        /// </summary>
        /// <returns>System.Byte[].</returns>
        byte[] ExportToFile();
        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileData">The file data.</param>
        /// <returns>System.UInt32.</returns>
        uint AddFile(byte[] fileName, byte[] fileData);
        /// <summary>
        /// Adds the download file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <param name="downloadEndpoint">The download endpoint.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <param name="authenticatorCode">The authenticator code.</param>
        /// <returns>System.UInt32.</returns>
        uint AddDownloadFile(byte[] fileName, byte[] relativePath, string downloadEndpoint, byte[] userName = null, byte[] passWord = null, byte[] authenticatorCode = null);
        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="entryID">The entry identifier.</param>
        /// <returns>System.UInt32.</returns>
        uint RemoveEntry(uint entryID);
        /// <summary>
        /// Finds the entry.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="relativePath">The relative path.</param>
        /// <returns>System.UInt32.</returns>
        uint FindEntry(byte[] fileName, byte[] relativePath);
        /// <summary>
        /// Decrypts the file.
        /// </summary>
        /// <param name="entryID">The entry identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <param name="authenticatorCode">The authenticator code.</param>
        /// <returns>System.Byte[].</returns>
        byte[] DecryptFile(uint entryID, byte[] userName, byte[] passWord, byte[] authenticatorCode = null);
        /// <summary>
        /// Encrypts the entry.
        /// </summary>
        /// <param name="entryID">The entry identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <param name="authenticatorCode">The authenticator code.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool EncryptEntry(uint entryID, byte[] userName, byte[] passWord, byte[] authenticatorCode = null);
        /// <summary>
        /// Exports all.
        /// </summary>
        /// <param name="exportPath">The export path.</param>
        /// <param name="downloadFiles">The download files.</param>
        /// <returns>System.UInt32.</returns>
        uint ExportAll(string exportPath, string downloadFiles);

    }
}
