// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbColumn.cs" company="Nebula Entertainment LLC">
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
    /// Interface I_DbColumn
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// Implements the <see cref="System.IComparable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    /// <seealso cref="System.IComparable" />
    public interface I_DbColumn : I_Core, IComparable
    {
        /// <summary>
        /// Reference to the Parent Table
        /// </summary>
        /// <value>The parent table.</value>
        I_DbTable ParentTable { get; set; }

        /// <summary>
        /// Fully Qualified Name
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Short Name
        /// </summary>
        /// <value>The short name.</value>
        string ShortName { get; set; }

        /// <summary>
        /// Data Type Of Column
        /// </summary>
        /// <value>The type of the data.</value>
        System.Data.DbType DataType { get; set; }

        /// <summary>
        /// Default Value i.e GetDate() or 1 or '1' etc..
        /// </summary>
        /// <value>The default.</value>
        string Default { get; set; }

        /// <summary>
        /// Allows Nulls
        /// </summary>
        /// <value><c>true</c> if nullable; otherwise, <c>false</c>.</value>
        bool Nullable { get; set; }

        /// <summary>
        /// Size of Data Column
        /// </summary>
        /// <value>The size.</value>
        int Size { get; set; }

        /// <summary>
        /// The Precision
        /// </summary>
        /// <value>The precision.</value>
        int Precision { get; set; }

        /// <summary>
        /// The Scale
        /// </summary>
        /// <value>The scale.</value>
        int Scale { get; set; }

        /// <summary>
        /// Is this an Identity Column
        /// </summary>
        /// <value><c>true</c> if identity; otherwise, <c>false</c>.</value>
        bool Identity { get; set; }

        /// <summary>
        /// Auto Increment
        /// </summary>
        /// <value><c>true</c> if [automatic increment]; otherwise, <c>false</c>.</value>
        bool AutoIncrement { get; set; }

        /// <summary>
        /// Identity Increment
        /// </summary>
        /// <value>The identity increment.</value>
        int IdentityIncrement { get; set; }

        /// <summary>
        /// Identity Seed
        /// </summary>
        /// <value>The identity seed.</value>
        int IdentitySeed { get; set; }

        /// <summary>
        /// Is this Column a Primary Key
        /// </summary>
        /// <value><c>true</c> if this instance is primary key; otherwise, <c>false</c>.</value>
        bool IsPrimaryKey { get; set; }

        /// <summary>
        /// ColumnDescription
        /// </summary>
        /// <value>The description.</value>
        string Description { get; set; }
    }
    
}
