// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_AutoUpdate.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ACT.Core.Extensions;

namespace ACT.Core.Types.ACTStudio.ApplicationLibrary
{
    /// <summary>
    /// Auto Update Class
    /// </summary>
    public class AutoUpdate
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        [JsonProperty("applicationname", NullValueHandling = NullValueHandling.Ignore)]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets the updatefrequency.
        /// </summary>
        /// <value>The updatefrequency.</value>
        [JsonProperty("updatefrequency", NullValueHandling = NullValueHandling.Ignore)]
        public string Updatefrequency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AutoUpdate"/> is notify.
        /// </summary>
        /// <value><c>null</c> if [notify] contains no value, <c>true</c> if [notify]; otherwise, <c>false</c>.</value>
        [JsonProperty("notify", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Notify { get; set; }
    }
}
