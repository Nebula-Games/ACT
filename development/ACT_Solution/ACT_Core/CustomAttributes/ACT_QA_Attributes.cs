// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_QA_Attributes.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.CustomAttributes
{
    /// <summary>
    /// This indicates the Target(s) expensive and to what level the cost is relative to Infinite Perfection and Frozen
    /// Implements the <see cref="System.Attribute" />
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Class)]
    public class ACT_Expensive : System.Attribute
    {
        /// <summary>
        /// What is the cost of this expensive code
        /// </summary>
        public ExpenseRating SpeedCost;

        /// <summary>
        /// Constructor For Marking Code as Expensive or Above Normal Measure
        /// </summary>
        /// <param name="ProcessingCost">What is the cost of this expensive code</param>
        public ACT_Expensive(ExpenseRating ProcessingCost) { SpeedCost = ProcessingCost; }
    }

    /// <summary>
    /// Rates the expense of the Rating
    /// </summary>
    public enum ExpenseRating
    {
        /// <summary>
        /// Above Normal Defined Speed
        /// </summary>
        Minimal,
        /// <summary>
        /// a concerning amount of performance impact
        /// </summary>
        Signifigant,
        /// <summary>
        /// Will slow the program down noticably
        /// </summary>
        Serious,
        /// <summary>
        /// Will slow the program down Never use in Releases
        /// </summary>
        Maximum,
        /// <summary>
        /// Scary Use at your own risk
        /// </summary>
        RediculasOnlyUseWhenAbsolutlyNeeded,
        /// <summary>
        /// Bad but unknown dont use in releases ever.
        /// </summary>
        Unknown
    }
}
