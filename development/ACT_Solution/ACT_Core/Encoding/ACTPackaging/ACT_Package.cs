// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-10-2019
// ***********************************************************************
// <copyright file="ACT_Package.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using ACT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Encoding.ACTPackaging
{

    /// <summary>
    /// This is a Package File (Similar to a Zip but includes Deduplications As Well)
    /// Limited to 4 Gigs Per Saved File
    /// Use Static Constructor or Load Method to Load Source Directories
    /// </summary>
    public class ACT_Package
    {
        /// <summary>
        /// Notify the caller as it progresses
        /// </summary>
        public event ACT.Core.Delegates.OnCommunication ProcessingUpdate;

        /// <summary>
        /// Base Directory.
        /// </summary>
        public string PackageBaseDirectory = "";

        /// <summary>
        /// The act package header
        /// </summary>
        private ACT_Package_Header _ACTPackageHeader;

        /// <summary>
        /// Holds the ACTPackageHeader Information
        /// </summary>
        /// <value>The act package header.</value>
        public ACT_Package_Header ACTPackageHeader { get { return _ACTPackageHeader; } }

        /// <summary>
        /// The temporary path headers
        /// </summary>
        private List<PackedPathHeader> _tmpPathHeaders = new List<PackedPathHeader>();
        /// <summary>
        /// The temporary file headers
        /// </summary>
        private List<PackedFileHeader> _tmpFileHeaders = new List<PackedFileHeader>();
        /// <summary>
        /// The temporary file data
        /// </summary>
        private List<PackedFileData> _tmpFileData = new List<PackedFileData>();
        /// <summary>
        /// The data position
        /// </summary>
        private ulong _DataPosition = 0;
        /// <summary>
        /// All of the Files Found Each File Is Limited to 4Gigs
        /// </summary>
        public List<Release_File> ReleaseFiles = new List<Release_File>();

        /// <summary>
        /// Returns the Total File Count
        /// </summary>
        /// <value>The loaded file count.</value>
        public int LoadedFileCount { get { return _tmpFileData.Count; } }

        /// <summary>
        /// Update Process Event Check
        /// </summary>
        /// <param name="Msg">The MSG.</param>
        private void UpdateProcess(List<string> Msg)
        {
            if (ProcessingUpdate == null) { return; }
            ProcessingUpdate(Msg);
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public ACT_Package() { }

        /// <summary>
        /// Load the Data From a Root Directory
        /// </summary>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <exception cref="Exception">You can currently only have 1 root directory!</exception>
        public void Load(string BaseDirectory)
        {
            if (!PackageBaseDirectory.NullOrEmpty()) { throw new Exception("You can currently only have 1 root directory!"); }
            PackageBaseDirectory = BaseDirectory;
            Dictionary<string, string> UniqueFiles = new Dictionary<string, string>();
            var _Files = BaseDirectory.GetAllFilesFromPath(true);

            UpdateProcess(new List<string>() { _Files.Count.ToString() + " Files Counted!" });

            foreach (var file in _Files)
            {
                UpdateProcess(new List<string>() { "." });
                Release_File _tmpFile = new Release_File(file, BaseDirectory);
                if (UniqueFiles.ContainsKey(_tmpFile.FileHash))
                {
                    _tmpFile.Duplicated = true;
                    _tmpFile.PointerFile = UniqueFiles[_tmpFile.FileHash];
                }
                else
                {
                    _tmpFile.Duplicated = false;
                    UniqueFiles.Add(_tmpFile.FileHash, _tmpFile.FullPath);
                }

                ReleaseFiles.Add(_tmpFile);
            }

            UpdateProcess(new List<string>() { "Completed Load" });
        }

        /// <summary>
        /// Package the Files Up into a PackageFile
        /// Each File Is Limited to 4Gigs
        /// </summary>
        /// <param name="Directory">Destination Directory</param>
        /// <param name="PasswordVerification">The password verification.</param>
        /// <returns>File Path</returns>
        public string PackageUp(string Directory, string PasswordVerification)
        {
            string _tmpReturn = "";
            //List<PackedPathHeader> _PathHeaders = new List<PackedPathHeader>();
            //List<PackedFileHeader> _PathFileHeaders = new List<PackedFileHeader>();
            //List<PackedFileData> _PathFileData = new List<PackedFileData>();

            //Dictionary<string, uint> PointerData = new Dictionary<string, uint>();

            ///// Get the Unique Files
            //foreach (var file in ReleaseFiles.Where(x => x.Duplicated == false))
            //{
            //    PackedFileData _tmpData = new PackedFileData();
            //    _tmpData.DataID = (ushort)_PathFileData.Count();
            //    _tmpData.DataLength = file.FileSize;

            //    PointerData.Add(file.FileHash, _tmpData.DataID);

            //    PackedPathHeader _tmpPath = new PackedPathHeader();
            //    _tmpPath.PathID = (ushort)_PathFileHeaders.Count();
            //    _tmpPath.Path = file.BaseFolderName.ToArray();
            //    _tmpPath.PathCharacterLength = (ushort)_tmpPath.Path.Length;

            //    PackedFileHeader _tmpHeader = new PackedFileHeader();
            //    _tmpHeader.DataID = _tmpData.DataID;
            //    _tmpHeader.PathID = _tmpPath.PathID;
            //    _tmpHeader.FileName = file.FileName.ToArray();
            //    _tmpHeader.FileNameCharacterLength = (ushort)_tmpHeader.FileName.Length;

            //    _PathFileHeaders.Add(_tmpHeader);
            //    _PathHeaders.Add(_tmpPath);
            //    _PathFileData.Add(_tmpData);
            //}

            ///// Get the Duplicate Files
            //foreach (var file in ReleaseFiles.Where(x => x.Duplicated == true))
            //{

            //    PackedPathHeader _tmpPath = new PackedPathHeader();
            //    _tmpPath.PathID = (ushort)_PathFileHeaders.Count();
            //    _tmpPath.Path = file.BaseFolderName.ToArray();
            //    _tmpPath.PathCharacterLength = (ushort)_tmpPath.Path.Length;

            //    PackedFileHeader _tmpHeader = new PackedFileHeader();
            //    _tmpHeader.DataID = PointerData[file.FileHash];
            //    _tmpHeader.PathID = _tmpPath.PathID;
            //    _tmpHeader.FileName = file.FileName.ToArray();
            //    _tmpHeader.FileNameCharacterLength = (ushort)_tmpHeader.FileName.Length;

            //    _PathFileHeaders.Add(_tmpHeader);
            //    _PathHeaders.Add(_tmpPath);
            //}

            //ACT_Package_Header _tmpVersionHeader = new ACT_Package_Header();
            //_tmpVersionHeader.CreationDate = DateTime.Now.ToUnixTime();
            //_tmpVersionHeader.HashCharacters = _tmpVersionHeader.CreationDate.ToString().ToSHA512().ToArray();
            //_tmpVersionHeader.HashLength = (ushort)_tmpVersionHeader.HashCharacters.Length;
            //_tmpVersionHeader.NumberOfFileHeaders = (uint)_PathFileHeaders.Count();
            //_tmpVersionHeader.NumberOfPathHeaders = (uint)_PathHeaders.Count();
            //_tmpVersionHeader.NumberOfDataHeaders = (uint)_PathFileData.Count();
            //_tmpVersionHeader.PWCharacters = PasswordVerification.ToSHA512().ToCharArray();
            //_tmpVersionHeader.PWLength = _tmpVersionHeader.PWCharacters.Length.ToUShort();
            //_tmpVersionHeader.BasePath = PackageBaseDirectory.ToCharArray();
            //_tmpVersionHeader.BasePathLength = _tmpVersionHeader.BasePath.Length.ToUShort();
            //_tmpVersionHeader.VersionID = 1;

            //if (Directory.EndsWith(".ACTP", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    _tmpReturn = Directory;
            //}
            //else
            //{
            //    string _tmpFileName = _tmpVersionHeader.CreationDate.ToString() + ".ACTP";
            //    _tmpReturn = Directory.EnsureDirectoryFormat() + _tmpFileName;
            //}

            //List<byte> _DataBytes = new List<byte>();
            ///// SET Output File Path as the Return Value

            //using (System.IO.BinaryWriter _BF = new System.IO.BinaryWriter(new System.IO.FileStream(_tmpReturn, System.IO.FileMode.Create)))
            //{
            //    ulong _fileOffset = 0;
            //    /// First Write the Version Information
            //    byte[] _VersionHeaderBytes = _tmpVersionHeader.Export();
            //    _BF.Write(_VersionHeaderBytes);
            //    _fileOffset = _fileOffset + _VersionHeaderBytes.Length.ToULong();
            //    // Cleanup
            //    _VersionHeaderBytes = null;

            //    Console.WriteLine("Export Position Before Path Header " + _BF.BaseStream.Position.ToString());

            //    /// Write All Path Header Data To Packed File
            //    for (int ppos = 0; ppos < _PathHeaders.Count(); ppos++)
            //    {
            //        var _pathHeader = _PathHeaders[ppos];
            //        byte[] _PathHeaderBytes = _pathHeader.Export();
            //        _fileOffset = _fileOffset + _PathHeaderBytes.Length.ToULong();
            //        _BF.Write(_PathHeaderBytes);
            //    }

            //    Console.WriteLine("Export Position Before File Header " + _BF.BaseStream.Position.ToString());

            //    /// Write All Path File Header To Packed File
            //    for (int pfhpos = 0; pfhpos < _PathFileHeaders.Count(); pfhpos++)
            //    {
            //        var _pathFileHeader = _PathFileHeaders[pfhpos];
            //        byte[] _PathFileHeaderBytes = _pathFileHeader.Export();
            //        _fileOffset = _fileOffset + _PathFileHeaderBytes.Length.ToULong();
            //        _BF.Write(_PathFileHeaderBytes);
            //    }


            //    Console.WriteLine("Export Position Before File Data " + _BF.BaseStream.Position.ToString());
            //    ulong _tmpFileOffset = _fileOffset;

            //    /// Unfortunate Step Can Speed up By Inserting SizeOf Function For PathFileData Array
            //    for (int ppos = 0; ppos < _PathFileData.Count(); ppos++)
            //    {
            //        var _pathData = _PathFileData[ppos];
            //        byte[] _PathFileBytes = _pathData.Export();
            //        _tmpFileOffset = _tmpFileOffset + _PathFileBytes.Length.ToULong();
            //    }

            //    /// Write All Path File Data To Packed File
            //    for (int ppos = 0; ppos < _PathFileData.Count(); ppos++)
            //    {
            //        var _pathData = _PathFileData[ppos];
            //        _pathData.ContentStartPosition = _tmpFileOffset;
            //        byte[] _PathFileBytes = _pathData.Export();
            //        _BF.Write(_PathFileBytes);
            //        _tmpFileOffset = _tmpFileOffset + _pathData.DataLength.ToULong();
            //    }

            //    /// Write All Data To Packed File
            //    for (int fpos = 0; fpos < _PathFileData.Count(); fpos++)
            //    {
            //        var fileData = _PathFileData[fpos];
            //        var _File = ReleaseFiles.Where(x => x.FileHash == PointerData.Where(xx => xx.Value == fileData.DataID).First().Key).First();
            //        using (BinaryReader streamReader = new BinaryReader(new FileStream(_File.FullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            //        {
            //            byte[] buffer = new byte[2048];
            //            int bytesRead;
            //            while ((bytesRead = streamReader.Read(buffer, 0, buffer.Length)) > 0)
            //            {
            //                _BF.Write(buffer, 0, bytesRead);
            //            }
            //        }
            //    }
            //    // string _FileCheck = "MarkAlicz";
            //    //  var _FileCheckCharacters = Encoding.Unicode.GetBytes(_FileCheck);
            //    // _BF.Write(_FileCheckCharacters);
            //}

            return _tmpReturn;
        }

        /// <summary>
        /// Imports the specified source file path.
        /// </summary>
        /// <param name="SourceFilePath">The source file path.</param>
        /// <param name="DestinationPath">The destination path.</param>
        public void Import(string SourceFilePath, string DestinationPath = "")
        {
            /*
            using (ACT.Core.Types.IO.ACTBinaryReader _reader = new Core.Types.IO.ACTBinaryReader(new System.IO.FileStream(SourceFilePath, System.IO.FileMode.Open)))
            {
                _tmpVersionHeader = ACT_Package_Header.Import(_reader);

                Console.WriteLine("Import Position Before Path Header " + _reader.BaseStream.Position.ToString());

                for (int phx = 0; phx < _tmpVersionHeader.NumberOfPathHeaders; phx++)
                {
                    _tmpPathHeaders.Add(PackedPathHeader.Import(_reader));
                }

                Console.WriteLine("Import Position Before File Header " + _reader.BaseStream.Position.ToString());

                for (int pfhx = 0; pfhx < _tmpVersionHeader.NumberOfFileHeaders; pfhx++)
                {
                    _tmpFileHeaders.Add(PackedFileHeader.Import(_reader));
                }

                Console.WriteLine("Import Position Before FileData " + _reader.BaseStream.Position.ToString());

                for (int phx = 0; phx < _tmpVersionHeader.NumberOfDataHeaders; phx++)
                {
                    _tmpFileData.Add(PackedFileData.Import(_reader));
                }

                // IMPORT THE FILES

                this.ReleaseFiles = new List<Release_File>();

                // Loop Over All Files
                for (int fx = 0; fx < _tmpFileHeaders.Count(); fx++)
                {
                    Release_File _tmpFile = new Release_File();

                    _tmpFile.Duplicated = false;
                    _tmpFile.Error = false;
                    _tmpFile.FileHash = "";
                    _tmpFile.FileName = String.Join("", _tmpFileHeaders[fx].FileName);
                    _tmpFile.FileRootFolder = String.Join("", _tmpVersionHeader.BasePath);
                    _tmpFile.FileSize = _tmpFileData.First(x => x.DataID == _tmpFileHeaders[fx].DataID).DataLength;
                    _tmpFile.BaseFolderName = String.Join("", _tmpPathHeaders.First(x => x.PathID == _tmpFileHeaders[fx].PathID).Path);
                    _tmpFile.FullPath = _tmpFile.FileRootFolder.EnsureDirectoryFormat() + _tmpFile.BaseFolderName.TrimStart("\\").EnsureDirectoryFormat() + _tmpFile.FileName;
                    _tmpFile.PointerFile = "";
                    _tmpFile.DataID = _tmpFileHeaders[fx].DataID;

                    this.PackageBaseDirectory = String.Join("", _tmpVersionHeader.BasePath);
                    this.ReleaseFiles.Add(_tmpFile);
                }

                _DataPosition = (ulong)_reader.BaseStream.Position;
                if (DestinationPath != "")
                {
                    if (DestinationPath.DirectoryExists(true) == false) { throw new Exception("Data Imported However, Destination Path Doesnt Exist"); }

                    foreach (var file in ReleaseFiles)
                    {
                        var _dataInfo = _tmpFileData.First(x => x.DataID == file.DataID);

                        byte[] buffer = new byte[2048];

                        ulong _tmpIndex = _dataInfo.ContentStartPosition;
                        _reader.BaseStream.Seek((long)_tmpIndex, SeekOrigin.Begin);
                        byte[] _FileData = new byte[_dataInfo.DataLength];

                        if (_dataInfo.DataLength > int.MaxValue)
                        {
                            byte[] _FirstFileData = _reader.ReadBytes(int.MaxValue);
                            byte[] _FinalFileData = _reader.ReadBytes((_dataInfo.DataLength - int.MaxValue).ToInt());
                            List<byte> _tmpBytes = new List<byte>();
                            _tmpBytes.AddRange(_FirstFileData);
                            _tmpBytes.AddRange(_FinalFileData);
                            _FileData = _tmpBytes.ToArray();
                        }
                        else
                        {
                            _FileData = _reader.ReadBytes(_dataInfo.DataLength.ToInt());
                        }

                        string _OutputDirectory = DestinationPath.EnsureDirectoryFormat() + file.BaseFolderName.TrimStart("\\").EnsureDirectoryFormat();
                        var _tmpOut = _OutputDirectory.DirectoryExists(true);

                        if (_tmpOut == false) { throw new Exception("File Is Unable To Be Written"); }
                        else
                        {
                            using (var _writer = new BinaryWriter(new FileStream(_OutputDirectory + file.FileName, FileMode.Create)))
                            {
                                _writer.Write(_FileData);
                            }
                        }

                    }
                }
            }
            */
        }
    }


}