// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbStoredProcedureParameter.cs" company="Nebula Entertainment LLC">
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
    /// Interface I_DbStoredProcedureParameter
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_DbStoredProcedureParameter : I_Plugin
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        System.Data.DbType DataType { get; set; }
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; set; }
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; set; }

        /// <summary>
        /// Gets or sets the name of the data type.
        /// </summary>
        /// <value>The name of the data type.</value>
        string DataTypeName { get; set; }
    }
}
