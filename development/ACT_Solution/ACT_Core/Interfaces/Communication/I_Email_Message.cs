// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_EMessage.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Communication
{

   
    /// <summary>
    /// Interface I_Email_Message
    /// </summary>
    public interface I_Email_Message
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is HTML.
        /// </summary>
        /// <value><c>true</c> if this instance is HTML; otherwise, <c>false</c>.</value>
        bool IsHTML { get; set; }

        /// <summary>
        /// Unique Message ID
        /// </summary>
        /// <value>The unique message identifier.</value>
        string UniqueMessageID { get; set; }

        /// <summary>
        /// Global Senders ID
        /// </summary>
        /// <value>The sender identifier.</value>
        string SenderID { get; set; }

        /// <summary>
        /// Global Recievers ID
        /// </summary>
        /// <value>The reciver identifier.</value>
        string RecipientID { get; set; }

        /// <summary>
        /// Specific Message Subject
        /// </summary>
        /// <value>The subject text.</value>
        string SubjectText { get; set; }

        /// <summary>
        /// Specific Message Body
        /// </summary>
        /// <value>The body text.</value>
        string BodyText { get; set; }
        
        /// <summary>
        /// Specific Message Body
        /// </summary>
        /// <value>The body text.</value>
        string BodyHTML { get; set; }

        /// <summary>
        /// Specific Touch Chain For This Message (Includes Complete History)
        /// </summary>
        /// <value>The touch chain.</value>
        string TouchChainID { get; }

        /// <summary>
        /// Specific Meta Data
        /// </summary>
        /// <value>The meta data.</value>
        string MetaData { get; }


    }
}
