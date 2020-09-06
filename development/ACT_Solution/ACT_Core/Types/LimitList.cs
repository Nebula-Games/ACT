// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="LimitList.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ACT.Core.Extensions;

namespace ACT.Core.Types
{
    /// <summary>
    /// Class LimitList.
    /// Implements the <see cref="System.Collections.Generic.List{System.String}" />
    /// </summary>
    /// <seealso cref="System.Collections.Generic.List{System.String}" />
    public class LimitList : List<string>
    {
        /// <summary>
        /// Gets or sets the maximum capacity.
        /// </summary>
        /// <value>The maximum capacity.</value>
        public int MaximumCapacity { get; set; }
        /// <summary>
        /// Gets or sets the maximum length of the combined.
        /// </summary>
        /// <value>The maximum length of the combined.</value>
        public long MaximumCombinedLength { get; set; }

        /// <summary>
        /// The combined length
        /// </summary>
        private long _CombinedLength = 0;


        /// <summary>
        /// Initializes a new instance of the <see cref="LimitList"/> class.
        /// </summary>
        public LimitList() { MaximumCapacity = 1000; MaximumCombinedLength = 0; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitList"/> class.
        /// </summary>
        /// <param name="MaxCapacity">The maximum capacity.</param>
        public LimitList(int MaxCapacity) { MaximumCapacity = MaxCapacity; MaximumCombinedLength = 0; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LimitList"/> class.
        /// </summary>
        /// <param name="MaxCombinedLength">Maximum length of the combined.</param>
        public LimitList(long MaxCombinedLength) { MaximumCombinedLength = MaxCombinedLength; MaximumCapacity = 0; }

        /// <summary>
        /// Removes the till it will fit.
        /// </summary>
        /// <param name="NewSize">Creates new size.</param>
        private void RemoveTillItWillFit(int NewSize)
        {
            int _TSize = this[0].Length;
            if (NewSize > _TSize) { this.RemoveAt(0); return; }
            else { this.RemoveAt(0); RemoveTillItWillFit(NewSize - _TSize); }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="Collection">The collection.</param>
        /// <exception cref="Exception">Not Implemented</exception>
        public new void AddRange(IEnumerable<string> Collection) { throw new Exception("Not Implemented"); }
        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="Item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">Not Implemented</exception>
        public new bool Remove(string Item) { throw new Exception("Not Implemented"); }
        /// <summary>
        /// Removes the range.
        /// </summary>
        /// <param name="Index">The index.</param>
        /// <param name="Count">The count.</param>
        /// <exception cref="Exception">Not Implemented</exception>
        public new void RemoveRange(int Index, int Count) { throw new Exception("Not Implemented"); }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <param name="Index">The index.</param>
        public new void RemoveAt(int Index)
        {
            if (this.Count() >= Index) { return; }

            int _TSize = this[0].Length;
            _CombinedLength -= _TSize;
            base.RemoveAt(Index);
        }

        /// <summary>
        /// Adds the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        public new void Add(string obj)
        {
            if (MaximumCombinedLength != 0)
            {
                if (_CombinedLength + obj.Length > MaximumCombinedLength)
                {
                    RemoveTillItWillFit(obj.Length);
                }
            }
            else if (MaximumCapacity > 0)
            {
                if (this.Count() >= MaximumCapacity)
                {
                    this.RemoveAt(0);
                }
            }
            
            base.Add(obj);
        }
    }
}
