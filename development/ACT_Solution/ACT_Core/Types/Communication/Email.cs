// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Email.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.CustomAttributes;

using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;


namespace ACT.Core.Types.Communication
{
    /// <summary>
    /// Represents an Email Message
    /// </summary>
    public class EmailMessage 
    {
        /// <summary>
        /// To Addresses
        /// </summary>
        /// <value>To.</value>
        public List<MailAddress> To { get; set; }

        /// <summary>
        /// CC Addresses
        /// </summary>
        /// <value>The cc.</value>
        public List<MailAddress> CC { get; set; }

        /// <summary>
        /// BCC Addresses
        /// </summary>
        /// <value>The BCC.</value>
        public List<MailAddress> BCC { get; set; }

        /// <summary>
        /// From Address
        /// </summary>
        /// <value>From.</value>
        public MailAddress From { get; set; }

        /// <summary>
        /// ReplyToEmailAddresses
        /// </summary>
        /// <value>The reply to.</value>
        public List<string> ReplyTo { get; set; }

        /// <summary>
        /// Subject of the Email
        /// </summary>
        /// <value>The subject.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Body of the Email
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }

        /// <summary>
        /// IsHTML
        /// </summary>
        /// <value><c>true</c> if this instance is HTML; otherwise, <c>false</c>.</value>
        public bool IsHTML { get; set; }

        /// <summary>
        /// Email Message Constructor
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        /// <param name="from">From.</param>
        /// <param name="replyto">The replyto.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="ishtml">if set to <c>true</c> [ishtml].</param>
        public EmailMessage(List<MailAddress> to, List<MailAddress> cc, List<MailAddress> bcc, MailAddress from, List<string> replyto, string subject, string body, bool ishtml)
        {
            To = to;
            CC = cc;
            BCC = bcc;
            From = from;
            ReplyTo = replyto;
            Subject = subject;
            Body = body;
            IsHTML = ishtml;
        }

    }

    
}
