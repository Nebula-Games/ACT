// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_ConfigTemplate.cs" company="Nebula Entertainment LLC">
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
    /// Represents a ACT Configuration File Template
    /// </summary>
    [ServiceContract]
    public interface I_ACT_ConfigTemplate
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        string ID { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>The application identifier.</value>
        [DataMember]
        string Application_ID { get; set; }

        /// <summary>
        /// Gets or sets the member identifier.
        /// </summary>
        /// <value>The member identifier.</value>
        [DataMember]
        string Member_ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>The tags.</value>
        [DataMember]
        string Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is public.
        /// </summary>
        /// <value><c>true</c> if this instance is public; otherwise, <c>false</c>.</value>
        [DataMember]
        bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        [DataMember]
        bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the plugin data.
        /// </summary>
        /// <value>The plugin data.</value>
        [DataMember]
        List<I_ACT_ConfigPluginData> PluginData { get; set; }

        /// <summary>
        /// Gets or sets the configuration data.
        /// </summary>
        /// <value>The configuration data.</value>
        [DataMember]
        List<I_ACT_ConfigData> ConfigData { get; set; }

    }

}
