///-------------------------------------------------------------------------------------------------
// file:	Windows\Events.cs
//
// summary:	Implements the events class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ACT.Core.Windows
{
    public static class Events
    {
        public enum EventLogLocation
        {
            Application = 1,
            System = 2
        }

        /// <summary>
        /// Saves the Data To The Event Log In Windows
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="EventText"></param>
        /// <param name="EventID"></param>
        public static void WriteToWindowsEventLog(string Source, EventLogLocation Location, string EventText, int EventID)
        {
            try
            {
                if (Location == EventLogLocation.Application)
                {
                    if (!EventLog.SourceExists(Source))
                        EventLog.CreateEventSource(Source, "Application");
                }
                else
                {
                    if (!EventLog.SourceExists(Source))
                        EventLog.CreateEventSource(Source, "System");
                }

                EventLog.WriteEntry(Source, EventText, EventLogEntryType.Warning, EventID);
            }
            catch (Exception ex)
            {
                var _ErrorLoggable = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent();

                _ErrorLoggable.LogError("Error Writing To Event Log", "RegistryKeyError", ex, "", Enums.ErrorLevel.Warning);
            }
        }
    }
}
