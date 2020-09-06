// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Enum.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ACT.Core.CustomAttributes;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class EnumType.
    /// </summary>
    public static class EnumType
    {
        /// <summary>
        /// Extension Method For Custom Attribute StringValue
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue(this Enum x)
        {            
            string output = "";
            Type type = x.GetType();

            FieldInfo fi = type.GetField(x.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            
            if (attrs.Length > 0) { output = attrs[0].Value; }

            return output;
        }

        /// <summary>
        /// Reverse Operation For CustomAttribute String Value
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="v">The v.</param>
        /// <returns>Enum.</returns>
        public static Enum FromStringValue(this Enum x, string v)
        {
            if (v == null) { return null; }

            Type t = x.GetType();
            FieldInfo[] f = t.GetFields();

            foreach (FieldInfo fi in f)
            {
                var _R = fi.GetCustomAttributes(typeof(StringValue), false);

                if (_R.Count() > 0)
                {
                    if ((_R[0] as StringValue).Value == v)
                    {
                        return (Enum)fi.GetValue(x);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Froms the string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="v">The v.</param>
        /// <returns>Enum.</returns>
        public static Enum FromString(this Enum x, string v)
        {            
            return (Enum)Enum.Parse(x.GetType(), v);
        }

        /// <summary>
        /// String Value Custom Attribute Contains Definition Check
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="v">The v.</param>
        /// <returns><c>true</c> if [has string value] [the specified v]; otherwise, <c>false</c>.</returns>
        public static bool HasStringValue(this Enum x, string v)
        {
            if (x.FromStringValue(v) == null)
            {
                return false;
            }

            return true;
        }

    }
}
