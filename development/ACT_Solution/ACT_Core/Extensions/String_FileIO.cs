#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Drawing;
using Microsoft.Win32;
using System.Runtime.InteropServices;
#endregion


namespace ACT.Core.Extensions
{
    /// <summary>
    /// String FILE IO Extension Class
    /// </summary>
    /// <summary>
    /// Class String_FileIO.
    /// </summary>
    public static class String_FileIO
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern uint ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        public static Icon IconFromExtension(this string extension, bool LargeImage)
        {
            // Add the '.' to the extension if needed
            if (extension[0] != '.') extension = '.' + extension;

            //opens the registry for the wanted key.
            RegistryKey Root = Registry.ClassesRoot;
            RegistryKey ExtensionKey = Root.OpenSubKey(extension);
            ExtensionKey.GetValueNames();
            RegistryKey ApplicationKey =
                Root.OpenSubKey(ExtensionKey.GetValue("").ToString());

            RegistryKey CurrentVer = null;
            try
            {
                CurrentVer = Root.OpenSubKey(ApplicationKey.OpenSubKey("CurVer").GetValue("").ToString());
            }
            catch (Exception ex)
            {
                Helper.ErrorLogger.VLogError("String_FileIO.cs - Missing CurVer", ex.Message, ex, Enums.ErrorLevel.Informational);
                //current version not found... carry on without it?
            }

            if (CurrentVer != null)
                ApplicationKey = CurrentVer;

            //gets the name of the file that have the icon.
            string IconLocation =
                ApplicationKey.OpenSubKey("DefaultIcon").GetValue("").ToString();
            string[] IconPath = IconLocation.Split(',');


            IntPtr[] Large = null;
            IntPtr[] Small = null;
            int iIconPathNumber = 0;

            if (IconPath.Length > 1)
                iIconPathNumber = 1;
            else
                iIconPathNumber = 0;


            if (IconPath[iIconPathNumber] == null) IconPath[iIconPathNumber] = "0";
            Large = new IntPtr[1];
            Small = new IntPtr[1];



            //extracts the icon from the file.
            if (iIconPathNumber > 0)
            {
                ExtractIconEx(IconPath[0],
                    Convert.ToInt16(IconPath[iIconPathNumber]), Large, Small, 1);
            }
            else
            {
                ExtractIconEx(IconPath[0],
                    Convert.ToInt16(0), Large, Small, 1);
            }


            if (LargeImage) { return Icon.FromHandle(Large[0]); } else { return Icon.FromHandle(Small[0]); }
        }
        /// <summary>
        /// Create Backup File
        /// </summary>
        /// <param name="SourcePath">File</param>
        /// <param name="Destination">Destination</param>
        /// <param name="AddDateStamp">DateStamp Added To End Of File</param>
        /// <param name="Encrypt">Encrypt the FileName</param>
        /// <param name="EncryptionKey">Key To Use</param>
        /// <returns>true/false</returns>
        public static bool CreateBackupFile(this string SourcePath, string Destination, bool AddDateStamp, bool Encrypt, string EncryptionKey = "")
        {
            try
            {
                if (SourcePath.FileExists() == false) { return false; }
                string _Extension = SourcePath.GetExtensionFromFileName();
                string _DestinationPath = Destination.EnsureDirectoryFormat();

                if (_DestinationPath.DirectoryExists(true) == false) { return false; }

                string _tmpFileName = SourcePath.GetFileNameWithoutExtension();
                if (AddDateStamp) { _tmpFileName = _tmpFileName + "_" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Millisecond.ToString(); }
                _tmpFileName = _tmpFileName + "actb";

                SourcePath.CopyFileTo(_DestinationPath + _tmpFileName);

                if (Encrypt) { (_DestinationPath + _tmpFileName).EncryptString(EncryptionKey); }
            }
            catch (Exception ex)
            {
                ACT.Core.Helper.ErrorLogger.LogError("String_FileIO.CreateBackupFile EXTENSION", "Unknown Error: " + SourcePath + "||" + Destination + "||" + AddDateStamp + "||" + Encrypt.ToString(), ex, Enums.ErrorLevel.Severe);
                return false;
            }

            return true;
        }

        /// <summary>
        /// The recursive copy counter
        /// </summary>
        private static int RecursiveCopyCounter = 0;

