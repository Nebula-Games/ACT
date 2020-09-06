// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ACT_ConfigData.cs" company="Stonegate Intel LLC">
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
    /// Holds the Configuration Template Data
    /// </summary>
    [ServiceContract]
    public interface I_ACT_ConfigData
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
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        [DataMember]
        string Product_ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember] 
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>The default value.</value>
        [DataMember]
        string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="I_ACT_ConfigData"/> is encrypted.
        /// </summary>
        /// <value><c>true</c> if encrypted; otherwise, <c>false</c>.</value>
        [DataMember]
        bool Encrypted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [internal encrypted].
        /// </summary>
        /// <value><c>true</c> if [internal encrypted]; otherwise, <c>false</c>.</value>
        [DataMember]
        bool InternalEncrypted { get; set; }

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
        /// Gets or sets a value indicating whether [base required].
        /// </summary>
        /// <value><c>true</c> if [base required]; otherwise, <c>false</c>.</value>
        [DataMember]
        bool BaseRequired { get; set; }
    }

}
