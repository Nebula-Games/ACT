// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Support_Types.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Web.DevExpress
{
    /// <summary>
    /// MenuItem used for various DEVEXPRESS Controls
    /// </summary>
    public struct MenuItem
    {
        /// <summary>
        /// The text
        /// </summary>
        public string Text;
        /// <summary>
        /// The name
        /// </summary>
        public string Name;
        /// <summary>
        /// The image URL
        /// </summary>
        public string ImageURL;
        /// <summary>
        /// The navigate URL
        /// </summary>
        public string NavigateURL;
        /// <summary>
        /// The target
        /// </summary>
        public string Target;
        /// <summary>
        /// The raise event
        /// </summary>
        public string RaiseEvent;
    }
}
