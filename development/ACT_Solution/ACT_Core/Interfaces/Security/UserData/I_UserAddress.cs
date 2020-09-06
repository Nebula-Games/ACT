// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_UserAddress.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Interfaces.Security.UserData
{
    /// <summary>
    /// Defines a Basic Placeholder for passing User State
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Saveable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Saveable" />
    public interface I_UserAddress : I_Plugin, I_Saveable
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string ID { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        string Address1 { get; set; }
        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>The address2.</value>
        string Address2 { get; set; }
        /// <summary>
        /// Gets or sets the address3.
        /// </summary>
        /// <value>The address3.</value>
        string Address3 { get; set; }
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        string City { get; set; }
        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        string PostalCode { get; set; }
        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        string State { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        string Country { get; set; }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        string Phone { get; set; }
        /// <summary>
        /// Gets or sets the fax.
        /// </summary>
        /// <value>The fax.</value>
        string Fax { get; set; }
        /// <summary>
        /// Gets or sets the type of the address.
        /// </summary>
        /// <value>The type of the address.</value>
        string AddressType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary shipping.
        /// </summary>
        /// <value><c>true</c> if this instance is primary shipping; otherwise, <c>false</c>.</value>
        bool IsPrimaryShipping { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary billing.
        /// </summary>
        /// <value><c>true</c> if this instance is primary billing; otherwise, <c>false</c>.</value>
        bool IsPrimaryBilling { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary contact.
        /// </summary>
        /// <value><c>true</c> if this instance is primary contact; otherwise, <c>false</c>.</value>
        bool IsPrimaryContact { get; set; }

       
    }
}
