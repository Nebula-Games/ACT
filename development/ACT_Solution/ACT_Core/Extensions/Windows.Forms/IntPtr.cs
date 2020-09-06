// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="IntPtr.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// IntPtr Extensions
    /// </summary>
    public static class IntPtrExtensions
    {
#if DOTNETFRAMEWORK
        /// <summary>
        /// GetI32 Wrapper
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>ACT.Core.Windows.I32WindowWrapper.</returns>
        public static ACT.Core.Windows.I32WindowWrapper GetI32(this IntPtr x)
        {
            Windows.I32WindowWrapper _w = new Windows.I32WindowWrapper(x);
            return _w;
        }
#endif
    }
}
