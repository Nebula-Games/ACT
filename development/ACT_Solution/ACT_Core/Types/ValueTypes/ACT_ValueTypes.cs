// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_ValueTypes.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Reflection;
using ACT.Core.Extensions;
using System.Collections.Generic;

namespace ACT.Core.Types.ValueTypes
{
    /// <summary>
    /// Struct Email
    /// </summary>
    public struct Email
    {
        /// <summary>
        /// The value
        /// </summary>
        private readonly string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="Exception">Invalid Email</exception>
        public Email(string value)
        {
            if (value.IsValidEmail())
            {
                this.value = value;
            }
            else
            {
                throw new Exception("Invalid Email");
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get { return value; } }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="Email"/>.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator Email(string s)
        {
            return new Email(s);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Email"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(Email p)
        {
            return p.Value;
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A JSON Object from a String. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/22/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public struct JSONString
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the raw JSON. </summary>
        ///
        /// <value> The raw JSON. </value>
        ///-------------------------------------------------------------------------------------------------

        public string RawJSON { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the JSON object. </summary>
        ///
        /// <value> The JSON object. </value>
        ///-------------------------------------------------------------------------------------------------

        public dynamic JSONObject { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Process this object. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/22/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public void Process()
        {
            JSONObject = Newtonsoft.Json.Linq.JObject.Parse(RawJSON);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given string. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/22/2019. </remarks>
        ///
        /// <param name="RawJSON">  The raw JSON. </param>
        ///
        /// <returns>   A JSONString. </returns>
        ///-------------------------------------------------------------------------------------------------
        public static JSONString FromString(string RawJSON)
        {
            JSONString _tmpReturn = new JSONString();
            _tmpReturn.Process();
            return _tmpReturn;
        }
    }
}
