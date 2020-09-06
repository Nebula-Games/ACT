// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Location.cs" company="Stonegate Intel LLC">
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

namespace ACT.Core.Interfaces.WebServices.Storage
{
    /// <summary>
    /// Interface I_Location
    /// </summary>
    [ServiceContract]
    public interface I_Location
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        int ID { get; set; }
        /// <summary>
        /// Gets or sets the raw location.
        /// </summary>
        /// <value>The raw location.</value>
        [DataMember]
        string RawLocation { get; set; }
        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        [DataMember]
        List<string> Files { get; set; }
        /// <summary>
        /// Gets or sets the child location names.
        /// </summary>
        /// <value>The child location names.</value>
        [DataMember]
        List<string> ChildLocationNames { get; set; }
        /// <summary>
        /// Gets or sets the child location raw.
        /// </summary>
        /// <value>The child location raw.</value>
        [DataMember]
        List<string> ChildLocationRaw { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [version control].
        /// </summary>
        /// <value><c>true</c> if [version control]; otherwise, <c>false</c>.</value>
        [DataMember]
        bool VersionControl { get; set; }
    }
}
