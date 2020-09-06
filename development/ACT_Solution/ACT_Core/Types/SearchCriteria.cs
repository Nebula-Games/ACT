// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="SearchCriteria.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;

namespace ACT.Core.Types
{
    /// <summary>
    /// Specifies a structure that defines a way to search a Class
    /// </summary>
    public class Class_SearchCriteria
    {
        /// <summary>
        /// The column name
        /// </summary>
        public string ColumnName;
        /// <summary>
        /// The column value
        /// </summary>
        public object ColumnValue;
        /// <summary>
        /// The compare method
        /// </summary>
        public Operators CompareMethod;
    }
}
