// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="EnumerableExtensions.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Types;
using ACT.Core.Helper;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class EnumerableExtensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Iterates through a generic list type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>([NotNull] this IEnumerable<T> list, [NotNull] Action<T> action)
        {
          //  CodeContracts.VerifyNotNull(list, "list");
         //   CodeContracts.VerifyNotNull(action, "action");

            foreach (var item in list.ToList())
            {
                action(item);
            }
        }

        /// <summary>
        /// Iterates through a list with a isFirst flag.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        public static void ForEachFirst<T>([NotNull] this IEnumerable<T> list, [NotNull] Action<T, bool> action)
        {
           // CodeContracts.VerifyNotNull(list, "list");
          //  CodeContracts.VerifyNotNull(action, "action");

            bool isFirst = true;
            foreach (var item in list.ToList())
            {
                action(item, isFirst);
                isFirst = false;
            }
        }

        /// <summary>
        /// Iterates through a list with a index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="action">The action.</param>
        public static void ForEachIndex<T>([NotNull] this IEnumerable<T> list, [NotNull] Action<T, int> action)
        {
          //  CodeContracts.VerifyNotNull(list, "list");
          //  CodeContracts.VerifyNotNull(action, "action");

            int i = 0;
            foreach (var item in list.ToList())
            {
                action(item, i++);
            }
        }

        /// <summary>
        /// If the <paramref name="currentEnumerable" /> is <see langword="null" /> , an Empty IEnumerable of <typeparamref name="T" /> is returned, else <paramref name="currentEnumerable" /> is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentEnumerable">The current enumerable.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> IfNullEmpty<T>([CanBeNull] this IEnumerable<T> currentEnumerable)
        {
            if (currentEnumerable == null)
            {
                return Enumerable.Empty<T>();
            }

            return currentEnumerable;
        }

        /// <summary>
        /// Creates an infinite IEnumerable from the <paramref name="currentEnumerable" /> padding it with default( <typeparamref name="T" /> ).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentEnumerable">The current enumerable.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<T> Infinite<T>([NotNull] this IEnumerable<T> currentEnumerable)
        {
            //CodeContracts.VerifyNotNull(currentEnumerable, "currentEnumerable");

            foreach (var item in currentEnumerable)
            {
                yield return item;
            }

            while (true)
            {
                yield return default(T);
            }
        }

        /// <summary>
        /// Converts an <see cref="IEnumerable" /> to a HashSet -- similar to ToList()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>HashSet&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        [NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> list)
        {
           // CodeContracts.VerifyNotNull(list, "list");

            return new HashSet<T>(list);
        }

        #endregion
    }
}
