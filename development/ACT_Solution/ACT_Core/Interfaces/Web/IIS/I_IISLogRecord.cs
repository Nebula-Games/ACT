// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_IISLogRecord.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Web.IIS
{
    /// <summary>
    /// Interface I_IISLogRecord
    /// </summary>
    public interface I_IISLogRecord
    {
        /// <summary>
        /// Gets or sets the hit data.
        /// </summary>
        /// <value>The hit data.</value>
        List<I_IISHit> HitData { get; set; }
        /// <summary>
        /// Saves to database.
        /// </summary>
        /// <param name="connectionName">Name of the connection.</param>
        /// <param name="ACT_ApplicationID">The act application identifier.</param>
        /// <param name="ACT_DomainID">The act domain identifier.</param>
        void SaveToDatabase(string connectionName, Guid ACT_ApplicationID, Guid ACT_DomainID);
    }
}
