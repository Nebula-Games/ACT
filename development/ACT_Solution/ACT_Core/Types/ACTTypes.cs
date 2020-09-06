// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACTTypes.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;

namespace ACT.Core.Types
{
    /// <summary>
    /// Holds 2 Values a First and Second of Any Type.
    /// Typically used for Numeric Base Types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable()]
    public struct TwoValues<T>
    {
        /// <summary>
        /// First Property
        /// </summary>
        public T First;

        /// <summary>
        /// Second Property
        /// </summary>
        public T Second;
    }

    /// <summary>
    /// Class str.
    /// </summary>
    [Serializable()]
    public class str
    {
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return _CodedString.GetHashCode();
        }
        /// <summary>
        /// The coded string
        /// </summary>
        private string _CodedString = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="str"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public str(string s)
        {
            _CodedString = s;
        }
        /// <summary>
        /// Performs an implicit conversion from <see cref="str"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator string(str s)
        {
            return s._CodedString;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj.ToString().ToLower() == this._CodedString.ToLower())
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(str a, string b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b)) { return true; }
            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null)) { return false; }
            // Return true if the fields match:
            return a._CodedString.ToLower() == b.ToLower();
        }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static str operator +(str a, str b) { return a._CodedString + b._CodedString; }

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static str operator +(str a, string b) { return a._CodedString + b; }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(str a, string b) { return !(a == b); }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.String"/> to <see cref="str"/>.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator str(string s) { return new str(s); }



    }

    /// <summary>
    /// Struct MinMax
    /// </summary>
    [Serializable()]
    public struct MinMax
    {
        /// <summary>
        /// Determines the minimum of the parameters.
        /// </summary>
        public int Min;
        /// <summary>
        /// Determines the maximum of the parameters.
        /// </summary>
        public int Max;
    }

    /// <summary>
    /// Struct AppVersion
    /// </summary>
    [Serializable()]
    public struct AppVersion
    {
        /// <summary>
        /// The major
        /// </summary>
        public int Major;
        /// <summary>
        /// The minor
        /// </summary>
        public int Minor;
        /// <summary>
        /// The patch
        /// </summary>
        public int Patch;

        /// <summary>
        /// Returns A Version String
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return Major.ToString() + "." + Minor.ToString() + "." + Patch.ToString();
        }

        /// <summary>
        /// Generates a AppVersion From "5.5.21"
        /// </summary>
        /// <param name="VersionData">Version As String</param>
        /// <returns>AppVersion.</returns>
        public static AppVersion FromString(string VersionData)
        {
            string[] VersionDataArray = VersionData.SplitString(".", StringSplitOptions.RemoveEmptyEntries);
            AppVersion _TmpReturn = new AppVersion();
            try { _TmpReturn.Major = VersionDataArray[0].ToIntFast(); }
            catch { _TmpReturn.Major = 0; }

            try { _TmpReturn.Minor = VersionDataArray[1].ToIntFast(); }
            catch { _TmpReturn.Minor = 0; }

            try { _TmpReturn.Patch = VersionDataArray[2].ToIntFast(); }
            catch { _TmpReturn.Patch = 0; }

            return _TmpReturn;

        }
    }
}
