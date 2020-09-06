// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Cacheable.cs" company="Stonegate Intel LLC">
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
    /// I_CacheAble
    /// </summary>
    public interface I_CacheAble
    {
        /// <summary>
        /// Get Hash ID -- Calculated By Hashing the Object
        /// </summary>
        /// <value>The hash identifier.</value>
        string HashID { get; }

        /// <summary>
        /// If the memory is cached in the systems memory (NOT Load Balanced Safe)
        /// </summary>
        /// <value><c>true</c> if [memory cached]; otherwise, <c>false</c>.</value>
        bool MemoryCached { get; set; }

        /// <summary>
        /// Cache the object in the database
        /// </summary>
        /// <value><c>true</c> if [database cached]; otherwise, <c>false</c>.</value>
        bool DatabaseCached { get; set; }

        /// <summary>
        /// JSON Configuration Data
        /// </summary>
        /// <value>The configuration data.</value>
        string ConfigurationData { get; set; }

        /// <summary>
        /// Save Update the Cache based on the HashID and the Settings
        /// </summary>
        /// <returns>I_TestResult</returns>
        /// <seealso cref="Common.I_TestResult" />
        Common.I_TestResult SaveUpdate();

        /// <summary>
        /// Retrieve the object from cache if it exists
        /// </summary>
        /// <returns>object in cache</returns>
        object Retrieve();
    }
}
