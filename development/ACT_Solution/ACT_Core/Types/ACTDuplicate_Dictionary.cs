// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACTDuplicate_Dictionary.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types
{
    /// <summary>
    /// Class ACTDuplicate_Dictionary.
    /// Implements the <see cref="System.Collections.Generic.Dictionary{TKey, System.Collections.Generic.List{TValue}}" />
    /// Implements the <see cref="System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{TKey, TValue}}" />
    /// </summary>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <typeparam name="TValue">The type of the t value.</typeparam>
    /// <seealso cref="System.Collections.Generic.Dictionary{TKey, System.Collections.Generic.List{TValue}}" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{TKey, TValue}}" />
    public class ACTDuplicate_Dictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets or sets the <see cref="IEnumerable{KeyValuePair{TKey, TValue}}"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;.</returns>
        public new IEnumerable<KeyValuePair<TKey, TValue>> this[TKey key]
        {
            get
            {
                List<TValue> values;
                if (!TryGetValue(key, out values))
                {
                    return Enumerable.Empty<KeyValuePair<TKey, TValue>>();
                }

                return values.Select(v => new KeyValuePair<TKey, TValue>(key, v));
            }
            set
            {
                foreach (var _value in value.Select(kvp => kvp.Value))
                {
                    Add(key, _value);
                }
            }
        }



        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(TKey key, TValue value)
        {
            List<TValue> values;
            if (!TryGetValue(key, out values))
            {
                values = new List<TValue>();
                Add(key, values);
            }
            values.Add(value);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>IEnumerator&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;.</returns>
        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in ((Dictionary<TKey, List<TValue>>)this))
            {
                foreach (var value in item.Value)
                {
                    yield return new KeyValuePair<TKey, TValue>(item.Key, value);
                }
            }
        }
    }
}
