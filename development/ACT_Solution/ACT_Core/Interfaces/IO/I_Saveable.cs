// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Saveable.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.IO
{
    /// <summary>
    /// Outlines methods that allow directory saving
    /// </summary>
    public interface I_Saveable
    {
        /// <summary>
        /// Save using Internal Settings
        /// </summary>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult Save();

        /// <summary>
        /// Save Using Specified Location / FileName
        /// </summary>
        /// <param name="Location">The location.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <param name="CreateBackup">if set to <c>true</c> [create backup].</param>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult Save(string Location, string FileName = "", bool OverWrite = false, bool CreateBackup = false);

        /// <summary>
        /// Save to Multiple Locations
        /// </summary>
        /// <param name="Locations">The locations.</param>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="OverWrite">if set to <c>true</c> [over write].</param>
        /// <param name="CreateBackup">if set to <c>true</c> [create backup].</param>
        /// <returns>Common.I_TestResult.</returns>
        Common.I_TestResult Save(List<string> Locations, string FileName = "", bool OverWrite = false, bool CreateBackup = false);
    }
}
