// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="MultiBase-Relationship.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Types;

namespace ACT.Core.Dynamic
{

    /// <summary>
    /// Class RelationalItem.
    /// </summary>
    public class RelationalItem
    {
        /// <summary>
        /// The name
        /// </summary>
        public string Name;
        /// <summary>
        /// The identifier
        /// </summary>
        public Guid ID = Guid.NewGuid();

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>
        /// The children
        /// </summary>
        public List<RelationalItem> Children = new List<RelationalItem>();
    }   
}
