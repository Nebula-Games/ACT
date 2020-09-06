// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 03-11-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-11-2019
// ***********************************************************************
// <copyright file="ACT_Directory.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using ACT.Core.Extensions;

namespace ACT.Core.Types.IO
{
    /// <summary>
    /// Represents a Directory In Any Operating system
    /// </summary>
    public class ACT_Directory
    {
        /// <summary>
        /// Contains all the Parts of the Path
        /// </summary>
        public List<string> Parts = new List<string>();
        
        /// <summary>
        /// Drive Letter (windows) etc.
        /// </summary>
        public string DriveLetter;
        /// <summary>
        /// Original Full Path
        /// </summary>
        public string FullPath;
        /// <summary>
        /// Is Network Path
        /// </summary>
        public bool IsNetworkPath;
        /// <summary>
        /// Dropped File Name (Happens when filename is passed)
        /// </summary>
        public string DroppedFileName;
        /// <summary>
        /// Possible File Name (Linux)
        /// </summary>
        public bool PossibleFileName;

        /// <summary>
        /// The available space
        /// </summary>
        long _AvailableSpace = 0;
        /// <summary>
        /// Total Available Space
        /// </summary>
        /// <value>The available space.</value>
        public long AvailableSpace { get { return _AvailableSpace; } }

        /// <summary>
        /// The drive size
        /// </summary>
        long _DriveSize = 0;
        /// <summary>
        /// Total Available Space
        /// </summary>
        /// <value>The size of the drive.</value>
        public long DriveSize { get { return _DriveSize; } }

        /// <summary>
        /// The volume label
        /// </summary>
        string _VolumeLabel = "";
        /// <summary>
        /// Volume Information as Specified
        /// </summary>
        /// <value>The volume label.</value>
        public string VolumeLabel { get { return _VolumeLabel; } }

        /// <summary>
        /// The operating system
        /// </summary>
        private readonly Enums.OperatingSystems _OperatingSystem;
        /// <summary>
        /// Current Operating System
        /// </summary>
        /// <value>The operating system.</value>
        public Enums.OperatingSystems OperatingSystem { get { return _OperatingSystem; } }

        /// <summary>
        /// ACT Directory represents a location on a drive
        /// </summary>
        /// <param name="FullPath">The full path.</param>
        /// <param name="SpecifiedOperatingSystem">The specified operating system.</param>
        /// <exception cref="Exception">Invalid Path For Windows</exception>
        public ACT_Directory(string FullPath, Enums.OperatingSystems SpecifiedOperatingSystem)
        {
            if (SpecifiedOperatingSystem == Enums.OperatingSystems.Windows)
            {
                _OperatingSystem = SpecifiedOperatingSystem;
                if (FullPath.Contains(ACT.Core.Consts.Windows.IO.FileSystem_Consts.WindowsDirectoryIllegalCharacters, true))
                {
                    throw new Exception("Invalid Path For Windows");
                }

                if (FullPath.StartsWith(@"\\"))
                {
                    Parts.Add(@"\\");
                    IsNetworkPath = true;
                    FullPath = FullPath.Right(FullPath.Length - 2);
                }
                else
                {
                    LoadDriveInfo();
                }

                var _Parts = FullPath.SplitString("\\", StringSplitOptions.RemoveEmptyEntries);
                if (_Parts.Last().Contains("."))
                {
                    DroppedFileName = _Parts.Last();
                    foreach (var p in Parts) { if (!p.Contains(".")) { Parts.Add(p); } }
                }

            }
            else if (SpecifiedOperatingSystem == Enums.OperatingSystems.Linux)
            {
                if (FullPath.StartsWith(@"//"))
                {
                    Parts.Add(@"//");
                    IsNetworkPath = true;
                    FullPath = FullPath.Right(FullPath.Length - 2);
                }

                var _Parts = FullPath.SplitString("/", StringSplitOptions.RemoveEmptyEntries);
                Parts = _Parts.ToList();

                if (Parts.Last().Contains(".")) { PossibleFileName = true; }
            }

        }

        /// <summary>
        /// Loads the Drive Information
        /// </summary>
        private void LoadDriveInfo()
        {
            var _driveInfo = System.IO.DriveInfo.GetDrives().Where(x => x.Name.ToLower().StartsWith(Parts[0].Left(1).ToLower())).First();

            if (_driveInfo == null) { return; }
            
            _AvailableSpace = _driveInfo.AvailableFreeSpace;
            _DriveSize = _driveInfo.TotalSize;
            _VolumeLabel = _driveInfo.VolumeLabel;

        }
    }
}
