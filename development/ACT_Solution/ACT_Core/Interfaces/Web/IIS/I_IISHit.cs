// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_IISHit.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Web.IIS
{
    /// <summary>
    /// Based On http://www.microsoft.com/technet/prodtechnol/WindowsServer2003/Library/IIS/676400bc-8969-4aa7-851a-9319490a9bbb.mspx?mfr=true
    /// </summary>
    public interface I_IISHit
    {
        /// <summary>
        /// Gets or sets the hit date.
        /// </summary>
        /// <value>The hit date.</value>
        DateTime HitDate { get; set; }
        /// <summary>
        /// Gets or sets the name of the site.
        /// </summary>
        /// <value>The name of the site.</value>
        string SiteName { get; set; }
        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        string ServerName { get; set; }
        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>The server ip.</value>
        string ServerIP { get; set; }
        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        string Method { get; set; }
        /// <summary>
        /// Gets or sets the URI root.
        /// </summary>
        /// <value>The URI root.</value>
        string UriRoot { get; set; }
        /// <summary>
        /// Gets or sets the URI query.
        /// </summary>
        /// <value>The URI query.</value>
        string UriQuery { get; set; }
        /// <summary>
        /// Gets or sets the port number.
        /// </summary>
        /// <value>The port number.</value>
        string PortNumber { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        string UserName { get; set; }
        /// <summary>
        /// Gets or sets the client ip address.
        /// </summary>
        /// <value>The client ip address.</value>
        string ClientIPAddress { get; set; }
        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>The resource.</value>
        string Resource { get; set; }
        /// <summary>
        /// Gets or sets the sc status.
        /// </summary>
        /// <value>The sc status.</value>
        int ScStatus { get; set; }
        /// <summary>
        /// Gets or sets the sc substatus.
        /// </summary>
        /// <value>The sc substatus.</value>
        int ScSubstatus { get; set; }
        /// <summary>
        /// Gets or sets the sc win32 status.
        /// </summary>
        /// <value>The sc win32 status.</value>
        int ScWin32Status { get; set; }
        /// <summary>
        /// Gets or sets the bytes sent.
        /// </summary>
        /// <value>The bytes sent.</value>
        int BytesSent { get; set; }
        /// <summary>
        /// Gets or sets the bytes received.
        /// </summary>
        /// <value>The bytes received.</value>
        int BytesReceived { get; set; }
        /// <summary>
        /// Gets or sets the time taken.
        /// </summary>
        /// <value>The time taken.</value>
        int TimeTaken { get; set; }
        /// <summary>
        /// Gets or sets the name of the host header.
        /// </summary>
        /// <value>The name of the host header.</value>
        string HostHeaderName { get; set; }
        /// <summary>
        /// Gets or sets the cs version.
        /// </summary>
        /// <value>The cs version.</value>
        string CSVersion { get; set; }
        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        /// <value>The user agent.</value>
        string UserAgent { get; set; }
        /// <summary>
        /// Gets or sets the cookie.
        /// </summary>
        /// <value>The cookie.</value>
        string Cookie { get; set; }
        /// <summary>
        /// Gets or sets the referrer.
        /// </summary>
        /// <value>The referrer.</value>
        string Referrer { get; set; }
        /// <summary>
        /// Gets or sets the raw line.
        /// </summary>
        /// <value>The raw line.</value>
        string RawLine { get; set; }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Parses the line.
        /// </summary>
        /// <param name="lineData">The line data.</param>
        void ParseLine(string lineData);        
    }

    
}
