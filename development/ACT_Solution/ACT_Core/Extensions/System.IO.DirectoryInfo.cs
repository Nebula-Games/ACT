// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.IO.DirectoryInfo.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Runtime.InteropServices;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class DirectoryInfoExtensions.
    /// </summary>
    public static class DirectoryInfoExtensions
    {


        /// <summary>
        /// Adds the security.
        /// </summary>
        /// <param name="dInfo">The d information.</param>
        /// <param name="Account">The account.</param>
        /// <param name="Rights">The rights.</param>
        /// <param name="ControlType">Type of the control.</param>
        public static void AddSecurity(this DirectoryInfo dInfo, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        /// <summary>
        /// Removes the security.
        /// </summary>
        /// <param name="dInfo">The d information.</param>
        /// <param name="Account">The account.</param>
        /// <param name="Rights">The rights.</param>
        /// <param name="ControlType">Type of the control.</param>
        public static void RemoveSecurity(this DirectoryInfo dInfo, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Get a DirectorySecurity object that represents the  
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }


        /// <summary>
        /// Copies the directory.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public static void CopyDirectory(this DirectoryInfo source, DirectoryInfo target)
        {
            CopyFilesRecursively(source, target);
        }

        /// <summary>
        /// Copies the files recursively.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }

        /// <summary>
        /// Deletes the contents.
        /// </summary>
        /// <param name="source">The source.</param>
        public static void DeleteContents(this DirectoryInfo source)
        {
            foreach (FileInfo file in source.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public static int UnblockAll(this DirectoryInfo x, bool Recursive)
        {
            int _x = 0;
            FileUnblocker _Unblocker = new FileUnblocker();

            if (x == null) { return 0; }

            if (Recursive)
            {
                foreach (var fli in x.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (_Unblocker.Unblock(fli.FullName) == true) { _x = _x + 1; }
                }
            }
            else
            {
                foreach (var fli in x.GetFiles(x.FullName, SearchOption.TopDirectoryOnly))
                {
                    if (_Unblocker.Unblock(fli.FullName) == true) { _x = _x + 1; }
                }
            }

            return _x;
        }

        public class FileUnblocker
        {
            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool DeleteFile(string name);

            public bool Unblock(string fileName)
            {
                return DeleteFile(fileName + ":Zone.Identifier");
            }
        }
    }
}
