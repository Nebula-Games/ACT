// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbStoredProcedure.cs" company="Nebula Entertainment LLC">
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
    /// Represents a Database Stored Procedure
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_DbStoredProcedure : I_Plugin
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

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
        List<I_DbStoredProcedureParameter> Parameters { get; set; }

        /// <summary>
        /// Fully Qualified Database Name
        /// </summary>
        /// <value>The full name.</value>
        string FullName { get;  }

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
