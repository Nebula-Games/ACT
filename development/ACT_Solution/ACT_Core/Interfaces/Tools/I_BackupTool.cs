using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Tools
{
    public interface I_BackupTool
    {

        string Name { get; set; }
        DateTime LastBackupPoint { get; set; }
        
        List<string> BaseDirectories { get; }
        List<ACT.Core.Types.IO.ACT_Directory> BaseDirectoriesObject { get; }

        void AddDirectory(string RootPath);

        void AddSchedule(ACT.Core.Types.Common.ACT_ScheduleData ScheduleData);


    }
}
