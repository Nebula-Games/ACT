// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_BatchManager.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Communication
{
    /// <summary>
    /// Interface I_BatchManager
    /// </summary>
    public interface I_BatchManager
    {
        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <value>The name of the batch.</value>
        string BatchName { get; set; }
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>The external identifier.</value>
        string ExternalID { get; set; }


    }
}
