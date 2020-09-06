// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_Configuration.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
#if DOTNETFRAMEWORK
using System.ServiceModel.Web;
#endif
using System.Text;


namespace ACT.Core.Interfaces.WebServices.Configuration
{
    /// <summary>
    /// This is a Public ACT Web Service Interface
    /// </summary>
    [ServiceContract]
    public interface I_ACT_Configuration
    {
        /// <summary>
        /// Gets all configuration templates.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>List&lt;I_ACT_ConfigTemplate&gt;.</returns>
        [OperationContract]
        List<I_ACT_ConfigTemplate> GetAllConfigurationTemplates(string Token, string APIKey);

        /// <summary>
        /// Gets the configuration template details.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="TemplateID">The template identifier.</param>
        /// <returns>I_ACT_ConfigTemplate.</returns>
        [OperationContract]
        I_ACT_ConfigTemplate GetConfigurationTemplateDetails(string Token, string APIKey, string TemplateID);

        /// <summary>
        /// Saves the configuration template.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="tmplate">The tmplate.</param>
        /// <returns>Common.I_TestResult.</returns>
        [OperationContract]
        Common.I_TestResult SaveConfigurationTemplate(string Token, string APIKey, I_ACT_ConfigTemplate tmplate);

        /// <summary>
        /// Saves all configuration templates.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="templatedata">The templatedata.</param>
        /// <returns>List&lt;Common.I_TestResult&gt;.</returns>
        [OperationContract]
        List<Common.I_TestResult> SaveAllConfigurationTemplates(string Token, string APIKey, List<I_ACT_ConfigTemplate> templatedata);

        /// <summary>
        /// Gets the system default template.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <returns>I_ACT_ConfigTemplate.</returns>
        [OperationContract]
        I_ACT_ConfigTemplate GetSystemDefaultTemplate(string Token, string APIKey);

        /// <summary>
        /// Gets the product default template.
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <param name="APIKey">The API key.</param>
        /// <param name="ProductID">The product identifier.</param>
        /// <returns>I_ACT_ConfigTemplate.</returns>
        [OperationContract]
        I_ACT_ConfigTemplate GetProductDefaultTemplate(string Token, string APIKey, string ProductID);
    }
}
