// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACTSqlProxy.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.WebServices.Data
{
    /// <summary>
    /// Interface I_ACTSqlProxy
    /// </summary>
    public interface I_ACTSqlProxy
    {
        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="EncryptedConnectionData">The encrypted connection data.</param>
        /// <param name="Command">The command.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>System.String.</returns>
        string GetDataTable(string EncryptedConnectionData, string Command, string Parameters);
    }
}
