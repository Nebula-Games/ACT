// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_APP_ENVIRONMENT_CONFIG_FILE.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Application
{
    /// <summary>
    /// Interface I_ACT_APP_ENVIRONMENT_CONFIG_FILE
    /// </summary>
    public interface I_ACT_APP_ENVIRONMENT_CONFIG_FILE
    {
        /// <summary>
        /// Gets or sets the environment identifier.
        /// </summary>
        /// <value>The environment identifier.</value>
        Guid EnvironmentID { get; set; }
        /// <summary>
        /// Sets the name of the environment.
        /// </summary>
        /// <value>The name of the environment.</value>
        string EnvironmentName { set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        string FileName { get; set; }
        /// <summary>
        /// Gets or sets the file data.
        /// </summary>
        /// <value>The file data.</value>
        string FileData { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [act configuration].
        /// </summary>
        /// <value><c>true</c> if [act configuration]; otherwise, <c>false</c>.</value>
        bool ACTConfig { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [web configuration].
        /// </summary>
        /// <value><c>true</c> if [web configuration]; otherwise, <c>false</c>.</value>
        bool WebConfig { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [other configuration].
        /// </summary>
        /// <value><c>true</c> if [other configuration]; otherwise, <c>false</c>.</value>
        bool OtherConfig { get; set; }
        /// <summary>
        /// Gets or sets the other destination.
        /// </summary>
        /// <value>The other destination.</value>
        string OtherDestination { get; set; }
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        int Version { get; set; }
    }
}
