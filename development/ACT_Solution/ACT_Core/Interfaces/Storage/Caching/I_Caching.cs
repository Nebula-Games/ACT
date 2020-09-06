///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Storage\Caching\I_Caching.cs
//
// summary:	Declares the I_Caching interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Storage.Caching
{
    /// <summary>
    /// Allows for the CACHING of Any Data
    /// </summary>
    public interface I_Caching : Interfaces.Common.I_Plugin, Common.I_Configurable
    {
        /// <summary>
        /// Allow the Cache to Save the data to a disk or database for quick recovery.
        /// </summary>
        bool EnsureStateRecovery { get; set; }

        /// <summary>
        /// State Recovery Level - Level at which the state is maintained.
        /// </summary>
        int StateRecoveryLevel { get; set; }

        /// <summary>
        /// Holds the Data
        /// </summary>
        Dictionary<string,dynamic> DATA { get; }

        /// <summary>
        /// Add Data To CACHE
        /// </summary>
        /// <param name="key">String Key</param>
        /// <param name="value">Data to Add To Cache</param>
        /// <returns></returns>
        Enums.Common.BasicMethodReturn Add(string key, dynamic value);

    }
}
