// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Plugin.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Interfaces.Common
{
    /// <summary> 
    /// Defines a Plugin
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    public interface I_Plugin : I_Core
    {
        /// <summary>
        /// Sets the Impersonation of the Executing User Level
        /// </summary>
        /// <param name="UserInfo">The user information.</param>
        void SetImpersonate(object UserInfo);

        /// <summary>
        /// Returns all the System Settings Required By The Plugin
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> ReturnSystemSettingRequirements();

        /// <summary>
        /// Validates the Configuration File is Setup Properly
        /// </summary>
        /// <returns>I_TestResult.</returns>
        I_TestResult ValidatePluginRequirements();
    }

    /// <summary>
    /// Interface IWebPlugin
    /// Implements the <see cref="ACT.Core.Interfaces.I_Core" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.I_Core" />
    public interface IWebPlugin : I_Core
    {
        /// <summary>
        /// Returns the system setting requirements.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> ReturnSystemSettingRequirements();

        /// <summary>
        /// Generates the output.
        /// </summary>
        /// <param name="StartSurroundTag">The start surround tag.</param>
        /// <param name="Data">The data.</param>
        /// <param name="EndSurroundTag">The end surround tag.</param>
        /// <returns>System.String.</returns>
        string GenerateOutput(string StartSurroundTag, Dictionary<string, object> Data, string EndSurroundTag);

    }
}
