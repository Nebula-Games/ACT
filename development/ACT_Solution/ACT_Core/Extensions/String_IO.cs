// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="String_IO.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Security;
using ACT.Core.Types;
using ACT.Core.Helper;
using Microsoft.Win32.SafeHandles;


#endregion


namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class String_IO.
    /// </summary>
    public static class String_IO
    {
        /// <summary>
        /// Converts a String to a MemoryStream.
        /// </summary>
        /// <param name="str">
        /// </param>
        /// <returns>
        /// </returns>
        /// <summary>
        /// Converts to stream.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>Stream.</returns>
        [NotNull]
        public static Stream ToStream([NotNull] this string str)
        {
          //  CodeContracts.VerifyNotNull(str, "str");

            byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        /// <summary>
        /// Determines whether [is image name] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>Returns if the String is a Image Name</returns>
        /// <summary>
        /// Determines whether [is image name] [the specified input string].
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns><c>true</c> if [is image name] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static bool IsImageName(this string inputString)
        {            
            return inputString.EndsWith("png", StringComparison.InvariantCultureIgnoreCase)
                   || inputString.EndsWith("gif", StringComparison.InvariantCultureIgnoreCase)
                   || inputString.EndsWith("jpeg", StringComparison.InvariantCultureIgnoreCase)
                   || inputString.EndsWith("jpg", StringComparison.InvariantCultureIgnoreCase)
                   || inputString.EndsWith("bmp", StringComparison.InvariantCultureIgnoreCase)
                   || inputString.EndsWith("tiff", StringComparison.InvariantCultureIgnoreCase)
                   || inputString.EndsWith("tif", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Gets A File Name From A Full Path Name
        /// </summary>
        /// <param name="x">full Path</param>
        /// <param name="includeExtension">Include Or Exclude The Extension</param>
        /// <returns>string - File Name</returns>
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="includeExtension">if set to <c>true</c> [include extension].</param>
        /// <returns>System.String.</returns>
        public static string GetFileName(this string x, bool includeExtension)
        {
            if (includeExtension == true) { return x.GetFileNameFromFullPath(); }
            else { return x.GetFileNameWithoutExtension(); }
        }

        /// <summary>
        /// Ensures the FileName passed in is Valid
        /// </summary>
        /// <param name="x">Either A Full Path To The FileName or The FileName ItSelf</param>
        /// <param name="ReplaceMentCharacter">Replacement Character to Replace</param>
        /// <returns></returns>
        /// <summary>
        /// Ensures the name of the valid windows file.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="ReplaceMentCharacter">The replace ment character.</param>
        /// <returns>System.String.</returns>
        public static string EnsureValidWindowsFileName(this string x, string ReplaceMentCharacter = "")
        {
            string _FileName = x.GetFileName(true);
            string _Directory = x.GetDirectoryFromFileLocation();

            foreach (char c in Path.GetInvalidFileNameChars())
            {
                _FileName = _FileName.Replace(c.ToString(), ReplaceMentCharacter);
            }

            if (_Directory.Length < 2)
            {
                return _FileName;
            }
            else
            {
                return _Directory + _FileName;
            }
        }

        /// <summary>
        /// Checks to see if the File Exists
        /// </summary>
        /// <param name="x">Full Path Name To File.  i.e. c:\test\test.txt</param>
        /// <returns>true / false</returns>
        public static bool FileExists(this string x)
        {
            if (System.IO.File.Exists(x)) { return true; }
            else { return false; }
        }

        /// <summary>
        /// Checks If The File Exists.
        /// </summary>
        /// <param name="DirectoryPath">Directory To The File</param>
        /// <param name="FileName">Actual File Name</param>
        /// <returns>true / false</returns>
        public static bool FileExists(this string DirectoryPath, string FileName) { return (DirectoryPath.EnsureDirectoryFormat() + FileName).FileExists(); }

        /// <summary>
        /// Moves Up a Directory [Count] Directories.
        /// </summary>
        /// <param name="StartingDirectoryPath">Starting Directory</param>
        /// <param name="Count">Number Of Parents Up To Navigate To</param>
        /// <param name="Validate">Validate The Information - throwing and error</param>
        /// <returns>New Directory Path</returns>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        public static string NavigateUpDirectory(this string StartingDirectoryPath, int Count, bool Validate)
        {

            if (Validate)
            {
                if (StartingDirectoryPath.DirectoryExists() == false)
                {
                    throw new System.IO.DirectoryNotFoundException(StartingDirectoryPath);
                }
            }

            string _TmpReturn = StartingDirectoryPath.EnsureDirectoryFormat();

            for (int pos = 0; pos <= Count; pos++)
            {
                _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.LastIndexOf("\\"));
            }

            return _TmpReturn.EnsureDirectoryFormat();
        }

        /// <summary>
        /// Checks To See if the Directory Exists,  Optional Parameter To Create If It Doesnt
        /// </summary>
        /// <param name="DirectoryPath">Directory Path</param>
        /// <param name="ForceCreate">Create The Directory when It is not found</param>
        /// <returns>True On Success, False On Failure</returns>
        public static bool DirectoryExists(this string DirectoryPath, bool ForceCreate)
        {
            if (DirectoryExists(DirectoryPath)) { return true; }
            else
            {
                if (ForceCreate)
                {
                    try
                    {
                        CreateDirectoryStructure(DirectoryPath.EnsureDirectoryFormat());
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks If The Directory Exists
        /// </summary>
        /// <param name="x">Directory Path</param>
        /// <returns>true / false</returns>
        public static bool DirectoryExists(this string x)
        {
            if (x.Contains("\\") == false) { x = x.EnsureDirectoryFormat(); }

            x = x.EnsureDirectoryFormat();
            if (System.IO.Directory.Exists(x))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create The Directory Structure Specified In The Parameter
        /// </summary>
        /// <param name="x">Directory Structure To Create</param>
        /// <param name="d">The d.</param>
        /// <exception cref="Exception">
        /// Invalid Path Contains No Directory Format.
        /// or
        /// Invalid Path Info
        /// or
        /// Root Path Does Not Exist: (" + DirectoryPathInfo[0] + ")
        /// or
        /// Error Creating Directory. Please Check Log
        /// or
        /// Error Creating Directory With Security. Please Check Log
        /// </exception>
        public static void CreateDirectoryStructure(this string x, System.Security.AccessControl.DirectorySecurity d = null)
        {
            if (x.Contains("\\") == false) { throw new Exception("Invalid Path Contains No Directory Format."); }

            string[] DirectoryPathInfo = x.SplitString("\\", StringSplitOptions.RemoveEmptyEntries);

            if (DirectoryPathInfo.Length == 0) { throw new Exception("Invalid Path Info"); }

            if (DirectoryPathInfo[0].DirectoryExists() == false) { throw new Exception("Root Path Does Not Exist: (" + DirectoryPathInfo[0] + ")"); }

            string _TotalPath = DirectoryPathInfo[0];

            for (int pos = 1; pos < DirectoryPathInfo.Length; pos++)
            {
                _TotalPath = _TotalPath + "\\" + DirectoryPathInfo[pos];

                if (_TotalPath.DirectoryExists() == false)
                {
                    try
                    {
                        if (d == null)
                        {
                            try
                            {
                                System.IO.Directory.CreateDirectory(_TotalPath.EnsureDirectoryFormat());
                            }
                            catch (Exception ex)
                            {
                                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Extensions.CreateDirectoryStructure", "Error Creating Directory Without Security.", x, ex, Enums.ErrorLevel.Critical);
                                throw new Exception("Error Creating Directory. Please Check Log");
                            }
                        }
                        else
                        {
                            try
                            {
#if DOTNETFRAMEWORK
                                System.IO.Directory.CreateDirectory(_TotalPath.EnsureDirectoryFormat(), d);
#elif DOTNETCORE
                                System.IO.Directory.CreateDirectory(_TotalPath.EnsureDirectoryFormat());
#endif
                            }
                            catch (Exception ex)
                            {
                                ACT.Core.Helper.ErrorLogger.LogError("ACT.Core.Extensions.CreateDirectoryStructure", "Error Creating Directory WITH Security.", x + d.ToString(), ex, Enums.ErrorLevel.Critical);
                                throw new Exception("Error Creating Directory With Security. Please Check Log");
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// Ensures The String Ends With \.  i.e. "c:\test\testdir" -&gt; "c:\test\testdir\"
        /// </summary>
        /// <param name="x">Directory Path</param>
        /// <param name="WindowsFormat">if set to <c>true</c> [windows format].</param>
        /// <returns>Formatted Directory Path</returns>
        public static string EnsureDirectoryFormat(this string x, bool WindowsFormat = true)
        {
            if (WindowsFormat)
            {
                if (x.EndsWith("\\") || x.EndsWith("/")) { return x.Replace("/", "\\"); }
                else { return x.Replace("/", "\\") + "\\"; }
            }
            else
            {
                if (x.EndsWith("\\") || x.EndsWith("/")) { return x.Replace("/", "\\"); }
                else { return x.Replace("/", "\\") + "\\"; }
            }
        }

        /// <summary>
        /// Returns the Directory Path From The Full FileName Path
        /// </summary>
        /// <param name="x">Directory With File Name</param>
        /// <returns>Directory Part Of The Full Path</returns>
        public static string GetDirectoryFromFileLocation(this string x)
        {
            string _TmpReturn = x;

            if (_TmpReturn.Contains("\\") == false) { return ""; }

            if (_TmpReturn.Contains("//"))
            {
                try
                {
                    _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.LastIndexOf("//")).EnsureDirectoryFormat();
                }
                catch { }
            }

            if (_TmpReturn.Contains("\\"))
            {
                try
                {
                    _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.LastIndexOf("\\")).EnsureDirectoryFormat();
                }
                catch { }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Gets the Last Directory Name From A Valid Directory Path
        /// </summary>
        /// <param name="x">Valid Directory Name</param>
        /// <returns>System.String.</returns>
        public static string GetDirectoryName(this string x)
        {
            string _TmpReturn = x;

            if (x.Contains("\\") == false) { return _TmpReturn; }

            if (x.Contains("//"))
            {
                _TmpReturn = _TmpReturn.TrimEnd("//");
                _TmpReturn = _TmpReturn.Substring(_TmpReturn.LastIndexOf("//")).Trim("//").Trim();
            }

            if (_TmpReturn.Contains("\\"))
            {
                _TmpReturn = _TmpReturn.TrimEnd("\\");
                _TmpReturn = _TmpReturn.Substring(_TmpReturn.LastIndexOf("\\")).Trim("\\").Trim();
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Depreciated Use EnsureDirectoryFormat()
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        [Obsolete("Please Use Extension Method EnsureDirectoryFormat()")]
        public static string FormatDirectory(this string x)
        {
            return x.EnsureDirectoryFormat();
        }

        /// <summary>
        /// Returns the File Name Without The Extension From a Full File Path
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>File Name - With No Extensions</returns>
        public static string GetFileNameWithoutExtension(this string x)
        {
            string _TmpReturn = x;

            if (_TmpReturn.Contains(".") == false) { return _TmpReturn; }
            else
            {
                _TmpReturn = _TmpReturn.GetFileNameFromFullPath();
                _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.LastIndexOf("."));
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Returns the File Name From The Full File Path (Includes Network Paths)
        /// </summary>
        /// <param name="x">Full File Path</param>
        /// <returns>FileName With Extension</returns>
        public static string GetFileNameFromFullPath(this string x)
        {
            string _TmpReturn = x;

            if (_TmpReturn.Contains("\\") == false) { return _TmpReturn; }

            else if (_TmpReturn.Contains("\\"))
            {
                _TmpReturn = _TmpReturn.EnsureDirectoryFormat().Substring(_TmpReturn.LastIndexOf("\\") + 1).TrimEnd("\\");
            }
            else if (_TmpReturn.Contains("//"))
            {
                _TmpReturn = _TmpReturn.EnsureDirectoryFormat().Substring(_TmpReturn.LastIndexOf("//") + 1).TrimEnd("//");
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Returns the Extension From The FileName
        /// </summary>
        /// <param name="x">FileName</param>
        /// <returns>File Extension - Without the Period</returns>
        public static string GetExtensionFromFileName(this string x)
        {
            string _TmpReturn = x;

            if (_TmpReturn.Contains(".") == false) { return _TmpReturn; }
            else
            {
                try
                {
                    string[] _Parts = _TmpReturn.SplitString(".", StringSplitOptions.RemoveEmptyEntries);
                    _TmpReturn = _Parts.Last();
                }
                catch { }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string? GetExecutingAssemblyPathDirectory(this string x)
        {
            string? _tmpReturn = "";
            var _GEA = System.Reflection.Assembly.GetEntryAssembly();
            if (_GEA != null)
            {
                _tmpReturn = System.IO.Path.GetDirectoryName(_GEA.Location);
            }
            else
            {
                return x;
            }

            return _tmpReturn;
        }

    }
}
