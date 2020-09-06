// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="JSON_DataSet.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Data;

namespace ACT.Core.Helper.JSON
{

    /// <summary>
    /// Converts a <see cref="DataSet" /> object to JSON. No support for reading.
    /// Implements the <see cref="Newtonsoft.Json.JsonConverter" />
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.JsonConverter" />
    public class DataSetConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="dataset">The dataset.</param>
        /// <param name="SER">The ser.</param>
        public override void WriteJson(JsonWriter writer, object dataset, Newtonsoft.Json.JsonSerializer SER = null)
        {
            DataSet dataSet = dataset as DataSet;

            DataTableConverter converter = new DataTableConverter();

            writer.WriteStartObject();

            writer.WritePropertyName("Tables");
            writer.WriteStartArray();

            foreach (DataTable table in dataSet.Tables)
            {
                converter.WriteJson(writer, table, SER);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }

        /// <summary>
        /// Determines whether this instance can convert the specified value type.
        /// </summary>
        /// <param name="valueType">Type of the value.</param>
        /// <returns><c>true</c> if this instance can convert the specified value type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type valueType)
        {
            return typeof(DataSet).IsAssignableFrom(valueType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="obj">The object.</param>
        /// <param name="SER">The ser.</param>
        /// <returns>The object value.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object ReadJson(JsonReader reader, Type objectType, object obj, Newtonsoft.Json.JsonSerializer SER = null)
        {
            throw new NotImplementedException();
        }
    }
}

