///-------------------------------------------------------------------------------------------------
// file:	Enums\Common\MethodResult_Enums.cs
//
// summary:	Implements the method result enums class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.Common
{
    /// <summary>
    /// REsult of Executing a Method
    /// </summary>
    public enum BasicMethodReturn
    {
        Success = 1,
        Failed = 2,
        InvalidConfiguration = 3
    }
}
