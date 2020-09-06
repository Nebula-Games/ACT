// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_DataObject.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface I_DataObject
    /// </summary>
    public interface I_DataObject
    {
        /// <summary>
        /// Gets the primary key.
        /// </summary>
        /// <value>The primary key.</value>
        string PrimaryKey { get;  }
        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult Update();
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>I_QueryResult.</returns>
        I_QueryResult Create();

        
    }
}
