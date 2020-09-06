// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="CookieAware_WebClient.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using ACT.Core.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace ACT.Core.Web
{
    /// <summary>
    /// Create A Cookie Aware Web Client
    /// Implements the <see cref="System.Net.WebClient" />
    /// </summary>
    /// <seealso cref="System.Net.WebClient" />
    public class CookieAwareWebClient : WebClient
    {
        /// <summary>
        /// The cookie
        /// </summary>
        private CookieContainer cookie = new CookieContainer();

        /// <summary>
        /// Gets the web request.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns>WebRequest.</returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = cookie;
            }
            return request;
        }
    }
}
