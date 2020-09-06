// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Stream.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACT.Core.Extensions
{
#if NETSTANDARD
    public static class StreamExtensions
    {

        public static void Close(this Stream stream)
        {
            stream.Dispose();
            GC.SuppressFinalize(stream);

        }



    }
#endif
}
