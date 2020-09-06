// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_JSON_Serializer.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Serialization
{
    /// <summary>
    /// Interface I_JSON_Serializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface I_JSON_Serializer<T>
    {
        /// <summary>
        /// Froms the json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>T.</returns>
        T FromJson(string json);
        /// <summary>
        /// Converts to json.
        /// </summary>
        /// <returns>System.String.</returns>
        string ToJson();
    }
}
