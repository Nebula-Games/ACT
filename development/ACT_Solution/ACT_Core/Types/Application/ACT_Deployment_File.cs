///-------------------------------------------------------------------------------------------------
// file:	Types\Application\ACT_Deployment_File.cs
//
// summary:	Implements the act deployment file class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;



namespace ACT.Core.Types.Application
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act deployment file. </summary>
    ///
    /// <remarks>   Mark Alicz, 8/3/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_Deployment_File
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("name")]
        public string Name { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the parts. </summary>
        ///
        /// <value> The parts. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("parts")]
        public List<ACT_Deployment_Part> Parts { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   From JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/3/2019. </remarks>
        ///
        /// <param name="json"> . </param>
        ///
        /// <returns>   An ACT_Deployment_File. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static ACT_Deployment_File FromJson(string json) => JsonConvert.DeserializeObject<ACT_Deployment_File>(json, JSONConverterSettings);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   From JSON. </summary>
        ///
        /// <remarks>   Mark Alicz, 8/3/2019. </remarks>
        ///
        /// <returns>   This object as a string. </returns>
        ///
        /// ### <param name="self"> . </param>
        ///-------------------------------------------------------------------------------------------------

        public string ToJson() => JsonConvert.SerializeObject(this, JSONConverterSettings);

        /// <summary>   JSON Converter Settings. </summary>
        public static readonly JsonSerializerSettings JSONConverterSettings = new JsonSerializerSettings
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
    /// <summary>   An act deployment part. </summary>
    ///
    /// <remarks>   Mark Alicz, 8/3/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_Deployment_Part
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("name")]
        public string Name { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the steps. </summary>
        ///
        /// <value> The steps. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("steps")]
        public List<ACT_Deployment_Step> Steps { get; set; }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act deployment step. </summary>
    ///
    /// <remarks>   Mark Alicz, 8/3/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_Deployment_Step
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the type of the step. </summary>
        ///
        /// <value> The type of the step. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("steptype")]
        public string StepType { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the value. </summary>
        ///
        /// <value> The value. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("val")]
        public string Val { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the variable. </summary>
        ///
        /// <value> The variable. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("variable")]
        public string Variable { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the environment. </summary>
        ///
        /// <value> The environment. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("environment")]
        public string Environment { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the name of the database connection. </summary>
        ///
        /// <value> The name of the database connection. </value>
        ///-------------------------------------------------------------------------------------------------

        [JsonProperty("conn")]
        public string DBConnectionName { get; set; }

    }
}
