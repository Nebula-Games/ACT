// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Db.cs" company="Nebula Entertainment LLC">
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
    /// Defines the Structure of a Database
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_Db : I_Plugin
    {
        /// <summary>
        /// Gets or sets the data access class.
        /// </summary>
        /// <value>The data access class.</value>
        I_DataAccess DataAccessClass { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <value>The table names.</value>
        List<string> TableNames { get; }
        /// <summary>
        /// Gets the stored procedure names.
        /// </summary>
        /// <value>The stored procedure names.</value>
        List<string> StoredProcedureNames { get; }
        /// <summary>
        /// Gets the type names.
        /// </summary>
        /// <value>The type names.</value>
        List<string> TypeNames { get; }
        /// <summary>
        /// Gets the view names.
        /// </summary>
        /// <value>The view names.</value>
        List<string> ViewNames { get; }

        /// <summary>
        /// Gets or sets the tables.
        /// </summary>
        /// <value>The tables.</value>
        BindingList<I_DbTable> Tables { get; set; }

        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        List<I_DbDataType> Types { get; set; }
        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>The views.</value>
        List<I_DbView> Views { get; set; }
        /// <summary>
        /// Gets or sets the stored procedures.
        /// </summary>
        /// <value>The stored procedures.</value>
        List<I_DbStoredProcedure> StoredProcedures { get; set; }

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>I_DbTable.</returns>
        I_DbTable GetTable(string Name, bool IgnoreCase);
        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns>I_DbTable.</returns>
        I_DbTable GetTable(int Index);

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>I_DbView.</returns>
        I_DbView GetView(string Name, bool IgnoreCase);
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns>I_DbView.</returns>
        I_DbView GetView(int Index);

        /// <summary>
        /// Gets the table count.
        /// </summary>
        /// <value>The table count.</value>
        int TableCount { get; }

        /// <summary>
        /// Gets the view count.
        /// </summary>
        /// <value>The view count.</value>
        int ViewCount { get; }
        /// <summary>
        /// Adds the table.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult AddTable(I_DbTable Table);
        /// <summary>
        /// Adds the view.
        /// </summary>
        /// <param name="View">The view.</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult AddView(I_DbView View);

        /// <summary>
        /// Removes the table.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="IgnoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult RemoveTable(string Name, bool IgnoreCase);
        /// <summary>
        /// Removes the table.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult RemoveTable(int Index);

        /// <summary>
        /// Included to reduce 1 line of developers code :-&gt;
        /// </summary>
        /// <param name="Original">The original.</param>
        /// <param name="New">The new.</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult ModifyTable(I_DbTable Original, I_DbTable New);
        /// <summary>
        /// Modifies the table.
        /// </summary>
        /// <param name="OriginalName">Name of the original.</param>
        /// <param name="New">The new.</param>
        /// <returns>I_TestResult.</returns>
        I_TestResult ModifyTable(string OriginalName, I_DbTable New);

        /// <summary>
        /// Validate the Current Structure of the Database
        /// </summary>
        /// <returns>I_TestResult</returns>
        I_TestResult Validate();
        
    }
}
