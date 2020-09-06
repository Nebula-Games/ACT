// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_QueryResult.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Represents a Query result that holds the data at the exceptions to any queries
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// Implements the <see cref="ACT.Core.Interfaces.Serialization.I_CacheAble" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    /// <seealso cref="ACT.Core.Interfaces.Serialization.I_CacheAble" />
    public interface I_QueryResult : I_Core, Serialization.I_CacheAble
    {
        /// <summary>
        /// Gets the tables.
        /// </summary>
        /// <value>The tables.</value>
        List<System.Data.DataTable> Tables { get; }
        /// <summary>
        /// Gets the records effected.
        /// </summary>
        /// <value>The records effected.</value>
        List<int> RecordsEffected { get; }
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        List<bool> Errors { get; }
        /// <summary>
        /// Gets or sets the identities captured.
        /// </summary>
        /// <value>The identities captured.</value>
        List<string> IdentitiesCaptured { get; set; }
        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>The messages.</value>
        List<string> Messages { get; set; }
        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        List<Exception> Exceptions { get; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="I_QueryResult"/> is commited.
        /// </summary>
        /// <value><c>true</c> if commited; otherwise, <c>false</c>.</value>
        bool Commited { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [rolled back].
        /// </summary>
        /// <value><c>true</c> if [rolled back]; otherwise, <c>false</c>.</value>
        bool RolledBack { get; set; }
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        List<List<System.Data.IDataParameter>> Params { get; set; }
        /// <summary>
        /// Gets the first table.
        /// </summary>
        /// <value>The first table.</value>
        System.Data.DataTable FirstTable { get; }
        /// <summary>
        /// Firsts the data table with rows.
        /// </summary>
        /// <returns>System.Data.DataTable.</returns>
        System.Data.DataTable FirstDataTable_WithRows();
        /// <summary>
        /// Gets a value indicating whether [first query has exceptions].
        /// </summary>
        /// <value><c>true</c> if [first query has exceptions]; otherwise, <c>false</c>.</value>
        bool FirstQueryHasExceptions { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has valid data.
        /// </summary>
        /// <value><c>true</c> if this instance has valid data; otherwise, <c>false</c>.</value>
        bool HasValidData { get; }
        /// <summary>
        /// Determines whether this instance has rows.
        /// </summary>
        /// <returns><c>true</c> if this instance has rows; otherwise, <c>false</c>.</returns>
        bool HasRows();

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="ColumnName">Name of the column.</param>
        /// <param name="Row">The row.</param>
        /// <returns>System.Object.</returns>
        object GetValue(string ColumnName, int Row = 0);

        /// <summary>
        /// Tests the return data.
        /// </summary>
        /// <param name="ErrorMessageForClient">The error message for client.</param>
        /// <param name="TestForNoRecords">if set to <c>true</c> [test for no records].</param>
        /// <param name="NoRecordsIsClientIssue">if set to <c>true</c> [no records is client issue].</param>
        /// <param name="TestForNULLValue">if set to <c>true</c> [test for null value].</param>
        /// <param name="NULLValueIsClientIssue">if set to <c>true</c> [null value is client issue].</param>
        /// <param name="TestForBlankValue">if set to <c>true</c> [test for blank value].</param>
        /// <param name="BlankValueIsClientIssue">if set to <c>true</c> [blank value is client issue].</param>
        /// <param name="TestForMoreThanOneRecord">if set to <c>true</c> [test for more than one record].</param>
        /// <param name="MoreThanOneRecorIsClientIssue">if set to <c>true</c> [more than one recor is client issue].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Test_Return_Data(string ErrorMessageForClient, bool TestForNoRecords, bool NoRecordsIsClientIssue, bool TestForNULLValue, bool NULLValueIsClientIssue, bool TestForBlankValue, bool BlankValueIsClientIssue, bool TestForMoreThanOneRecord, bool MoreThanOneRecorIsClientIssue);
        

    }
}
