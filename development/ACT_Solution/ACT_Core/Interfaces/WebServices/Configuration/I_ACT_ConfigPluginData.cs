// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_ConfigPluginData.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

using System.Text;


namespace ACT.Core.Interfaces.WebServices.Configuration
{
    /// <summary>
    /// Holds the Configuration Template Data
    /// </summary>
    [ServiceContract]
    public interface I_ACT_ConfigPluginData
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        string ID { get; set; }

        /// <summary>
        /// Gets or sets the template identifier.
        /// </summary>
        /// <value>The template identifier.</value>
        [DataMember]
        string Template_ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the interface.
        /// </summary>
        /// <value>The name of the interface.</value>
        [DataMember]
        string InterfaceName { get; set; }

        /// <summary>
        /// Gets or sets the name of the DLL.
        /// </summary>
        /// <value>The name of the DLL.</value>
        [DataMember] 
        string DLLName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the class.
        /// </summary>
        /// <value>The full name of the class.</value>
        [DataMember]
        string FullClassName { get; set; }

        /// <summary>
        /// Gets or sets the plugin arguments.
        /// </summary>
        /// <value>The plugin arguments.</value>
        [DataMember]
        string PluginArguments { get; set; }
    }

}
