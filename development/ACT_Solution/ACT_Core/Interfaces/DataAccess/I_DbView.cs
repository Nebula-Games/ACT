// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbView.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;
using System.ComponentModel;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface I_DbView
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// Implements the <see cref="System.IComparable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    /// <seealso cref="System.IComparable" />
    public interface I_DbView : I_Core, IComparable
    {
        /// <summary>
        /// Enumerable List of Columns
        /// </summary>
        /// <value>The columns.</value>
        BindingList<I_DbColumn> Columns { get; set; }

        /// <summary>
        /// Parent Of This Table
        /// </summary>
        /// <value>The parent database.</value>
        I_Db ParentDatabase { get; set; }

        /// <summary>
        /// Name of the Physical Table.  FULLY Qualified Name i.e [DatabaseA].[dbo].[Member]
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Name of the table.  Not Fully Qualified. i.e. Member
        /// </summary>
        /// <value>The short name.</value>
        string ShortName { get; set; }

        /// <summary>
        /// Description Of The Table
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }

        /// <summary>
        /// Owner of the Table
        /// </summary>
        /// <value>The owner.</value>
        string Owner { get; set; }

        /// <summary>
        /// Add a Column to the Structure
        /// </summary>
        /// <param name="Column">The column.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AddColumn(I_DbColumn Column);

        /// <summary>
        /// Get Column by Name
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>IDbColumn</returns>
        I_DbColumn GetColumn(string Name, bool IgnoreCase);

        /// <summary>
        /// Get Column at Index Position
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns>IDbColumn</returns>
        I_DbColumn GetColumn(int Index);

        /// <summary>
        /// Age in Days
        /// </summary>
        /// <value>The age in days.</value>
        int AgeInDays { get; set; }
    }
}
