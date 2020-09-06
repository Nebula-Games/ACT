// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Object.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// An IEnumerable&lt;T&gt; extension method that first or default from many.
        /// </summary>
        ///
        /// <remarks>   Mark Alicz, 10/28/2016. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="source">           The source to act on. </param>
        /// <param name="childrenSelector"> The children selector. </param>
        /// <param name="condition">        The condition. </param>
        ///
        /// <returns>   A T. </returns>
        /// <summary>
        /// Firsts the or default from many.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="childrenSelector">The children selector.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>T.</returns>
        public static T FirstOrDefaultFromMany<T>(
            this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector,
            Predicate<T> condition)
        {
            // return default if no items
            if (source == null || !source.Any()) return default(T);

            // return result if found and stop traversing hierarchy
            var attempt = source.FirstOrDefault(t => condition(t));
            if (!Equals(attempt, default(T))) return attempt;

            // recursively call this function on lower levels of the
            // hierarchy until a match is found or the hierarchy is exhausted
            return source.SelectMany(childrenSelector)
                .FirstOrDefaultFromMany(childrenSelector, condition);
        }

        /// <summary>
        /// Converts to bool.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool? ToBool(this object x)
        {
            try
            {
                return Convert.ToBoolean(x);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts to guid.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Nullable&lt;Guid&gt;.</returns>
        public static Guid? ToGuid(this object x)
        {
            try
            {
                return x.ToString().TryToGuid();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts to nullableint.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public static int? ToNullableInt(this object x)
        {
            if (x == null || x == DBNull.Value) { return null; }
            else
            {
                var _tmpA = x.ToString().ToInt(-1);
                if (_tmpA == -1) { return null; }
                return _tmpA;
            }
        }

        /// <summary>
        /// Converts the string representation of a number to an integer.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt(this object x, int _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                return x.ToString().ToInt(_Default);
            }
        }

        /// <summary>
        /// Converts to double.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(this object x, double _Default = 0d)
        {
            if (x.TryToString() == null)
            {
                return _Default;
            }
            else
            {
                try
                {
                    return Convert.ToDouble(x);
                }
                catch
                {
                    return _Default;
                }
            }
        }

        /// <summary>
        /// Converts to long.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.Int64.</returns>
        public static long ToLong(this object x, long _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                return x.ToString().ToLong(_Default);
            }
        }

        /// <summary>
        /// Converts to ulong.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.UInt64.</returns>
        public static ulong ToULong(this object x, ulong _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                try
                {
                    return Convert.ToUInt64(x);
                }
                catch
                {
                    return _Default;
                }
            }
        }

        /// <summary>
        /// Converts to float.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.Single.</returns>
        public static float ToFloat(this object x, float _Default = 0f)
        {
            if (x.TryToString() == null)
            {
                return _Default;
            }
            else
            {
                try
                {
                    return Convert.ToSingle(x);
                }
                catch
                {
                    return _Default;
                }

            }
        }

        /// <summary>
        /// Converts to bool.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">if set to <c>true</c> [default].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool ToBool(this object x, bool _Default = false)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                return x.ToString().ToBool(_Default);
            }
        }

        /// <summary>
        /// Converts to short.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.Int16.</returns>
        public static short ToShort(this object x, short _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                try
                {
                    return Convert.ToInt16(x);
                }
                catch
                {
                    return _Default;
                }
            }
        }

        /// <summary>
        /// Converts to ushort.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.UInt16.</returns>
        public static ushort ToUShort(this object x, ushort _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                try
                {
                    return Convert.ToUInt16(x);
                }
                catch
                {
                    return _Default;
                }
            }
        }

        /// <summary>
        /// Converts to decimal.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="_Default">The default.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal ToDecimal(this object x, decimal _Default = 0)
        {
            if (x == null || x == DBNull.Value)
            {
                return _Default;
            }
            else
            {
                return x.ToString().ToDecimal(_Default);
            }
        }

        /// <summary>
        /// Converts to datetime.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? ToDateTime(this object x)
        {
            try
            {
                return Convert.ToDateTime(Convert.ToDateTime(x).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>System.String.</returns>
        public static string TryToString(this object x)
        {
            try
            {
                return x.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Tries to string.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="DefaultVal">The default value.</param>
        /// <returns>System.String.</returns>
        public static string TryToString(this object x, string DefaultVal = "")
        {
            try
            {
                return x.ToString();
            }
            catch
            {
                return DefaultVal;
            }
        }

        /// <summary>
        /// Objects to byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] ObjectToByteArray(this Object obj)
        {
            if (obj == null) { return null; }

            if (obj == DBNull.Value) { return null; }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Corrects the type value.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="t">The t.</param>
        /// <returns>System.Object.</returns>
        public static object CorrectTypeValue(this object obj, Type t)
        {
            if (t == typeof(string))
            {
                if (obj == DBNull.Value) { return ""; }
                else { return obj.TryToString(""); }
            }
            else if (t == typeof(int) || t == typeof(Int32) || t == typeof(Int32?))
            {

                return obj.ToInt(0);
            }
            else if (t == typeof(double))
            {
                return obj.ToDouble(0);
            }
            else if (t == typeof(float))
            {
                return obj.ToFloat(0);
            }
            else if (t == typeof(decimal))
            {
                return obj.ToDecimal(0);
            }
            else if (t == typeof(long) || t == typeof(Int64))
            {
                return obj.ToLong(0);
            }
            else if (t == typeof(ulong))
            {
                return obj.ToULong(0);
            }
            else if (t == typeof(short) || t == typeof(Int16))
            {
                return obj.ToShort(0);
            }
            else
            {
                return obj;
            }
        }
    }
}