        /// <summary>
        /// The asynchronous long holder
        /// </summary>
        private static readonly Dictionary<string, long> AsyncLongHolder = new Dictionary<string, long>();

        /// <summary>
        /// The asynchronous time span holder
        /// </summary>
        private static readonly Dictionary<string, TimeSpan> AsyncTimeSpanHolder = new Dictionary<string, TimeSpan>();

        /// <summary>
        /// The asynchronous cleanup i ds
        /// </summary>
        private static readonly List<string> AsyncCleanupIDs = new List<string>();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A string extension method that files are equal hash. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/27/2019. </remarks>
        ///
        /// <param name="first">    The first to act on. </param>
        /// <param name="second">   The second. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool FilesAreEqual_Hash(this string first, string second)
        {
            byte[] firstHash = System.Security.Cryptography.SHA256.Create().ComputeHash(first.ReadAll());
            byte[] secondHash = System.Security.Cryptography.SHA256.Create().ComputeHash(second.ReadAll());

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Reads all the text from the File Path
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static string ReadAllText(this string FilePath)
        {
            if (FilePath.FileExists() == false)
            {
                ACT.Core.Helper.ErrorLogger.LogError("string ReadAllText String Extension", "File Not Found", FilePath, null, Enums.ErrorLevel.Warning);
                return null;
            }

            return System.IO.File.ReadAllText(FilePath);
        }

        /// <summary>
        /// Reads all the text from the File Path
        /// </summary>
        /// <param name="FilePath">The file path.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ReadAll(this string FilePath)
        {
            if (FilePath.FileExists() == false)
            {
                ACT.Core.Helper.ErrorLogger.LogError("byte[] ReadAll String Extension", "File Not Found", FilePath, null, Enums.ErrorLevel.Warning);
                return null;
            }

            return System.IO.File.ReadAllBytes(FilePath);
        }

        /// <summary>
        /// Tries to copy a file from one location to another using a FileShare.ReadWrite operation.
        /// On error the length is 0 and the TimeSpan is set to MAX.  Errors are logged when VerboseDebugging = true;
        /// </summary>
        /// <param name="SourceFilePath">Source File Path</param>
        /// <param name="DestinationFilePath">Destination File Path</param>
        /// <param name="progress">Progress Info</param>
        /// <returns>(long, TimeSpan) or (TotalBytesWritten,Time To Do It)</returns>
        public static System.Threading.Tasks.Task CopyFile_Passivly(this string SourceFilePath, string DestinationFilePath, IProgress<(long, TimeSpan)> progress)
        {
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(
                () =>
                {
                    var _Start = DateTime.Now;
                    try
                    {
                        using (BinaryReader streamReader = new BinaryReader(new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                        {
                            using (BinaryWriter streamWriter = new BinaryWriter(new FileStream(DestinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None)))
                            {
                                byte[] buffer = new byte[2048];
                                int bytesRead;
                                while ((bytesRead = streamReader.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    streamWriter.Write(buffer);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (SystemSettings.GetSettingByName("VerboseDebugging").Value.ToBool())
                        {
                            ACT.Core.Helper.ErrorLogger.LogError("ACT String Extensions: CopyFile_Passivly", SourceFilePath + ";" + DestinationFilePath, ex, Enums.ErrorLevel.Severe);
                        }
                    }
                }
            );

            return t;
        }

        /// <summary>
        /// Copies the file to.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="DestinationFilePath">The destination file path.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CopyFileTo(this string FileName, string DestinationFilePath, bool OverWrite = true)
        {
            try
            {
                System.IO.File.Copy(FileName, DestinationFilePath, OverWrite);
            }
            catch
            {
                Helper.ErrorLogger.LogError("String Extension CopyFile", "Error Copying File", FileName + "---" + DestinationFilePath, null, Enums.ErrorLevel.Severe);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculate the Directory Size Using a Parallel Loop
        /// </summary>
        /// <param name="SourcePath">Directory Path To Calculate</param>
        /// <returns>(int Count, long TotalSize)</returns>
        public static (int, long) CalculateDirectorySize(this string SourcePath)
        {
            DirectoryInfo _DI = new DirectoryInfo(SourcePath);
            var _AllFileInfo = _DI.GetFiles("*", SearchOption.AllDirectories);
            long _tmpTotal = 0;

            System.Threading.Tasks.Parallel.ForEach(_AllFileInfo, (currentFile) =>
            {
                System.Threading.Interlocked.Add(ref _tmpTotal, currentFile.Length);
            });

            return (_AllFileInfo.Count(), _tmpTotal);
        }

        /// <summary>
        /// Calculate the Directory Size Using a Parallel Loop
        /// </summary>
        /// <param name="SourcePath">Directory Path To Calculate</param>
        /// <returns>(int Count, long TotalSize, int TotalDirectoryCount)</returns>    
        public static (int, long, int) CalculateDirectorySizeExtra(this string SourcePath)
        {
            DirectoryInfo _DI = new DirectoryInfo(SourcePath);
            var _AllFileInfo = _DI.GetFiles("*", SearchOption.AllDirectories);
            long _tmpTotal = 0;
            var _AllDirectories = _DI.GetDirectories("*", SearchOption.AllDirectories);

            System.Threading.Tasks.Parallel.ForEach(_AllFileInfo, (currentFile) =>
            {
                System.Threading.Interlocked.Add(ref _tmpTotal, currentFile.Length);
            });

            return (_AllFileInfo.Count(), _tmpTotal, _AllDirectories.Count());
        }

        /// <summary>
        /// Saves all text.
        /// </summary>
        /// <param name="FileData">The file data.</param>
        /// <param name="FilePath">The file path.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SaveAllText(this string FileData, string FilePath)
        {
            try
            {
                System.IO.File.WriteAllText(FilePath, FileData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Copies the files recursively.
        /// </summary>
        /// <param name="SourceDir">The source dir.</param>
        /// <param name="DestinationDir">The destination dir.</param>
        /// <param name="Extensions">The extensions.</param>
        /// <param name="CopyOptions">The copy options.</param>
        /// <returns>System.Int32.</returns>
        public static int CopyFilesRecursively(this string SourceDir, string DestinationDir, List<string> Extensions, Enums.IO.FolderFileCopyOptions CopyOptions)
        {
            RecursiveCopyCounter = 0;

            /// Adjust and Ensure the Source Directory
            SourceDir = SourceDir.EnsureDirectoryFormat();

            // Check Source Directory
            if (SourceDir.DirectoryExists() == false)
            {
                ACT.Core.Helper.ErrorLogger.VLogError("ACT-String Extension-CopyFilesRecursively", "Source Directory Doesnt Exist: " + SourceDir, null, Enums.ErrorLevel.Severe);
                return -1;
            }

            // Clean Destination If Set
            try
            {
                if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.CleanDestination)) { DestinationDir.DeleteAllFilesFromDirectory(true); }
            }
            catch
            {
                ACT.Core.Helper.ErrorLogger.VLogError("ACT-String Extension-CopyFilesRecursively", "Error Cleaning Destination Directory: " + DestinationDir, null, Enums.ErrorLevel.Severe);

                Console.WriteLine("ERROR CLEANING DESTINATION!!!");
                return -2;
            }

            #region Setup Local Variables
            bool _over = false;
            bool _verbose = false;
            if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.VerboseDebugging)) { _verbose = true; }
            if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.Overwrite)) { _over = true; }

            System.IO.DirectoryInfo source = new DirectoryInfo(SourceDir);
            System.IO.DirectoryInfo destination = new DirectoryInfo(DestinationDir);

            // All Files With the Version (Key = Full File Path, Value = Version)
            Dictionary<string, string> FilesWithVersion = new Dictionary<string, string>();

            // The Final List of Files (Key = File Full Path, Value = Destination File Path )
            Dictionary<string, string> _FinalList = new Dictionary<string, string>();

            #endregion

            // Capture All Files and Add Make Sure Its Distinct - Calculates Files With Version
            // Populates FilesWithVersion
            foreach (string file in SourceDir.GetAllFilesFromPath(true).Distinct())
            {
                var _versionInfo = FileVersionInfo.GetVersionInfo(file);
                int _fileVersion = 0;
                int _productVersion = 0;
                int _version = 0;

                try
                {
                    // Get the version info to an integer format
                    _fileVersion = _versionInfo.FileVersion.Replace(".", "").ToInt(0);
                    _productVersion = _versionInfo.ProductVersion.Replace(".", "").Replace("-", "").ToInt(0);
                    // Prefer Product Version Over File Version
                    if (_version < 1) { if (_fileVersion > _productVersion) { _version = _fileVersion; } }
                }
                catch
                {
                    ACT.Core.Helper.ErrorLogger.VLogError("ACT.Core.Extensions.String_FileIO", "Error Grabbing File Version", null, Enums.ErrorLevel.Informational);
                    _version = 0;
                }

                // Add the File to the FilesWithVersion Dictionary if it doesnt exist
                if (Extensions == null || Extensions.Count() == 0) { FilesWithVersion.Add(file, _version.ToString()); }
                else
                {
                    // Check the Extension of the filename
                    if (Extensions.Contains(file.GetExtensionFromFileName().ToLower(), true))
                    {
                        if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.Keeponlythehighestversion))
                        {
                            bool _skip = false;
                            var _files = FilesWithVersion.Keys.Where(x => x.Contains(file.GetFileName(true)));
                            if (_files != null)
                            {
                                if (_files.Count() > 0)
                                {
                                    foreach (string fk in _files)
                                    {
                                        if (FilesWithVersion[fk].ToInt() > _version) { _skip = true; }
                                    }
                                }
                            }

                            if (_skip == true) { continue; }

                            FilesWithVersion.Add(file, _version.ToString());
                        }
                        else
                        {
                            FilesWithVersion.Add(file, _version.ToString());
                        }
                    }
                }
            }

            #region Create The Final List

            foreach (var key in FilesWithVersion.Keys)
            {
                string _filename = key.GetFileName(true);
                string _destination = DestinationDir.EnsureDirectoryFormat();
                string _SubFolder = key.Replace(SourceDir.EnsureDirectoryFormat(), "").Replace("\\" + _filename, "");

                // Add Version To Name IF Option Is Set
                if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.Addversiontoname))
                {
                    int _idex = _filename.LastIndexOf(".");
                    _filename = _filename.Substring(0, _idex) + FilesWithVersion[key].ToString() + "." + _filename.Substring(_idex + 1);
                }

                if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.Version_CreateFolders))
                {
                    _filename = "\\" + _filename.Replace(".", "_").Replace(" ", "_") + "_v" + FilesWithVersion[key].ToString() + "\\" + _filename;
                }

                if (CopyOptions.HasFlag(Enums.IO.FolderFileCopyOptions.Placeinroot) == false)
                {
                    _destination += _SubFolder.EnsureDirectoryFormat();
                }

                _FinalList.Add(key, _destination + _filename);
            }

            #endregion

            foreach (string key in _FinalList.Keys)
            {
                string val = _FinalList[key];
                string _folder = val.GetDirectoryFromFileLocation().EnsureDirectoryFormat();
                if (_folder.DirectoryExists(true) == false)
                {
                    ACT.Core.Helper.ErrorLogger.VLogError("ACT.Core.Extensions.String_FileIO", "Error Creating Destination Folder: " + _folder, null, Enums.ErrorLevel.Informational);
                    if (_verbose)
                    {
                        Console.WriteLine("Error Creating Folder: " + _folder);
                    }
                }
                else
                {
                    key.CopyFileTo(val, _over);
                    if (_verbose)
                    {
                        Console.WriteLine("File Copied To: " + val);
                    }
                    RecursiveCopyCounter++;
                }
            }

            // TODO IMPLEMENT NOT OVERWRITE 

            return RecursiveCopyCounter;
        }

