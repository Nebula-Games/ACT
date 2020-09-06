// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_JsonObject.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Encoding
{
    /// <summary>
    /// Interface I_JsonObject
    /// </summary>
    public interface I_JsonObject
    {
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        string ToJson();
    }
}
