using ACT.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ACT.Plugins.Common
{
    public class NLog_ErrorLoggeing : ACT.Core.Interfaces.Common.I_ErrorLoggable
    {
        public static class NLogStaticData
        {
            public static bool NLogLoadedCorrectly = false;

            static NLogStaticData()
            {
                /*
                string assemblyFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var _LogConfig = new NLog.LogFactory();                
                NLog.LogManager.Configuration = new NLog.Config.LoggingConfiguration()
                */
            }
        }
        public void LogError(string className, string summary, Exception ex, string additionInformation, ErrorLevel errorType)
        {
            // CHECK FOR NLOG CONFIGURATION FILE
            // NLog.Config.LoggingConfigurationParser _P = new 
        }
    }
}
