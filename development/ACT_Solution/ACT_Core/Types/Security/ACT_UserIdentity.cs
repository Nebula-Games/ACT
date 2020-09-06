///-------------------------------------------------------------------------------------------------
// file:	Types\Security\ACT_UserIdentity.cs
//
// summary:	Implements the act user identity class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Types.Security
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act user identify. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/1/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public partial class ACT_UserIdentify
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the firstname. </summary>
        ///
        /// <value> The firstname. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("firstname", NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the lastname. </summary>
        ///
        /// <value> The lastname. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("lastname", NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the email. </summary>
        ///
        /// <value> The email. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the identifierhash. </summary>
        ///
        /// <value> The identifierhash. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("identifierhash", NullValueHandling = NullValueHandling.Ignore)]
        public string Identifierhash { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the website. </summary>
        ///
        /// <value> The website. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("website", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Website { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the bio. </summary>
        ///
        /// <value> The bio. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("bio", NullValueHandling = NullValueHandling.Ignore)]
        public string Bio { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the updatedate. </summary>
        ///
        /// <value> The updatedate. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("updatedate", NullValueHandling = NullValueHandling.Ignore)]
        public string Updatedate { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/1/2019. </remarks>
        ///
        /// <param name="json"> The JSON. </param>
        ///
        /// <returns>   An ACT_UserIdentify. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static ACT_UserIdentify FromJson(string json) => JsonConvert.DeserializeObject<ACT_UserIdentify>(json, ACT.Core.Serialization.JsonSerializationSettings.Settings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this object to a JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/1/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string ToJson() => JsonConvert.SerializeObject(this, ACT.Core.Serialization.JsonSerializationSettings.Settings);

    }

   

}
