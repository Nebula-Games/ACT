// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="GenericInformationReturn.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Database
{
    /// <summary>
    /// Class GenericInformationReturn.
    /// </summary>
    [Serializable]
    public class GenericInformationReturn
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>The uid.</value>
        public string UID { get; set; }
        /// <summary>
        /// Gets or sets the image base64.
        /// </summary>
        /// <value>The image base64.</value>
        public string ImageBase64 { get; set; }

        /// <summary>
        /// Gets or sets the query result.
        /// </summary>
        /// <value>The query result.</value>
        public ACT.Core.Interfaces.DataAccess.I_QueryResult QueryResult { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GenericInformationReturn"/> is error.
        /// </summary>
        /// <value><c>true</c> if error; otherwise, <c>false</c>.</value>
        public bool Error { get; set; }
        /// <summary>
        /// Gets or sets the error exception.
        /// </summary>
        /// <value>The error exception.</value>
        public Exception ErrorException { get; set; }
        /// <summary>
        /// Gets or sets the redirect URL.
        /// </summary>
        /// <value>The redirect URL.</value>
        public string RedirectURL { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GenericInformationReturn"/> is redirect.
        /// </summary>
        /// <value><c>true</c> if redirect; otherwise, <c>false</c>.</value>
        public bool Redirect { get; set; }


    }
}
