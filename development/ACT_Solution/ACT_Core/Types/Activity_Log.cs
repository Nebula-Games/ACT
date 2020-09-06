// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Activity_Log.cs" company="Stonegate Intel LLC">
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
    /// Class Activity_Log.
    /// </summary>
    public class Activity_Log
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        public string Activity { get; set; }
        /// <summary>
        /// Gets or sets the user account.
        /// </summary>
        /// <value>The user account.</value>
        public string UserAccount { get; set; }
        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        /// <value>The old value.</value>
        public string OldValue { get; set; }
        /// <summary>
        /// Creates new value.
        /// </summary>
        /// <value>The new value.</value>
        public string NewValue { get; set; }
        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        /// <value>The application.</value>
        public string Application { get; set; }
    }
}
