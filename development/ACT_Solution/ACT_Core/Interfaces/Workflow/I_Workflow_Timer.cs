using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Workflow
{
    public interface I_Workflow_Timer
    {
        event Delegates.OnComplete OnWorkflowTick;
        int MilliSecondInterval { get; set; }
        dynamic ConfigurationSettings { get; set; }
        bool LoadConfiguration(string JSONData);
        Interfaces.Common.I_ErrorLoggable ErrorLogger { get; set; }
    }
}
