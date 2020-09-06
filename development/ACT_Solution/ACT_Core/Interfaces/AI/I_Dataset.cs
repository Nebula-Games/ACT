// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Dataset.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.AI
{
    /// <summary>
    /// Represents a Single data Set
    /// Implements the <see cref="ACT.Core.Interfaces.IO.I_Exportable{System.Byte[]}" />
    /// Implements the <see cref="ACT.Core.Interfaces.IO.I_Saveable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.IO.I_Exportable{System.Byte[]}" />
    /// <seealso cref="ACT.Core.Interfaces.IO.I_Saveable" />
    public interface I_Dataset : IO.I_Exportable<byte[]>, IO.I_Saveable
    {
        /// <summary>
        /// Internal ID for long term data storage and retrieval
        /// </summary>
        /// <value>The identifier.</value>
        Guid ID { get; }

        /// <summary>
        /// Unique Identifier External and Unstored
        /// </summary>
        /// <value>The unique identifier.</value>
        string UniqueIdentifier { get; set; }

        /// <summary>
        /// Gets the Data Column Identifier
        /// </summary>
        /// <value>The columns.</value>
        Dictionary<string, I_DataColumnIdentifier> Columns { get; set; }

    }
}
