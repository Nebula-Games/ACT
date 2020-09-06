// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_Get_Web_Object.cs" company="Nebula Entertainment LLC">
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
    /// Interface I_ACT_Get_Web_Object
    /// </summary>
    public interface I_ACT_Get_Web_Object
    {
        /// <summary>
        /// Gets or sets the gathered data.
        /// </summary>
        /// <value>The gathered data.</value>
        Dictionary<Guid, Dictionary<string, List<string>>> GatheredData { get; set; }
        /// <summary>
        /// Logins to service.
        /// </summary>
        /// <param name="ServiceInfo">The service information.</param>
        /// <param name="UN">The un.</param>
        /// <param name="PW">The pw.</param>
        /// <param name="Token">The token.</param>
        /// <param name="Other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool LoginToService(string ServiceInfo, string UN, string PW, string Token = "", string Other = "");
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="TracerID">The tracer identifier.</param>
        /// <param name="Object">The object.</param>
        /// <param name="Conditions">The conditions.</param>
        /// <param name="Token">The token.</param>
        /// <param name="Other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool GetData(Guid TracerID, string Object, ACT.Core.Interfaces.DataAccess.I_DbWhereStatement Conditions = null, string Token = "", string Other = "");
        /// <summary>
        /// Gets or sets the system messages.
        /// </summary>
        /// <value>The system messages.</value>
        Dictionary<Guid, List<string>> SystemMessages { get; set; }
        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="TracerID">The tracer identifier.</param>
        /// <param name="Object">The object.</param>
        /// <param name="DataToSet">The data to set.</param>
        /// <param name="Conditions">The conditions.</param>
        /// <param name="Token">The token.</param>
        /// <param name="Other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool SetData(Guid TracerID, string Object, Dictionary<string,string> DataToSet, ACT.Core.Interfaces.DataAccess.I_DbWhereStatement Conditions = null, string Token = "", string Other = "");
    }
}
