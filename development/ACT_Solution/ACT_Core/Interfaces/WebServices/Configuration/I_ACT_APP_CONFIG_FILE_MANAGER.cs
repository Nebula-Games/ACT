// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_APP_CONFIG_FILE_MANAGER.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.WebServices.Configuration
{
    /// <summary>
    /// Interface I_ACT_APP_CONFIG_FILE_MANAGER
    /// </summary>
    public interface I_ACT_APP_CONFIG_FILE_MANAGER
    {
        /// <summary>
        /// Saves the configuration file.
        /// </summary>
        /// <param name="ConfigFile">The configuration file.</param>
        /// <param name="UserToken">The user token.</param>
        /// <param name="AppKey">The application key.</param>
        /// <returns>System.Int32.</returns>
        int SaveConfigurationFile(ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE ConfigFile, string UserToken, string AppKey);
        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="EnvironmentName">Name of the environment.</param>
        /// <param name="UserToken">The user token.</param>
        /// <param name="AppKey">The application key.</param>
        /// <returns>ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE.</returns>
        ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE GetConfigurationFile(string FileName, string EnvironmentName, string UserToken, string AppKey);
        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="EnvironmentName">Name of the environment.</param>
        /// <param name="SpecificVersion">The specific version.</param>
        /// <param name="UserToken">The user token.</param>
        /// <param name="AppKey">The application key.</param>
        /// <returns>ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE.</returns>
        ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE GetConfigurationFile(string FileName, string EnvironmentName, int SpecificVersion, string UserToken, string AppKey);
        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="EnvironmentID">The environment identifier.</param>
        /// <param name="UserToken">The user token.</param>
        /// <param name="AppKey">The application key.</param>
        /// <returns>ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE.</returns>
        ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE GetConfigurationFile(string FileName, Guid EnvironmentID, string UserToken, string AppKey);
        /// <summary>
        /// Gets the configuration file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <param name="EnvironmentID">The environment identifier.</param>
        /// <param name="SpecificVersion">The specific version.</param>
        /// <param name="UserToken">The user token.</param>
        /// <param name="AppKey">The application key.</param>
        /// <returns>ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE.</returns>
        ACT.Core.Interfaces.Application.I_ACT_APP_ENVIRONMENT_CONFIG_FILE GetConfigurationFile(string FileName, Guid EnvironmentID, int SpecificVersion, string UserToken, string AppKey);
    }
}
