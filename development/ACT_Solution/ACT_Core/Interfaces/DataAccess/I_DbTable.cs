// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbTable.cs" company="Nebula Entertainment LLC">
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
    /// Interface I_DbTable
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// Implements the <see cref="System.IComparable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    /// <seealso cref="System.IComparable" />
    public interface I_DbTable : I_Core, IComparable
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
        /// Remove a Column From A Index Position
        /// </summary>
        /// <param name="Index">The index.</param>
        void RemoveColumn(int Index);

        /// <summary>
        /// Remove a Column by Name
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        void RemoveColumn(string Name, bool IgnoreCase);

        /// <summary>
        /// Gets the Primary Keys
        /// </summary>
        /// <value>The get primary column names.</value>
        List<string> GetPrimaryColumnNames { get; set; }

        /// <summary>
        /// Return the Column Count
        /// </summary>
        /// <value>The column count.</value>
        int ColumnCount { get; }



        /// <summary>
        /// Is a System Table
        /// </summary>
        /// <value><c>true</c> if this instance is system; otherwise, <c>false</c>.</value>
        bool IsSystem { get; }

        /// <summary>
        /// Is a Package Table??
        /// </summary>
        /// <value><c>true</c> if this instance is package table; otherwise, <c>false</c>.</value>
        bool IsPackageTable { get; }

        /// <summary>
        /// Is a UserTable
        /// </summary>
        /// <value><c>true</c> if this instance is user table; otherwise, <c>false</c>.</value>
        bool IsUserTable { get; }

        /// <summary>
        /// We only Support Single Primary Keys.  NO Composite Keys!
        /// </summary>
        /// <value>The name of the primary key column.</value>
        string PrimaryKeyColumnName { get; }


        /// <summary>
        /// FOREIGN Keys are only the ones that are found using sp_fkeys in MSSSQL
        /// </summary>
        /// <value>The relationship count.</value>
        int RelationshipCount { get; }
        /// <summary>
        /// Adds the relationship.
        /// </summary>
        /// <param name="Relationship">The relationship.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AddRelationship(I_DbRelationship Relationship);
        /// <summary>
        /// Removes the relationship.
        /// </summary>
        /// <param name="Index">The index.</param>
        void Remove_Relationship(int Index);
        /// <summary>
        /// Removes the relationship.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        void Remove_Relationship(string Name, bool IgnoreCase);
        /// <summary>
        /// Gets the relationship.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>I_DbRelationship.</returns>
        I_DbRelationship GetRelationship(string Name, bool IgnoreCase);
        /// <summary>
        /// Gets the relationship.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns>I_DbRelationship.</returns>
        I_DbRelationship GetRelationship(int Index);
        /// <summary>
        /// Gets all relationships.
        /// </summary>
        /// <value>All relationships.</value>
        BindingList<I_DbRelationship> AllRelationships { get; }

        /// <summary>
        /// Good luck implemeting this . I choose to only process this on create
        /// </summary>
        /// <param name="ColName">Name of the col.</param>
        void MoveColumnUp(string ColName);
        /// <summary>
        /// Good luck implemeting this .  I choose to only process this on create
        /// </summary>
        /// <param name="ColName">Name of the col.</param>
        void MoveColumnDown(string ColName);

        /// <summary>
        /// Generates the data export structure.
        /// </summary>
        /// <returns>System.String.</returns>
        string GenerateDataExportStructure();
        /// <summary>
        /// Gets the insert data SQL.
        /// </summary>
        /// <param name="Fields">The fields.</param>
        /// <returns>System.String.</returns>
        string GetInsertDataSQL(List<string> Fields);
        /// <summary>
        /// Gets the update data SQL.
        /// </summary>
        /// <param name="Fields">The fields.</param>
        /// <param name="Where">The where.</param>
        /// <returns>System.String.</returns>
        string GetUpdateDataSQL(List<string> Fields,I_DbWhereStatement Where);
        /// <summary>
        /// Gets the update data SQL.
        /// </summary>
        /// <param name="Fields">The fields.</param>
        /// <returns>System.String.</returns>
        string GetUpdateDataSQL(List<string> Fields);
        /// <summary>
        /// Gets the delete data SQL.
        /// </summary>
        /// <param name="Where">The where.</param>
        /// <returns>System.String.</returns>
        string GetDeleteDataSQL(I_DbWhereStatement Where);

        /// <summary>
        /// Gets or sets the age in days.
        /// </summary>
        /// <value>The age in days.</value>
        int AgeInDays { get; set; }
    }
}
