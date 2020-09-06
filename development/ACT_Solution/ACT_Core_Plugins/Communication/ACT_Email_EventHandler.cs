///-------------------------------------------------------------------------------------------------
// file:	Communication\ACT_Email_EventHandler.cs
//
// summary:	Implements the act email event handler class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;
using ACT.Core.Extensions;

namespace ACT.Plugins.Communication
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act email event handler. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/24/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_Email_EventHandler : ACT.Core.Interfaces.Communication.I_Email_Event
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the date. </summary>
        ///
        /// <value> The date. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Date { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the event. </summary>
        ///
        /// <value> The event. </value>
        ///-------------------------------------------------------------------------------------------------

        public string EventEvent { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the username. </summary>
        ///
        /// <value> The username. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Username { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the source for the. </summary>
        ///
        /// <value> from. </value>
        ///-------------------------------------------------------------------------------------------------

        public string From { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the recipient. </summary>
        ///
        /// <value> The recipient. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Recipient { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the subject. </summary>
        ///
        /// <value> The subject. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Subject { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the reason. </summary>
        ///
        /// <value> The reason. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Reason { get; set; }
        public string EmailID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/23/2019. </remarks>
        ///
        /// <param name="json"> The JSON. </param>
        ///-------------------------------------------------------------------------------------------------

        public void FromJson(string json)
        {
            var _Data = JsonConvert.DeserializeObject<ACT_Email_EventHandler>(json.URLDecode(), Settings);
            this.Date = _Data.Date;
            this.EventEvent = _Data.EventEvent;
            this.From = _Data.From;
            this.Reason = _Data.Reason;
            this.Recipient = _Data.Recipient;
            this.Subject = _Data.Subject;
            this.Username = _Data.Username;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this object to a JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/23/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Settings).URLEncode();
        }

        /// <summary>   Options for controlling the operation. </summary>
        [JsonIgnore()]
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
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
