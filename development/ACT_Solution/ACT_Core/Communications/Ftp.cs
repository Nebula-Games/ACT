// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-18-2019
// ***********************************************************************
// <copyright file="Ftp.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ACT.Core.Extensions;
using FluentFTP;
using Renci.SshNet;

namespace ACT.Core.Communications
{
    /// <summary>
    /// File Transfer Protocal Methods
    /// </summary>
    public static class FTP_SFTP
    {


        /// <summary>
        /// Uploads the SFTP file.
        /// </summary>
        /// <param name="LocalFile">The local file.</param>
        /// <param name="PathInfo">The path information.</param>
        /// <param name="ConnectionInfo">The connection information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool UploadSftpFile(string LocalFile, string PathInfo, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            try
            {
                using (var sftp = new SftpClient(ConnectionInfo.IPAddress, ConnectionInfo.PortNumber, ConnectionInfo.UserName, ConnectionInfo.PassWord))
                {
                    try { sftp.Connect(); } catch { return false; }
                    if (sftp.IsConnected == false) { return false; }

                    using (var stream = new FileStream(LocalFile, FileMode.Create))
                    {
                        sftp.UploadFile(stream, PathInfo);
                    }

                    sftp.Disconnect();
                }
            }
            catch (Exception _Ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Windows.SFTPMethod:UploadFile", _Ex.Message, _Ex, Enums.ErrorLevel.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// FTPs the list dir.
        /// </summary>
        /// <param name="PathInfo">The path information.</param>
        /// <param name="ConnectionInfo">The connection information.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private static List<string> FTPListDir(string PathInfo, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            List<string> _tmpReturn = new List<string>();

            // connect to the FTP server
            using (FtpClient client = new FtpClient())
            {
                client.Host = ConnectionInfo.IPAddress;
                client.Credentials = new NetworkCredential(ConnectionInfo.UserName, ConnectionInfo.PassWord);

                try { client.Connect(); } catch { return _tmpReturn; }
                if (client.IsConnected == false) { return _tmpReturn; }

                var _Files = client.GetListing(PathInfo);

                if (_Files.Length > 0)
                {
                    _tmpReturn = _Files.Select(x => x.FullName).ToList();
                    client.Disconnect();
                    return _tmpReturn;
                }
                else
                {
                    client.Disconnect();
                    return _tmpReturn;
                }
            }
        }
        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="LocalFile">The local file.</param>
        /// <param name="RemotePathInfo">The remote path information.</param>
        /// <param name="ConnectionInfo">The connection information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool DownloadFile(string LocalFile, string RemotePathInfo, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            try
            {
                using (FtpClient conn = new FtpClient())
                {
                    conn.Host = ConnectionInfo.IPAddress;
                    conn.Credentials = new NetworkCredential(ConnectionInfo.UserName, ConnectionInfo.PassWord);

                    using (Stream ostream = conn.OpenRead(RemotePathInfo))
                    {
                        var _FileData = ostream.ReadFully();
                        //TODO TEST ANSII,UTF8,BINARY, ETC
                        System.IO.File.WriteAllBytes(LocalFile, _FileData);
                    }
                    conn.Disconnect();
                }
                return true;
            }
            catch (Exception _Ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Windows.FTPMethod:UploadFile", _Ex.Message, _Ex, Enums.ErrorLevel.Warning);
                return false;
            }
        }
        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="DirectoryName">Name of the directory.</param>
        /// <param name="RemotePathInfo">The remote path information.</param>
        /// <param name="ConnectionInfo">The connection information.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CreateDirectory(string DirectoryName, string RemotePathInfo, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            try
            {
                using (FtpClient conn = new FtpClient())
                {
                    conn.Host = ConnectionInfo.IPAddress;
                    conn.Credentials = new NetworkCredential(ConnectionInfo.UserName, ConnectionInfo.PassWord);

                    try
                    {
                        conn.CreateDirectory(RemotePathInfo.EnsureEndsWith("/") + DirectoryName, true);
                    }
                    catch
                    {
                        conn.Disconnect();
                        return false;
                    }

                    conn.Disconnect();
                }                
                return true;
            }
            catch (Exception _Ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Windows.FTPMethod:UploadFile", _Ex.Message, _Ex, Enums.ErrorLevel.Warning);
                return false;
            }
        }



        /// <summary>
        /// Upload a file to a SFTP or FTP Server MaX 2 GIG
        /// </summary>
        /// <param name="UseSFTP">True/False</param>
        /// <param name="LocalFile">Local File To Upload</param>
        /// <param name="RemotePathInfo">FTP Server Path</param>
        /// <param name="ConnectionInfo"><seealso cref="Types.Communication.ACT_FTPConnection" /></param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool UploadFile(bool UseSFTP, string LocalFile, string RemotePathInfo, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            if (LocalFile.FileExists() == false)
            {
                ACT.Core.Helper.ErrorLogger.LogError("FTP.UploadFile", "Local file Doesnt Exist", LocalFile, null, Enums.ErrorLevel.Severe);
                return false;
            }

            if (UseSFTP)
            {
                return UploadSftpFile(LocalFile, RemotePathInfo, ConnectionInfo);
            }

            try
            {
                using (FtpClient conn = new FtpClient())
                {
                    conn.Host = ConnectionInfo.IPAddress;
                    conn.Credentials = new NetworkCredential(ConnectionInfo.UserName, ConnectionInfo.PassWord);

                    byte[] _FileData = System.IO.File.ReadAllBytes(LocalFile);

                    using (Stream ostream = conn.OpenWrite(RemotePathInfo))
                    {
                        try
                        {
                            ostream.Write(_FileData, 0, _FileData.Length);
                        }
                        finally
                        {
                            ostream.Close();
                        }
                    }
                }
                return true;
            }
            catch (Exception _Ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Windows.FTPMethod:UploadFile", _Ex.Message, _Ex, Enums.ErrorLevel.Warning);
                return false;
            }
        }

        /// <summary>
        /// List all Files in a Directory
        /// </summary>
        /// <param name="UseSFTP">Use SFTP</param>
        /// <param name="PathInfo">The path information.</param>
        /// <param name="ConnectionInfo">The connection information.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> ListDirectory(bool UseSFTP, string PathInfo, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            List<string> _tmpReturn = new List<string>();

            if (UseSFTP)
            {
                using (var sftp = new SftpClient(ConnectionInfo.IPAddress, ConnectionInfo.PortNumber, ConnectionInfo.UserName, ConnectionInfo.PassWord))
                {
                    try { sftp.Connect(); }
                    catch (Exception ex)
                    {
                        Helper.ErrorLogger.LogError("Ftp", "Connection Error", ConnectionInfo.ToString(), ex, Enums.ErrorLevel.Critical);
                        return _tmpReturn;
                    }

                    _tmpReturn = sftp.ListDirectory(PathInfo).Select(x => x.Name).ToList();
                    sftp.Disconnect();
                    return _tmpReturn;
                }
            }
            else
            {
                return FTPListDir(PathInfo, ConnectionInfo);
            }
        }

        /// <summary>
        /// Download a File using SFTP
        /// </summary>
        /// <param name="UseSFTP">True/False</param>
        /// <param name="LocalFileLocation">c:\temp\temp.xlsx</param>
        /// <param name="FTPPath">/SomeDirectory/SomeDir/File.xlsx</param>
        /// <param name="ConnectionInfo"><seealso cref="ACT.Core.Types.Communication.ACT_FTPConnection" /></param>
        /// <returns>True/False</returns>
        public static bool DownloadFile(bool UseSFTP, string LocalFileLocation, string FTPPath, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            if (UseSFTP==false)
            {
                return DownloadFile(LocalFileLocation, FTPPath, ConnectionInfo);
            }
            else
            {
                using (var sftp = new SftpClient(ConnectionInfo.IPAddress, ConnectionInfo.PortNumber, ConnectionInfo.UserName, ConnectionInfo.PassWord))
                {
                    try { sftp.Connect(); } catch { return false; }
                    if (sftp.IsConnected == false) { return false; }

                    using (Stream stream = File.Create(LocalFileLocation))
                    {
                        sftp.DownloadFile(FTPPath, stream);
                    }

                    sftp.Disconnect();
                    return true;
                }
            }
        }

        /// <summary>
        /// Create the directory
        /// </summary>
        /// <param name="UseSFTP">Use Secure FTP or Normal FTP</param>
        /// <param name="DirectoryName">Name of the Directory</param>
        /// <param name="FTPPath">Path To The FTP Folder To create The Directory In</param>
        /// <param name="ConnectionInfo">Connection Information</param>
        /// <returns>True/False</returns>
        public static bool CreateDirectory(bool UseSFTP, string DirectoryName, string FTPPath, Types.Communication.ACT_FTPConnection ConnectionInfo)
        {
            if (UseSFTP)
            {
                return CreateDirectory(DirectoryName, FTPPath, ConnectionInfo);
            }
            else
            {
                using (var sftp = new SftpClient(ConnectionInfo.IPAddress, ConnectionInfo.PortNumber, ConnectionInfo.UserName, ConnectionInfo.PassWord))
                {
                    try { sftp.Connect(); } catch { return false; }
                    if (sftp.IsConnected == false) { return false; }

                    try { sftp.CreateDirectory(FTPPath.EnsureEndsWith("/") + DirectoryName); }
                    catch
                    {
                        sftp.Disconnect();
                        return false;
                    }

                    sftp.Disconnect();
                    return true;
                }
            }
        }
    }
}
