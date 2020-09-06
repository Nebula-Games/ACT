// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_File.cs" company="Stonegate Intel LLC">
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

namespace ACT.Core.Interfaces.WebServices.Storage
{
    /// <summary>
    /// Interface I_File
    /// </summary>
    [ServiceContract]
    public interface I_File
    {
        /// <summary>
        /// Gets or sets the dbid.
        /// </summary>
        /// <value>The dbid.</value>
        [DataMember]
        int DBID { get; set; }
        /// <summary>
        /// Gets or sets the raw data.
        /// </summary>
        /// <value>The raw data.</value>
        [DataMember]
        byte[] RawData { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        [DataMember]
        string FileName { get; set; }
        /// <summary>
        /// Gets or sets the raw location.
        /// </summary>
        /// <value>The raw location.</value>
        [DataMember]
        string RawLocation { get; set; }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>The location identifier.</value>
        [DataMember]
        int LocationID { get; set; }
               
    }
}
