// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Super_Types.cs" company="Stonegate Intel LLC">
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

namespace System
{
    /// <summary>
    /// Class BitStorage.
    /// </summary>
    public class BitStorage
    {
        /// <summary>
        /// The names
        /// </summary>
        private string[] Names = new string[16] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        /// <summary>
        /// Actual Value
        /// </summary>
        public ushort value;

        /// <summary>
        /// Set Names Based on Array of Data
        /// </summary>
        /// <param name="NamesArray">The names array.</param>
        /// <exception cref="Exception">Name Array Too Long</exception>
        public void SetNames(string[] NamesArray)
        {
            if (Names.Length > 16) { throw new Exception("Name Array Too Long"); }
            for (int x = 0; x < NamesArray.Length; x++) { Names[x] = NamesArray[x]; }
        }

        /// <summary>
        /// BitStorage Constructor
        /// </summary>
        /// <param name="Data">The data.</param>
        public BitStorage(List<string> Data)
        {
            int _Pos = 0;
            foreach (string dta in Data)
            {
                var _d = dta.SplitString(",", StringSplitOptions.RemoveEmptyEntries);

                if (_d.Length != 2) { continue; }
                Names[_Pos] = _d[0];
                value = (ushort)(value | GetPositionValue(_Pos));
            }

        }

        /// <summary>
        /// Get Position Value
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>System.UInt16.</returns>
        public ushort GetPositionValue(int pos)
        {
            return Convert.ToUInt16(Math.Pow(2, pos));
        }

        /// <summary>
        /// IsTrue
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns><c>true</c> if the specified name is true; otherwise, <c>false</c>.</returns>
        public bool IsTrue(string Name)
        {
            int _v = -1;
            for (int x = 0; x < Names.Length; x++) { if (Names[x] == Name) { _v = x; break; } }
            if (_v == -1) { return false; }
            if ((value & GetPositionValue(_v)) == GetPositionValue(_v)) { return true; }
            return false;
        }

        /// <summary>
        /// Clear the Value
        /// </summary>
        public void Clear() { value = 0; return; }

        /// <summary>
        /// BitStorage Implicit Operator
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator BitStorage(string[] Data)
        {
            // While not technically a requirement; see below why this is done.
            if (Data == null) { return null; }

            return new BitStorage(Data.ToList());
        }

        /// <summary>
        /// Equal Operator
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(BitStorage a, BitStorage b) { if (a.value == b.value) { return true; } else { return false; } }

        /// <summary>
        /// Not Equal Operator
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(BitStorage a, BitStorage b) { return !(a == b); }

        /// <summary>
        /// Get Hash Code
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() { return value.GetHashCode(); }

        /// <summary>
        /// Equals Override
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BitStorage)
            {
                return (obj as BitStorage).value == value;
            }
            else
            {
                return false;
            }
        }
    }
}
