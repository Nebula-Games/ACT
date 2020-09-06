///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Serialization\I_CacheEngine.cs
//
// summary:	Declares the I_CacheEngine interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Serialization
{
    /// <summary>
    /// A CACHING ENGINE
    /// </summary>
    public interface I_CacheEngine : Common.I_Plugin
    {
        /// <summary>
        /// Only use InMemory Loses the State When Object is Recycled
        /// </summary>
        bool InMemory { get; set; }

        /// <summary>
        /// Use a Database for Storing Cache Overflow and Saved Processing
        /// </summary>
        bool UseDatabase { get; set; }

        /// <summary>
        /// Use the File System To Save The Cache
        /// </summary>
        bool UseFileSystem { get; set; }

        /// <summary>
        /// Size of the Cache in Bytes before the data is saved to the database
        /// </summary>
        int CacheSizeOverflow { get; set; }

        /// <summary>
        /// Compress the Data
        /// </summary>
        bool CompressData { get; set; }
        
        /// <summary>
        /// Number of Seconds to Wait Till Compressing The Data 0 Means Always Compress
        /// </summary>
        int CompressDataAfterNoAccess { get; set; }

        /// <summary>
        /// FileName Pattern
        /// </summary>
        string FileNamePattern { get; set; }

        /// <summary>
        /// ACT Connection Name
        /// </summary>
        string DatabaseConnectionName { get; set; }

        /// <summary>
        /// Current Cache State
        /// </summary>
        Enums.Serialization.CacheEngineState CacheState { get; }
    }
}
