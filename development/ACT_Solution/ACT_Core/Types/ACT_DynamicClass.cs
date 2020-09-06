// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="ACT_DynamicClass.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ACT.Core.Types
{
    /// <summary>
    /// Class ACT_DynamicClass.
    /// </summary>
    public class ACT_DynamicClass
    {
        /// <summary>
        /// Occurs when [on class changed].
        /// </summary>
        public event ACT.Core.Delegates.OnChanged OnClassChanged;

        /// <summary>
        /// The dynamic class hash
        /// </summary>
        private int _DynamicClassHash;
        /// <summary>
        /// Calculates the class hash.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int CalculateClassHash()
        {
            var _type = this.GetType();
            var _Properties = _type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            int _tmpHash = ACT.Core.Helper.RandomHelper.Random_Helper.GenerateRandomNumber(10000, 1000000);
            foreach(var property in _Properties)
            {
                _tmpHash = _tmpHash ^ property.GetHashCode();
                _tmpHash = _tmpHash + property.GetValue(this).ToString().Length;                    
            }
            return _tmpHash;
        }
        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value><c>true</c> if this instance has changed; otherwise, <c>false</c>.</value>
        public bool HasChanged
        {
            get
            {
                int _tmp = CalculateClassHash();
                if (_DynamicClassHash == _tmp)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
