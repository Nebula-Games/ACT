// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Randomization.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Random
{
    /// <summary>
    /// Interface I_Randomization
    /// </summary>
    public interface I_Randomization
    {
        /// <summary>
        /// Nexts the specified minimum value.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>System.Int32.</returns>
        int Next(int minValue, int maxValue);
        /// <summary>
        /// Nexts this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int Next();
        /// <summary>
        /// Nexts the specified maximum value.
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns>System.Int32.</returns>
        int Next(int maxValue);
        /// <summary>
        /// Nexts the double.
        /// </summary>
        /// <returns>System.Double.</returns>
        double NextDouble();

    }
}
