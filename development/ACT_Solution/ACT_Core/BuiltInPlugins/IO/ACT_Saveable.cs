// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Saveable.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.BuiltInPlugins.IO
{
    /// <summary>
    /// Class ACT_Saveable.
    /// Implements the <see cref="ACT.Core.Interfaces.IO.I_Saveable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.IO.I_Saveable" />
    public class ACT_Saveable : ACT.Core.Interfaces.IO.I_Saveable
    {
        /// <summary>
        /// Save using Internal Settings
        /// </summary>
        /// <returns>I_TestResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Save Using Specified Location / FileName
        /// </summary>
        /// <param name="Location">The location.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <param name="CreateBackup">if set to <c>true</c> [create backup].</param>
        /// <returns>I_TestResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult Save(string Location, string FileName = "", bool OverWrite = false, bool CreateBackup = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the specified locations.
        /// </summary>
        /// <param name="Locations">The locations.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <param name="CreateBackup">if set to <c>true</c> [create backup].</param>
        /// <returns>I_TestResult.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public I_TestResult Save(List<string> Locations, string FileName = "", bool OverWrite = false, bool CreateBackup = false)
        {
            throw new NotImplementedException();
        }
    }
}