        /// <summary>
        /// Copys All The Files From The Source To The Destination
        /// </summary>
        /// <param name="SourceDir">Source Directory</param>
        /// <param name="DestinationDir">Destination Directory</param>
        /// <param name="Extensions">Extensions LOWER CASE</param>
        /// <returns>Total Files Copied</returns>
        /// <summary>
        /// Copies the files recursively.
        /// </summary>
        /// <param name="SourceDir">The source dir.</param>
        /// <param name="DestinationDir">The destination dir.</param>
        /// <param name="Extensions">The extensions.</param>
        /// <returns>System.Int32.</returns>
        public static int CopyFilesRecursively(this string SourceDir, string DestinationDir, List<string> Extensions)
        {
            RecursiveCopyCounter = 0;
            System.IO.DirectoryInfo source = new DirectoryInfo(SourceDir);
            System.IO.DirectoryInfo destination = new DirectoryInfo(DestinationDir);

            foreach (System.IO.DirectoryInfo dir in source.GetDirectories())
            {
                CopyFilesRecursively(dir, destination.CreateSubdirectory(dir.Name));
            }

            foreach (System.IO.FileInfo file in source.GetFiles())
            {
                if (Extensions.Contains(file.Extension.ToLower()))
                {
                    file.CopyTo(System.IO.Path.Combine(destination.FullName, file.Name), true);
                    RecursiveCopyCounter++;
                }
            }

            return RecursiveCopyCounter;
        }

