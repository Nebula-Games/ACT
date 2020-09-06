// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="BaseFile.cs" company="Stonegate Intel LLC">
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
    /*
     *  FILE VERSION INFO
     *  HEADERS
     * 
     * 
     */

    /// <summary>
    /// Class PackedFile.
    /// </summary>
    public class PackedFile
    {

        /// <summary>
        /// The file headers
        /// </summary>
        public List<PackedFileHeader> FileHeaders = new List<PackedFileHeader>();

        /// <summary>
        /// Adds the file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        public void AddFile(string FileName)
        {

        }


    }



}
