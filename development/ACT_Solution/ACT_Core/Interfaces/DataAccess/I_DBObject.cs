// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DBObject.cs" company="Stonegate Intel LLC">
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
using ACT.Core.Types;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface I_DBObject
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Saveable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Saveable" />
    public interface I_DBObject : I_Core, I_Saveable
    {

        /// <summary>
        /// Returns a object that represnts a Database Table.  Used primarly for AUTO Generated Code.
        /// </summary>
        /// <param name="PhysicalTableName">The Physical Table Name</param>
        /// <returns>object representing that table.</returns>
        object ReturnDatabaseChild(string PhysicalTableName);
        /// <summary>
        /// Generates the SQL insert.
        /// </summary>
        /// <param name="IncludeNulls">if set to <c>true</c> [include nulls].</param>
        /// <returns>System.String.</returns>
        string GenerateSQLInsert(bool IncludeNulls);
        /// <summary>
        /// Generates the SQL update.
        /// </summary>
        /// <param name="IncludeNulls">if set to <c>true</c> [include nulls].</param>
        /// <returns>System.String.</returns>
        string GenerateSQLUpdate(bool IncludeNulls);
        /// <summary>
        /// Searches the specified search criteria.
        /// </summary>
        /// <param name="SearchCriteria">The search criteria.</param>
        /// <returns>System.Object.</returns>
        object Search(List<Class_SearchCriteria> SearchCriteria);


    }
}
