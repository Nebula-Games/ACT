///-------------------------------------------------------------------------------------------------
// file:	Windows\FileSystem\DirectoryServices.cs
//
// summary:	Implements the directory services class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using ACT.Core.Extensions;


namespace ACT.Core.Windows.FileSystem
{
   public static class DirectoryServices
    {
        public static void AddDirectorySecurity(string FolderName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FolderName);

            dInfo.AddSecurity(Account, Rights, ControlType);
        }

        // Removes an ACL entry on the specified directory for the specified account. 
        public static void RemoveDirectorySecurity(string FolderName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FolderName);

            dInfo.RemoveSecurity(Account, Rights, ControlType);
        }
    }
}
