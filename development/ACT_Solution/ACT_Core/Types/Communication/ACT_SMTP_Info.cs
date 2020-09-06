///-------------------------------------------------------------------------------------------------
// file:	Types\Communication\ACT_SMTP_Info.cs
//
// summary:	Implements the act SMTP information class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ACT.Core.Types.Communication
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Information about the act smtp. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/30/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_SMTP_Info
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP host. </summary>
        ///
        /// <value> The SMTP host. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtphost", NullValueHandling = NullValueHandling.Ignore)]
        public string SmtpHost { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP port. </summary>
        ///
        /// <value> The SMTP port. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtpport", NullValueHandling = NullValueHandling.Ignore)]
        public string SmtpPort { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the SMTP user./ </summary>
        ///
        /// <value> The name of the SMTP user. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtpusername", NullValueHandling = NullValueHandling.Ignore)]
        public string SmtpUserName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP password. </summary>
        ///
        /// <value> The SMTP password. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtppassword", NullValueHandling = NullValueHandling.Ignore)]
        public string SmtpPassword { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP API key. </summary>
        ///
        /// <value> The SMTP API key. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtpapikey", NullValueHandling = NullValueHandling.Ignore)]
        public string SmtpApiKey { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the SMTP API secret. </summary>
        ///
        /// <value> The SMTP API secret. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("smtpapisecret", NullValueHandling = NullValueHandling.Ignore)]
        public string SmtpApiSecret { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets information describing the additional. </summary>
        ///
        /// <value> Information describing the additional. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("additionalinfo", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AdditionalInfo { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the API plugin full class. </summary>
        ///
        /// <value> The name of the API plugin full class. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("apipluginfullclassname", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> APIPluginFullClassName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/30/2019. </remarks>
        ///
        /// <param name="json"> The JSON. </param>
        ///
        /// <returns>   An ACT_SMTP_Info. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static ACT_SMTP_Info FromJson(string json) => JsonConvert.DeserializeObject<ACT_SMTP_Info>(json, JSONSettings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this object to a JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/30/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string ToJson() => JsonConvert.SerializeObject(this, JSONSettings);


        /// <summary>   The JSON settings. </summary>
        public static readonly JsonSerializerSettings JSONSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

    }
}
