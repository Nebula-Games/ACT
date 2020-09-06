// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_PaymentGateway.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Interfaces.Payment
{
    /// <summary>
    /// Interface I_PaymentGateway
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_PaymentGateway : I_Plugin
    {
        /// <summary>
        /// Wases the successfull authorize.
        /// </summary>
        /// <param name="Response">The response.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WasSuccessfullAuthorize(Dictionary<string, string> Response);
        /// <summary>
        /// Wases the successfull capture.
        /// </summary>
        /// <param name="Response">The response.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WasSuccessfullCapture(Dictionary<string, string> Response);
        /// <summary>
        /// Wases the successfull credit.
        /// </summary>
        /// <param name="Response">The response.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool WasSuccessfullCredit(Dictionary<string, string> Response);

        /// <summary>
        /// Priors the authentication capture.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="Amount">The amount.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        Dictionary<string, string> PriorAuthCapture(string TransactionID, decimal Amount);
        /// <summary>
        /// Authorizes the cc.
        /// </summary>
        /// <param name="Amount">The amount.</param>
        /// <param name="Track1">The track1.</param>
        /// <param name="Track2">The track2.</param>
        /// <param name="CardCode">The card code.</param>
        /// <param name="ZipCode">The zip code.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        Dictionary<string, string> AuthorizeCC(decimal Amount, string Track1, string Track2, string CardCode, string ZipCode);
        /// <summary>
        /// Authorizes the cc.
        /// </summary>
        /// <param name="CCNumber">The cc number.</param>
        /// <param name="Month">The month.</param>
        /// <param name="Year">The year.</param>
        /// <param name="Amount">The amount.</param>
        /// <param name="Description">The description.</param>
        /// <param name="CardCode">The card code.</param>
        /// <param name="ZipCode">The zip code.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        Dictionary<string, string> AuthorizeCC(string CCNumber, string Month, string Year, decimal Amount, string Description, string CardCode, string ZipCode);
        /// <summary>
        /// Credits the specified transaction identifier.
        /// </summary>
        /// <param name="TransactionID">The transaction identifier.</param>
        /// <param name="Amount">The amount.</param>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        Dictionary<string, string> Credit(string TransactionID, decimal Amount, string cardNumber);

    }
}
