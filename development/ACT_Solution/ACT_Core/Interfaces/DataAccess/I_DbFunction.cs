// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-16-2015
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-16-2015
// ***********************************************************************
// <copyright file="I_DbFunction.cs" company="">
//     Copyright ©  2015
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
    /// Represents a Function In MSSQL
    /// </summary>
    public interface I_DbFunction : I_Plugin
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Is The Function Scalar
        /// </summary>
        bool IsScalar { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        System.Data.DbType DataType { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        string Owner { get; set; }

        /// <summary>
        /// Gets or sets the age in days.
        /// </summary>
        /// <value>The age in days.</value>
        int AgeInDays { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        List<I_DbFunctionParameter> Parameters { get; set; }

        /// <summary>
        /// Fully Qualified Database Name
        /// </summary>
        /// <value>The full name.</value>
        string FullName { get; }

        /// <summary>
        /// Short Name No Schema Qualifier
        /// </summary>
        /// <value>The short name.</value>
        string ShortName { get; }

        /// <summary>
        /// Gets the comments from the database must start and end with -- #####...etc
        /// </summary>
        string Comments { get; set; }

        /// <summary>
        /// Gets the actual code 
        /// </summary>
        string Code { get; set; }
    }
}
