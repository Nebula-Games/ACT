///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Plugins\I_Simple_Plugin.cs
//
// summary:	Declares the I_Simple_Plugin interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using ACT.Core.Interfaces.Security.Authentication;
using ACT.Core.Interfaces.Common;



///-------------------------------------------------------------------------------------------------
// namespace: ACT.Core.Interfaces.Plugins
//
// summary:	.
///-------------------------------------------------------------------------------------------------
namespace ACT.Core.Interfaces.Plugins
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for simple plugin. </summary>
    ///
    /// <remarks>   Mark Alicz, 11/10/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public interface I_Simple_Plugin
    {
        /// <summary>
        /// Sets the Impersonation of the Executing User Level
        /// </summary>
        /// <param name="UserInfo">The user information.</param>
        void SetImpersonate(I_UserInfo UserInfo);

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
}
