// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DbDataType.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface I_DbDataType
    /// </summary>
    public interface I_DbDataType
    {
        /// <summary>
        /// Gets or sets the system type identifier.
        /// </summary>
        /// <value>The system type identifier.</value>
        int SystemTypeID { get; set; }
        /// <summary>
        /// Gets or sets the user type identifier.
        /// </summary>
        /// <value>The user type identifier.</value>
        int UserTypeID { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is user type.
        /// </summary>
        /// <value><c>true</c> if this instance is user type; otherwise, <c>false</c>.</value>
        bool IsUserType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is table type.
        /// </summary>
        /// <value><c>true</c> if this instance is table type; otherwise, <c>false</c>.</value>
        bool IsTableType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is nullable.
        /// </summary>
        /// <value><c>true</c> if this instance is nullable; otherwise, <c>false</c>.</value>
        bool IsNullable { get; set; }
        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        int MaxLength { get; set; }
        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        int Precision { get; set; }
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        int Scale { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
    }
}
