///-------------------------------------------------------------------------------------------------
// file:	Types\Web\ACT_UserControl_Replacements.cs
//
// summary:	Implements the act user control replacements class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Types.Web
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A user control replacements. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public partial class UserControlReplacements
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the version. </summary>
        ///
        /// <value> The version. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Version { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the replacements. </summary>
        ///
        /// <value> The replacements. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("replacements", NullValueHandling = NullValueHandling.Ignore)]
        public List<Replacement> Replacements { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object from the given JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
        ///
        /// <param name="json"> The JSON. </param>
        ///
        /// <returns>   The UserControlReplacements. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static UserControlReplacements FromJson(string json) => JsonConvert.DeserializeObject<UserControlReplacements>(json, Converter.Settings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Converts this object to a JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string ToJson() => JsonConvert.SerializeObject(this, Converter.Settings);
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A replacement. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public partial class Replacement
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the replacementvariable. </summary>
        ///
        /// <value> The replacementvariable. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("replacementvariable", NullValueHandling = NullValueHandling.Ignore)]
        public string Replacementvariable { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the default. </summary>
        ///
        /// <value> The default. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("default", NullValueHandling = NullValueHandling.Ignore)]
        public string Default { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets options for controlling the operation. </summary>
        ///
        /// <value> The options. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Options { get; set; }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A converter. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    internal static class Converter
    {
        /// <summary>   Options for controlling the operation. </summary>
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

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A parse string converter. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    internal class ParseStringConverter : JsonConverter
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Determines whether this instance can convert the specified object type. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
        ///
        /// <param name="t">    Type of the object. </param>
        ///
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        ///-------------------------------------------------------------------------------------------------

        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Reads the JSON representation of the object. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="reader">           The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from. </param>
        /// <param name="t">                Type of the object. </param>
        /// <param name="existingValue">    The existing value of object being read. </param>
        /// <param name="serializer">       The calling serializer. </param>
        ///
        /// <returns>   The object value. </returns>
        ///-------------------------------------------------------------------------------------------------

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Writes the JSON representation of the object. </summary>
        ///
        /// <remarks>   Mark Alicz, 9/26/2019. </remarks>
        ///
        /// <param name="writer">       The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to. </param>
        /// <param name="untypedValue"> The value. </param>
        /// <param name="serializer">   The calling serializer. </param>
        ///-------------------------------------------------------------------------------------------------

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        /// <summary>   The singleton. </summary>
        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

}
