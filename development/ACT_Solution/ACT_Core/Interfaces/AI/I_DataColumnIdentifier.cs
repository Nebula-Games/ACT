// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DataColumnIdentifier.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.AI
{
    /// <summary>
    /// Column Identifier
    /// </summary>
    public interface I_DataColumnIdentifier
    {
        /// <summary>
        /// Column Name
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Column Weight
        /// </summary>
        /// <value>The weight.</value>
        decimal Weight { get; set; }

        /// <summary>
        /// Unique System Identifier
        /// </summary>
        /// <value>The identifier.</value>
        Guid ID { get; }

        /// <summary>
        /// System Identifier
        /// </summary>
        /// <value>The identifier.</value>
        string Identifier { get; set; }


    }
}
