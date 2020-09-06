// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Principal_Extensions.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if DOTNETFRAMEWORK
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
#endif

namespace ACT.Core.Extensions
{
#if DOTNETFRAMEWORK
    /// <summary>
    /// Class AccountManagementExtensions.
    /// </summary>
    public static class AccountManagementExtensions
    {

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="property">The property.</param>
        /// <returns>String.</returns>
        public static String GetProperty(this Principal principal, String property)
        {
            DirectoryEntry directoryEntry = principal.GetUnderlyingObject() as DirectoryEntry;
            if (directoryEntry.Properties.Contains(property))
                return directoryEntry.Properties[property].Value.ToString();
            else
                return String.Empty;
        }

        /// <summary>
        /// Gets the company.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>String.</returns>
        public static String GetCompany(this Principal principal)
        {
            return principal.GetProperty("company");
        }

        /// <summary>
        /// Gets the department.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>String.</returns>
        public static String GetDepartment(this Principal principal)
        {
            return principal.GetProperty("department");
        }

    }
#endif
}
