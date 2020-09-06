///-------------------------------------------------------------------------------------------------
// file:	BuiltInPlugins\Serialization\ACT_MixedMode_Cache.cs
//
// summary:	Implements the act mixed mode cache class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums;
using ACT.Core.Enums.Serialization;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;

namespace ACT.Plugins.Serialization
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   ACT Cache Plugin. </summary>
    ///
    /// <remarks>   Mark Alicz, 7/20/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ACT_MixedMode_Cache : ACT.Plugins.ACT_Core, ACT.Core.Interfaces.Serialization.I_CacheEngine
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   In Memory Usage. </summary>
        ///
        /// <value> True if in memory, false if not. </value>
        ///-------------------------------------------------------------------------------------------------
        public bool InMemory { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Use a Database for Storing Cache Overflow and Saved Processing. </summary>
        ///
        /// <value> True if use database, false if not. </value>
        ///-------------------------------------------------------------------------------------------------
        public bool UseDatabase { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Use the File System To Save The Cache. </summary>
        ///
        /// <value> True if use file system, false if not. </value>
        ///-------------------------------------------------------------------------------------------------
        public bool UseFileSystem { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Size of the Cache in Bytes before the data is saved to the database. </summary>
        ///
        /// <value> The cache size overflow. </value>
        ///-------------------------------------------------------------------------------------------------
        public int CacheSizeOverflow { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Compress the Data. </summary>
        ///
        /// <value> True if compress data, false if not. </value>
        ///-------------------------------------------------------------------------------------------------
        public bool CompressData { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Number of Seconds to Wait Till Compressing The Data 0 Means Always Compress.
        /// </summary>
        ///
        /// <value> The compress data after no access. </value>
        ///-------------------------------------------------------------------------------------------------
        public int CompressDataAfterNoAccess { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   FileName Pattern. </summary>
        ///
        /// <value> The file name pattern. </value>
        ///-------------------------------------------------------------------------------------------------
        public string FileNamePattern { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   ACT Connection Name. </summary>
        ///
        /// <value> The name of the database connection. </value>
        ///-------------------------------------------------------------------------------------------------
        public string DatabaseConnectionName { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Current Cache State. </summary>
        ///
        /// <value> The cache state. </value>
        ///-------------------------------------------------------------------------------------------------
        public CacheEngineState CacheState => throw new NotImplementedException();


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Returns system setting requirements. </summary>
        ///
        /// <remarks>   Mark Alicz, 7/20/2019. </remarks>
        ///
        /// <returns>   The system setting requirements. </returns>
        ///-------------------------------------------------------------------------------------------------

        public override List<string> ReturnSystemSettingRequirements()
        {
            throw new NotImplementedException();
        }


    }
}
