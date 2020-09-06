// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Excel_Sheet.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.DataAccess.Excel
{
    /// <summary>
    /// Interface I_Excel_Sheet
    /// </summary>
    public interface I_Excel_Sheet
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has column headers.
        /// </summary>
        /// <value><c>true</c> if this instance has column headers; otherwise, <c>false</c>.</value>
        bool HasColumnHeaders { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [vertical alignment].
        /// </summary>
        /// <value><c>true</c> if [vertical alignment]; otherwise, <c>false</c>.</value>
        bool VerticalAlignment { get; set; }
        /// <summary>
        /// Gets or sets the row limit.
        /// </summary>
        /// <value>The row limit.</value>
        int RowLimit { get; set; }
        /// <summary>
        /// Exports this instance.
        /// </summary>
        /// <returns>System.Data.DataTable.</returns>
        System.Data.DataTable Export();

        /// <summary>
        /// Imports the specified d.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns>ACT.Core.Interfaces.Common.I_TestResult.</returns>
        ACT.Core.Interfaces.Common.I_TestResult Import(System.Data.DataTable d);

        /// <summary>
        /// Exports to json.
        /// </summary>
        /// <returns>System.String.</returns>
        string ExportToJson();
        /// <summary>
        /// Imports the json.
        /// </summary>
        /// <param name="JSON">The json.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ImportJSON(string JSON);
    }
}
