// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_Actions.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using J = Newtonsoft.Json.JsonPropertyAttribute;
using R = Newtonsoft.Json.Required;
using N = Newtonsoft.Json.NullValueHandling;
using ACT.Core.Extensions;

namespace ACT.Core.Types.ACTAction
{
    /// <summary>
    /// Container class holding all of the Actions
    /// </summary>
    public class ActActions
    {
        /// <summary>
        /// Gets or sets the actions.
        /// </summary>
        /// <value>The actions.</value>
        [J("actions", NullValueHandling = N.Ignore)] public List<Action> Actions { get; set; }

        /// <summary>
        /// Converts this object to JSON
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, JSONSettings);

        /// <summary>
        /// custom Converter Settings
        /// </summary>
        public static readonly JsonSerializerSettings JSONSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        /// <summary>
        /// Returns the Actions From a string.
        /// </summary>
        /// <param name="json">JSON Data</param>
        /// <returns>ACTActions Object</returns>
        public static ActActions FromJson(string json) => JsonConvert.DeserializeObject<ActActions>(json, JSONSettings);

        /// <summary>
        /// Returns the Actions From a File.  Decrypts the data if needed
        /// </summary>
        /// <param name="fileName">JSON File Containing the Action JSON Data</param>
        /// <param name="Encrypted">True/False if the JSON File is Encrypted</param>
        /// <param name="Password">Optional Password - Default uses internal encryption</param>
        /// <returns>ACTActions Object</returns>
        /// <exception cref="Exception">Encryption Error.  Please ensure DLL, Password Information is correct (See Log For More Information) : " + ex.Message</exception>
        public static ActActions FromFile(string fileName, bool Encrypted, string Password = "")
        {
            string _JSON = "";

            if (Encrypted)
            {
                var _EncryptionDLL = ACT.Core.CurrentCore<ACT.Core.Interfaces.Security.Encryption.I_Encryption>.GetCurrent();

                try
                {
                    if (Password.NullOrEmpty()) { _JSON = _EncryptionDLL.Decrypt(System.IO.File.ReadAllText(fileName)); }
                    else { _JSON = _EncryptionDLL.Decrypt(System.IO.File.ReadAllText(fileName), Password); }
                }
                catch (Exception ex)
                {
                    // TODO Log Error
                    throw new Exception("Encryption Error.  Please ensure DLL, Password Information is correct (See Log For More Information) : " + ex.Message);
                }
            }
            else { _JSON = System.IO.File.ReadAllText(fileName); }

            return FromJson(_JSON);
        }
    }

    /// <summary>
    /// Single Action Specification
    /// </summary>
    public class Action
    {
        /// <summary>
        /// Gets or sets the action action.
        /// </summary>
        /// <value>The action action.</value>
        [J("action", NullValueHandling = N.Ignore)] public string ActionAction { get; set; }
        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        [J("arguments", NullValueHandling = N.Ignore)] public List<Argument> Arguments { get; set; }
        /// <summary>
        /// Gets or sets the plugin.
        /// </summary>
        /// <value>The plugin.</value>
        [J("plugin", NullValueHandling = N.Ignore)] public string Plugin { get; set; }
        /// <summary>
        /// Gets or sets the fullclassname.
        /// </summary>
        /// <value>The fullclassname.</value>
        [J("fullclassname", NullValueHandling = N.Ignore)] public string Fullclassname { get; set; }
    }

    /// <summary>
    /// Single Action Argument
    /// </summary>
    public class Argument
    {
        /// <summary>
        /// Gets or sets the argument argument.
        /// </summary>
        /// <value>The argument argument.</value>
        [J("argument", NullValueHandling = N.Ignore)] public string ArgumentArgument { get; set; }
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>The values.</value>
        [J("values", NullValueHandling = N.Ignore)] public List<string> Values { get; set; }
    }
}
