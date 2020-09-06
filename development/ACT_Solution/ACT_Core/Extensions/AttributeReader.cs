// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="AttributeReader.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class AttributeExtensions.
    /// </summary>
    public static class AttributeExtensions
    {
        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the t attribute.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="valueSelector">The value selector.</param>
        /// <returns>TValue.</returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            if (att != null)
            {
                return valueSelector(att);
            }
            return default(TValue);
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="t">The t.</param>
        /// <returns>System.Attribute.</returns>
        public static System.Attribute GetAttributeValue(this object x, Type t)
        {
            var _TmpReturn = System.Attribute.GetCustomAttribute(x.GetType(), t);
            return _TmpReturn;
        }
    }
}
