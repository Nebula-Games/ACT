// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Boolean_Return.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types
{
    /// <summary>
    /// Class ACT_Boolean_Return.
    /// </summary>
    [Serializable()]
    public class ACT_Boolean_Return
    {
        /// <summary>
        /// The messages
        /// </summary>
        public List<string> Messages;
        /// <summary>
        /// The overall success
        /// </summary>
        public bool OverallSuccess;
    }
}
