// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-19-2019
// ***********************************************************************
// <copyright file="I_UserInfo.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.UserData;

namespace ACT.Core.Interfaces.Security.Authentication
{
    /// <summary>
    /// Defines a common USER Information Structure
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Saveable" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Saveable" />
    public interface I_UserInfo : I_Plugin, I_Saveable
    {
        #region Data Members (9) 

        /// <summary>
        /// Current Authentication Token
        /// </summary>
        /// <value>The authentication token.</value>
        string AuthenticationToken { get; set; }

        /// <summary>
        /// Is User Active
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        bool Active { get; set; }

        /// <summary>
        /// Additional User Information
        /// </summary>
        /// <value>The additional information.</value>
        Dictionary<string, string> AdditionalInfo { get; set; }

        /// <summary>
        /// Is The Current User Authenticated
        /// </summary>
        /// <value><c>true</c> if authenticated; otherwise, <c>false</c>.</value>
        bool Authenticated { get; set; }

        /// <summary>
        /// Related Company IDs
        /// </summary>
        /// <value>The related company identifiers.</value>
        List<string> RelatedCompanyIdentifiers { get; set; }

        #region Data Elements

        /// <summary>
        /// Primary Key Of The User
        /// </summary>
        /// <value>The user key.</value>
        string UserKey { get; set; }

        /// <summary>
        /// UserName Associated With This User
        /// </summary>
        /// <value>The name of the user.</value>
        string UserName { get; set; }

        /// <summary>
        /// Used In Update And Create Procedures Otherwise Ignored
        /// </summary>
        /// <value>The password.</value>
        string Password { get; set; }

        /// <summary>
        /// Email Address Of The User
        /// </summary>
        /// <value>The email.</value>
        string Email { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        /// <value>The name of the company.</value>
        string CompanyName { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        /// <value>The first name.</value>
        string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        /// <value>The last name.</value>
        string LastName { get; set; }

        /// <summary>
        /// Middle Name
        /// </summary>
        /// <value>The name of the middle.</value>
        string MiddleName { get; set; }

        /// <summary>
        /// Work Phone
        /// </summary>
        /// <value>The work phone.</value>
        string WorkPhone { get; set; }

        /// <summary>
        /// Work Phone Ext
        /// </summary>
        /// <value>The work phone ext.</value>
        string WorkPhoneExt { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        /// <value>The mobile phone.</value>
        string MobilePhone { get; set; }

        /// <summary>
        /// User Encryption Key
        /// </summary>
        /// <value>The encryption key.</value>
        string EncryptionKey { get; set; }

        /// <summary>
        /// Confirmation Code To Validate Email
        /// </summary>
        /// <value>The confirmation code.</value>
        string ConfirmationCode { get; set; }


        #endregion



        #endregion Data Members 

        #region Operations (6) 

        /// <summary>
        /// Optional Function To Get UnEncrypted Password
        /// </summary>
        /// <returns>System.String.</returns>
        string GetUnencryptedPassword();

        /// <summary>
        /// List Of All The Users Groups
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetGroups();

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="NewAddress">Creates new address.</param>
        void AddAddress(I_UserAddress NewAddress);
        /// <summary>
        /// Updates the address.
        /// </summary>
        /// <param name="AddressToUpdate">The address to update.</param>
        void UpdateAddress(I_UserAddress AddressToUpdate);
        /// <summary>
        /// Deletes the address.
        /// </summary>
        /// <param name="AddressToDelete">The address to delete.</param>
        void DeleteAddress(I_UserAddress AddressToDelete);

        /// <summary>
        /// Gets the address by identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>I_UserAddress.</returns>
        I_UserAddress GetAddressByID(Guid ID);
        /// <summary>
        /// Gets the name of the address by.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>I_UserAddress.</returns>
        I_UserAddress GetAddressByName(string Name);
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <returns>List&lt;I_UserAddress&gt;.</returns>
        List<I_UserAddress> GetAddresses();
        /// <summary>
        /// Gets the primary billing address.
        /// </summary>
        /// <returns>I_UserAddress.</returns>
        I_UserAddress GetPrimaryBillingAddress();
        /// <summary>
        /// Gets the primary shipping address.
        /// </summary>
        /// <returns>I_UserAddress.</returns>
        I_UserAddress GetPrimaryShippingAddress();
        /// <summary>
        /// Gets the primary contact address.
        /// </summary>
        /// <returns>I_UserAddress.</returns>
        I_UserAddress GetPrimaryContactAddress();

        /// <summary>
        /// Gets the user contacts.
        /// </summary>
        /// <returns>List&lt;I_UserContact&gt;.</returns>
        List<I_UserContact> GetUserContacts();
        /// <summary>
        /// Gets the contact.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns>I_UserContact.</returns>
        I_UserContact GetContact(Guid ID);
        /// <summary>
        /// Searches the specified search string.
        /// </summary>
        /// <param name="SearchString">The search string.</param>
        /// <returns>List&lt;I_UserContact&gt;.</returns>
        List<I_UserContact> Search(string SearchString);

        /// <summary>
        /// Updates the contact.
        /// </summary>
        /// <param name="ContactToUpdate">The contact to update.</param>
        void UpdateContact(I_UserContact ContactToUpdate);
        /// <summary>
        /// Adds the contact.
        /// </summary>
        /// <param name="ContactToAdd">The contact to add.</param>
        void AddContact(I_UserContact ContactToAdd);
        /// <summary>
        /// Deletes the contact.
        /// </summary>
        /// <param name="ContactToDelete">The contact to delete.</param>
        void DeleteContact(I_UserContact ContactToDelete);
                
		#endregion Operations 
    }


   
}
