using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ACT.Core.Interfaces.Application
{
    public interface I_ApplicationManager
    {
        Types.SystemConfiguration.Application ApplicationData { get; set; }

        void LoadIntoMemory(IsolationLevel Lvl);

        void Unload();

        void SilentAlarm();

    }
}
