// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_File_Information.cs" company="Stonegate Intel LLC">
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

namespace ACT.Core.Types
{
    /// <summary>
    /// Class ACT_File_Information.
    /// </summary>
    public class ACT_File_Information
    {
        /// <summary>
        /// The original file name
        /// </summary>
        public string OriginalFileName;
        /// <summary>
        /// The file size
        /// </summary>
        public int FileSize;
        /// <summary>
        /// Creates new name.
        /// </summary>
        public string NewName;
        /// <summary>
        /// The other destinations
        /// </summary>
        public List<string> OtherDestinations = new List<string>();
        /// <summary>
        /// Exports the XML.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ExportXML()
        {
            string _TmpReturn = "<file>";

            _TmpReturn += "<originalfilename>" + OriginalFileName + "</originalfilename>";
            _TmpReturn += "<filesize>" + OriginalFileName + "</filesize>";
            _TmpReturn += "<newname>" + OriginalFileName + "</newname>";

            _TmpReturn += "</file>";

            return _TmpReturn;
        }

        /// <summary>
        /// Exports all.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns>System.String.</returns>
        public static string ExportAll(List<ACT_File_Information> fileInfo)
        {
            string _tmpReturn = "<backup>" + Environment.NewLine;

            foreach (ACT_File_Information _f in fileInfo)
            {
                _tmpReturn += "\t<file>" + Environment.NewLine;
                _tmpReturn += "\t<originalfilename>" + _f.OriginalFileName + "</originalfilename>" + Environment.NewLine;
                _tmpReturn += "\t<filesize>" + _f.OriginalFileName + "</filesize>" + Environment.NewLine;
                _tmpReturn += "\t<newname>" + _f.OriginalFileName + "</newname>" + Environment.NewLine;
                _tmpReturn += "\t<otherdestinations>" + Environment.NewLine;
                foreach (var other in _f.OtherDestinations)
                {
                    _tmpReturn += "\t\t<otherdestination>" + other + "</otherdestination>" + Environment.NewLine;
                }
                _tmpReturn += "\t</otherdestinations>" + Environment.NewLine;
                _tmpReturn += "\t</file>" + Environment.NewLine;
            }

            _tmpReturn += "</backup>" + Environment.NewLine;

            return _tmpReturn;
        }

        /// <summary>
        /// Loads Data From XML
        /// </summary>
        /// <param name="XML">The XML.</param>
        /// <returns>List&lt;ACT_File_Information&gt;.</returns>
        public static List<ACT_File_Information> Import(string XML)
        {
            var _TmpReturn = new List<ACT_File_Information>();

            System.Xml.Linq.XDocument _xDocument = System.Xml.Linq.XDocument.Load(new System.IO.StringReader(XML));

            var _elements = _xDocument.Root.Elements();

            foreach (var elm in _elements)
            {
                ACT_File_Information _newFile = new ACT_File_Information();
                string _orig = elm.Element("originalfilename").Value;
                string _filesize = elm.Element("filesize").Value;
                string _newname = elm.Element("newname").Value;

                _newFile.OriginalFileName = _orig;
                _newFile.FileSize = _filesize.ToInt(-1);
                _newFile.NewName = _newname;

                try
                {
                    var _element = elm.Element("otherdestinations");
                    var _otherdest = _element.Elements("otherdestination");
                    foreach (var _oth in _otherdest)
                    {
                        _newFile.OtherDestinations.Add(_oth.Value);
                    }
                }
                catch { }

                _TmpReturn.Add(_newFile);
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Recreates the specified XML.
        /// </summary>
        /// <param name="XML">The XML.</param>
        /// <param name="BaseURL">The base URL.</param>
        /// <param name="DestinationURL">The destination URL.</param>
        /// <returns>Interfaces.Common.I_TestResult.</returns>
        public static Interfaces.Common.I_TestResult Recreate(string XML, string BaseURL, string DestinationURL)
        {
            var _I_TestResult = CurrentCore<Interfaces.Common.I_TestResult>.GetCurrent();

            try
            {
                var _AllFiles = Import(XML);
                if (_AllFiles.Count() == 0) { _I_TestResult.Success = false; _I_TestResult.Messages.Add("No Data Found In XML"); return _I_TestResult; }

                foreach(var fileInfo in _AllFiles)
                {
                    File.Copy(BaseURL.EnsureDirectoryFormat() + fileInfo.OriginalFileName.GetFileNameFromFullPath(), DestinationURL.EnsureDirectoryFormat() + fileInfo.OriginalFileName.GetFileNameFromFullPath());

                    foreach(var otherLocation in fileInfo.OtherDestinations)
                    {
                        var _OriginalPath = fileInfo.OriginalFileName.GetDirectoryFromFileLocation().EnsureDirectoryFormat();
                        var _NewPath = otherLocation.Replace(_OriginalPath, "");
                        string _NewFolder = DestinationURL.EnsureDirectoryFormat() + _NewPath;
                        File.Copy(BaseURL.EnsureDirectoryFormat() + fileInfo.OriginalFileName.GetFileNameFromFullPath(), _NewFolder.EnsureDirectoryFormat() + fileInfo.OriginalFileName.GetFileNameFromFullPath());
                    }                    
                }
            }
            catch (Exception ex)
            {
                _I_TestResult.Success = false;
                _I_TestResult.Messages.Add("An Exception Ocurred: " + ex.Message);
            }

            return _I_TestResult;
        }

    }
}