        /// <summary>
        /// Copy Files Recursive using AWAIT
        /// </summary>
        /// <param name="SourceDir"></param>
        /// <param name="DestinationDir"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        /// <summary>
        /// Copies the files recursively.
        /// </summary>
        /// <param name="SourceDir">The source dir.</param>
        /// <param name="DestinationDir">The destination dir.</param>
        /// <param name="progress">The progress.</param>
        /// <returns>System.Int64.</returns>
        public static long CopyFilesRecursively(this string SourceDir, string DestinationDir, IProgress<int> progress)
        {
            long _totalBytes = 0;

            System.IO.DirectoryInfo _source = new DirectoryInfo(SourceDir);

            foreach (System.IO.DirectoryInfo dir in _source.GetDirectories())
            {
                string _newDest = (DestinationDir + dir.Name + "\\");
                if (_newDest.DirectoryExists(true) == false) { }
                else
                {
                    CopyFilesRecursively(dir.FullName.EnsureDirectoryFormat(), _newDest, progress);
                }
            }

            System.Threading.Tasks.Task.Run(() =>
            {
                foreach (System.IO.FileInfo file in _source.GetFiles())
                {
                    string _newDest = (DestinationDir.EnsureDirectoryFormat() + file.Name);
                    file.CopyTo(_newDest, true);
                    _totalBytes += file.Length;
                    progress.Report(file.Length.ToInt(-1));
                }
            });

            return _totalBytes;
        }


