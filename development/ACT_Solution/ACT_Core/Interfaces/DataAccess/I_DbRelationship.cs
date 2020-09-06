// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbRelationship.cs" company="Nebula Entertainment LLC">
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
using System.Data;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface Defines a DBRelationship
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    public interface I_DbRelationship : I_Core
    {
        /// <summary>
        /// Gets or sets a value indicating whether [multi field relationship].
        /// </summary>
        /// <value><c>true</c> if [multi field relationship]; otherwise, <c>false</c>.</value>
        bool MultiFieldRelationship { get; set; }

        /// <summary>
        /// Adds the column.
        /// </summary>
        /// <param name="ColName">Name of the col.</param>
        /// <param name="ExternalColName">Name of the external col.</param>
        void AddColumn(string ColName, string ExternalColName);

        /// <summary>
        /// Gets or sets the column names.
        /// </summary>
        /// <value>The column names.</value>
        List<string> ColumnNames { get; set; }
        /// <summary>
        /// Gets or sets the external column names.
        /// </summary>
        /// <value>The external column names.</value>
        List<string> ExternalColumnNames { get; set; }
        /// <summary>
        /// Column Name Where Relationship Is Found
        /// </summary>
        /// <value>The name of the column.</value>
        string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        /// <value>The type of the column.</value>
        DbType ColumnType { get; set; }
        /// <summary>
        /// Table Name Where Relationship Is Found (Fully Qualified)
        /// </summary>
        /// <value>The name of the table.</value>
        string TableName { get; set; }

        /// <summary>
        /// Short Name of Table Where Relationship is Found
        /// </summary>
        /// <value>The short name of the table.</value>
        string ShortTableName { get; set; }

        /// <summary>
        /// Relationship Name..  Changed depending on Origin (FK_NAME,PK_NAME)
        /// </summary>
        /// <value>The name of the relationship.</value>
        string RelationshipName { get; set; }

        /// <summary>
        /// Table Name Where Relationship Matched (Fully Qualified)
        /// </summary>
        /// <value>The name of the external table.</value>
        string External_TableName { get; set; }

        /// <summary>
        /// Gets or sets the type of the external column.
        /// </summary>
        /// <value>The type of the external column.</value>
        DbType External_ColumnType { get; set; }
        /// <summary>
        /// Short Name of Table Where Relationship is Matched
        /// </summary>
        /// <value>The short name of the external table.</value>
        string ShortExternal_TableName { get; set; }

        /// <summary>
        /// Column Name Where Relationship Is Matched
        /// </summary>
        /// <value>The name of the external column.</value>
        string External_ColumnName { get; set; }

        /// <summary>
        /// Specifies if this relationship is a ForeignKey or not Depending on Origin
        /// </summary>
        /// <value><c>true</c> if this instance is foreign key; otherwise, <c>false</c>.</value>
        bool IsForeignKey { get; set; }

        
    }
}
