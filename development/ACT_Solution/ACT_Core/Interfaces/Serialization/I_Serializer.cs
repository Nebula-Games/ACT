// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Serializer.cs" company="Nebula Entertainment LLC">
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
    /// Interface I_Serializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface I_Serializer<T>
    {
        /// <summary>
        /// Exports the specified source object.
        /// </summary>
        /// <param name="SourceObject">The source object.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>T.</returns>
        T Export(I_Core SourceObject, params string[] Parameters);

        /// <summary>
        /// Imports the specified source.
        /// </summary>
        /// <param name="Source">The source.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns>T.</returns>
        T Import(T Source, params string[] Parameters);
    }
}