        /// <summary>
        /// Copys All The Files From The Source To The Destination
        /// </summary>
        /// <param name="SourceDir">Source Directory</param>
        /// <param name="DestinationDir">Destination Directory</param>
        /// <returns>Total Files Copied</returns>
        public static int CopyFilesRecursively(this string SourceDir, string DestinationDir)
        {
            RecursiveCopyCounter = 0;
            System.IO.DirectoryInfo _source = new DirectoryInfo(SourceDir);
            System.IO.DirectoryInfo _destination = new DirectoryInfo(DestinationDir);

            CopyFilesRecursively(_source, _destination);

            return RecursiveCopyCounter;
        }

        /// <summary>
        /// Eventually Move This To ACT Foo
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        private static void CopyFilesRecursively(System.IO.DirectoryInfo source, System.IO.DirectoryInfo target)
        {
            foreach (System.IO.DirectoryInfo dir in source.GetDirectories())
            {
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (System.IO.FileInfo file in source.GetFiles())
            {
                file.CopyTo(System.IO.Path.Combine(target.FullName, file.Name), true);
                RecursiveCopyCounter++;
            }
        }

        /// <summary>
        /// Attempts To Delete A File.  Waits For It To Complete.  Throws Error On Lock or Other Issue.
        /// </summary>
        /// <param name="FileToDelete">Full File Path To Delete.</param>
        /// <param name="MaxWaitTime">(Optional) the maximum wait time.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        /// <remarks>Mark Alicz, 12/17/2016.</remarks>
        public static bool DeleteFile(this string FileToDelete, int MaxWaitTime = 2000)
        {
            int _WaitTime = 0;
            var fi = new System.IO.FileInfo(FileToDelete);
            if (fi.Exists)
            {
                fi.Delete();
                fi.Refresh();
                while (fi.Exists)
                {
                    System.Threading.Thread.Sleep(100);
                    fi.Refresh();
                    _WaitTime += 100;
                    if (_WaitTime > MaxWaitTime)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Delete All Files From Directory Optionally Recursive
        /// </summary>
        /// <param name="Directory">The directory.</param>
        /// <param name="Recursive">if set to <c>true</c> [recursive].</param>
        /// <returns>Dictionary&lt;System.String, System.Boolean&gt;.</returns>
        public static Dictionary<string, bool> DeleteAllFilesFromDirectory(this string Directory, bool Recursive = false)
        {
            Dictionary<string, bool> _TmpReturn = new Dictionary<string, bool>();

            if (Recursive)
            {
                var _AllFiles = Directory.GetAllFilesFromPath(true);
                foreach (string file in _AllFiles) { _TmpReturn.Add(file, file.DeleteFile(2000)); }
            }
            else
            {
                var _AllFiles = Directory.GetAllFilesFromPath(false);
                foreach (string file in _AllFiles) { _TmpReturn.Add(file, file.DeleteFile(2000)); }
            }
            return _TmpReturn;
        }

        /// <summary>
        /// Internal get All Files From Path
        /// </summary>
        /// <param name="BaseDirectory">The base directory.</param>
        /// <param name="data">The data.</param>
        /// <param name="SearchPattern">The Search Pattern </param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private static List<string> _getallfilesfrompath(string BaseDirectory, List<string> data, string SearchPattern = "")
        {
            if (System.IO.Directory.Exists(BaseDirectory.EnsureDirectoryFormat()))
            {
                string[] _Files;

                if (SearchPattern == "") { _Files = System.IO.Directory.GetFiles(BaseDirectory); }
                else { _Files = System.IO.Directory.GetFiles(BaseDirectory).Where(x => x.Contains(SearchPattern)).ToArray(); }

                data.AddRange(_Files);

                foreach (string p in System.IO.Directory.GetDirectories(BaseDirectory))
                {
                    data = _getallfilesfrompath(p, data, SearchPattern);
                }
                return data;
            }
            else
            {
                return data;
            }
        }

        /// <summary>
        /// Get All Files From Base Directory
        /// </summary>
        /// <param name="BaseDirectory">Base Directory</param>
        /// <param name="Recursive">Get Files In Sub Folders</param>
        /// <returns>List{string} All Files Found</returns>
        public static List<string> GetAllFilesFromPath(this string BaseDirectory, bool Recursive)
        {
            if (Recursive)
            {
                return _getallfilesfrompath(BaseDirectory, new List<string>());
            }
            else
            {
                return System.IO.Directory.GetFiles(BaseDirectory).ToList();
            }
        }

        /// <summary>
        /// Get All Files From Base Directory
        /// </summary>
        /// <param name="BaseDirectory">Base Directory</param>
        /// <param name="Recursive">Get Files In Sub Folders</param>
        /// <param name="FileNamePattern">Parameter of the FileName</param>
        /// <returns>List{string} All Files Found</returns>
        public static List<string> GetAllFilesFromPath(this string BaseDirectory, bool Recursive, string FileNamePattern)
        {
            if (Recursive)
            {
                return System.IO.Directory.GetFiles(BaseDirectory, FileNamePattern, SearchOption.AllDirectories).ToList();
            }
            else
            {
                return System.IO.Directory.GetFiles(BaseDirectory).ToList();
            }
        }

        /// <summary>
        /// Returns the SubFolders Under the Base Directory
        /// </summary>
        /// <param name="BaseDirectory">Root Directory</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetAllSubFolders(this string BaseDirectory)
        {
            return System.IO.Directory.GetDirectories(BaseDirectory).ToList();
        }

        /// <summary>
        /// Returns the SubFolders Under the Base Directory
        /// </summary>
        /// <param name="BaseDirectory">Root Directory</param>
        /// <param name="Recursive">Recursive</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetAllSubFolders(this string BaseDirectory, bool Recursive)
        {
            if (Recursive)
            {
                return System.IO.Directory.GetDirectories(BaseDirectory, "*", SearchOption.AllDirectories).ToList();
            }
            else
            {
                return System.IO.Directory.GetDirectories(BaseDirectory).ToList();
            }
        }

        /// <summary>
        /// Attempts To Delete A File.  Waits For It To Complete.  Throws Error On Lock or Other Issue.
        /// </summary>
        /// <param name="FileToDelete">Full File Path To Delete.</param>
        /// <param name="MaxTries">The maximum tries.</param>
        /// <param name="throwError">if set to <c>true</c> [throw error].</param>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <remarks>Mark Alicz, 12/17/2016.</remarks>
        public static void DeleteFile(this string FileToDelete, short MaxTries = 50, bool throwError = false)
        {
            int _TotalTries = 0;

            var fi = new System.IO.FileInfo(FileToDelete);
            if (fi.Exists)
            {
                fi.Delete();
                fi.Refresh();
                while (fi.Exists)
                {
                    System.Threading.Thread.Sleep(100);
                    fi.Refresh();
                    _TotalTries++;
                    if (_TotalTries > MaxTries) { throw new Exception("Error Deleting File"); }
                }
            }
        }

        /// <summary>
        /// File Size
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns>-1 File Doesnt Exist, -2 Other General Error</returns>
        public static long GetFileSize(this string FileName)
        {
            try
            {
                if (FileName.FileExists() == false) { return 0; }
                System.IO.FileInfo _FI = new System.IO.FileInfo(FileName);
                return _FI.Length;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Search For a File and Return the Data as a byte[]
        /// </summary>
        /// <param name="SearchPath"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static byte[] FindAndReadFile(this string SearchPath, string FileName)
        {
            string _foundPath = SearchPath.EnsureDirectoryFormat().FindFileReturnPath(FileName, true);

            if (_foundPath.NullOrEmpty()) { return null; }

            _foundPath = _foundPath + FileName;
            return _foundPath.ReadAll();
        }

        /// <summary>
        /// Search For A File And Return the String Contents 
        /// </summary>
        /// <param name="SearchPath"></param>
        /// <param name="FileName"></param>
        /// <param name="ConvertBinaryToBase64">Convert Binary files To Base64Strings</param>
        /// <returns></returns>
        public static string FindAndReadFile(this string SearchPath, string FileName, bool ConvertBinaryToBase64 = true)
        {
            string _foundPath = SearchPath.EnsureDirectoryFormat().FindFileReturnPath(FileName, true);
            if (_foundPath.NullOrEmpty()) { return null; }
            _foundPath = _foundPath + FileName;
            
            if (ACT.Core.Constants.Constants_FILETYPES.TXTFileExtensions.Contains(FileName.GetExtensionFromFileName().ToLower()))
            {
                return _foundPath.ReadAllText();
            }
            else
            {
                if (ConvertBinaryToBase64) { return _foundPath.ReadAll().ToBase64String(); }
                else { return null; }
            }
        }

        /// <summary>
        /// FindFile and return the Path Optionally Searches Sub Folder
        /// </summary>
        /// <param name="path">Path to Start In</param>
        /// <param name="fileName">Name Of The File</param>
        /// <param name="searchSubFolders">Search Sub Folders Or Not</param>
        /// <returns>System.String.</returns>
        public static string FindFileReturnPath(this string path, string fileName, bool searchSubFolders = true)
        {
            if (System.IO.Directory.Exists(path) == false) { return ""; }

            if (System.IO.Directory.EnumerateFiles(path).Any(x => x.GetFileNameFromFullPath().ToLower() == fileName.ToLower()))
            {
                return path.EnsureDirectoryFormat();
            }
            else if (searchSubFolders)
            {
                foreach (string d in System.IO.Directory.GetDirectories(path))
                {
                    var _P = FindFileReturnPath(d, fileName, searchSubFolders);
                    if (_P != "") { return _P.EnsureDirectoryFormat(); }
                }

                return "";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Searches for all references of the specified file
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="searchSubFolders">if set to <c>true</c> [search sub folders].</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> FindAllFileReferencesInPath(this string path, string filename, bool searchSubFolders = true)
        {
            var _Files = path.GetAllFilesFromPath(searchSubFolders);
            return _Files.Where(x => x.ToLower().GetFileNameFromFullPath() == filename.ToLower()).ToList();
        }

        /// <summary>
        /// Get a Memory Stream From a File Path
        /// </summary>
        /// <param name="Path">The path.</param>
        /// <returns>MemoryStream.</returns>
        public static MemoryStream GetMemoryStream(this string Path)
        {
            using (System.IO.FileStream _FS = new FileStream(Path, FileMode.Open))
            {
                System.IO.MemoryStream _MS = new MemoryStream();
                _FS.CopyTo(_MS);
                return _MS;
            }
        }
    }
}
