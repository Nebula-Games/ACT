///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Development\I_Code_Logging.cs
//
// summary:	Declares the I_Code_Logging interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Development
{
    /// <summary>
    /// I Code Logging
    /// </summary>
    public interface I_Code_Logging : I_Core
    {
        /// <summary>
        /// Configuration Settings Available
        /// </summary>
        List<(string,string)> AcceptedConfigurationSettings { get; }

        /// <summary>
        /// Initialize with a JSON Object
        /// </summary>
        /// <param name="ConfigurationSettings"></param>
        void Initialize(string ConfigurationSettings);

        /// <summary>
        /// Initialize With ACT System Settings Defined
        /// </summary>
        void Initialize();

        /// <summary>
        /// Create a Log Entry
        /// </summary>
        /// <param name="list"></param>
        void Log(params object[] args);

        /// <summary>
        /// Log Message with Info and optional params
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Info"></param>
        void Log(string Message, string Info);

        /// <summary>
        /// Log Message with Info and optional params
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Info"></param>
        void Log(string Message, params object[] args);
    }
}
