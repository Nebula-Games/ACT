// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Array.cs" company="Nebula Entertainment LLC">
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
    /// Class ArrayExtensions.
    /// </summary>
    public static class ArrayExtensions
    {


        /// <summary>
        /// Converts a byte array to a string
        /// </summary>
        /// <param name="bytes">the byte array</param>
        /// <returns>The string</returns>
        public static string ToBase64String(this byte[] bytes)
        {
            
            return Convert.ToBase64String(bytes);
        }


        /// <summary>
        /// Firsts the or default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The x.</param>
        /// <returns>T.</returns>
        public static T FirstOrDefault<T>(this Array x)
        {
            if (x.Length == 0) { return default(T); }
            else
            {
                return (T)x.GetEnumerator().Current;
            }

        }

        /// <summary>
        /// Picks the random.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>T.</returns>
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        /// <summary>
        /// Picks the random.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="count">The count.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        /// <summary>
        /// Shuffles the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }
    }

    
}
