// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Service_Enums.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Enums.Windows
{

    /// <summary>
    /// Enum ServiceState
    /// </summary>
    public enum ServiceState
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown = -1, // The state cannot be (has not been) retrieved.
        /// <summary>
        /// The not found
        /// </summary>
        NotFound = 0, // The service is not known on the host server.
        /// <summary>
        /// The stopped
        /// </summary>
        Stopped = 1,
        /// <summary>
        /// The start pending
        /// </summary>
        StartPending = 2,
        /// <summary>
        /// The stop pending
        /// </summary>
        StopPending = 3,
        /// <summary>
        /// The running
        /// </summary>
        Running = 4,
        /// <summary>
        /// The continue pending
        /// </summary>
        ContinuePending = 5,
        /// <summary>
        /// The pause pending
        /// </summary>
        PausePending = 6,
        /// <summary>
        /// The paused
        /// </summary>
        Paused = 7
    }

    /// <summary>
    /// Enum ScmAccessRights
    /// </summary>
    [Flags]
    public enum ScmAccessRights
    {
        /// <summary>
        /// The connect
        /// </summary>
        Connect = 0x0001,
        /// <summary>
        /// The create service
        /// </summary>
        CreateService = 0x0002,
        /// <summary>
        /// The enumerate service
        /// </summary>
        EnumerateService = 0x0004,
        /// <summary>
        /// The lock
        /// </summary>
        Lock = 0x0008,
        /// <summary>
        /// The query lock status
        /// </summary>
        QueryLockStatus = 0x0010,
        /// <summary>
        /// The modify boot configuration
        /// </summary>
        ModifyBootConfig = 0x0020,
        /// <summary>
        /// The standard rights required
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// All access
        /// </summary>
        AllAccess = (StandardRightsRequired | Connect | CreateService |
                     EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
    }

    /// <summary>
    /// Enum ServiceAccessRights
    /// </summary>
    [Flags]
    public enum ServiceAccessRights
    {
        /// <summary>
        /// The query configuration
        /// </summary>
        QueryConfig = 0x1,
        /// <summary>
        /// The change configuration
        /// </summary>
        ChangeConfig = 0x2,
        /// <summary>
        /// The query status
        /// </summary>
        QueryStatus = 0x4,
        /// <summary>
        /// The enumerate dependants
        /// </summary>
        EnumerateDependants = 0x8,
        /// <summary>
        /// The start
        /// </summary>
        Start = 0x10,
        /// <summary>
        /// The stop
        /// </summary>
        Stop = 0x20,
        /// <summary>
        /// The pause continue
        /// </summary>
        PauseContinue = 0x40,
        /// <summary>
        /// The interrogate
        /// </summary>
        Interrogate = 0x80,
        /// <summary>
        /// The user defined control
        /// </summary>
        UserDefinedControl = 0x100,
        /// <summary>
        /// The delete
        /// </summary>
        Delete = 0x00010000,
        /// <summary>
        /// The standard rights required
        /// </summary>
        StandardRightsRequired = 0xF0000,
        /// <summary>
        /// All access
        /// </summary>
        AllAccess = (StandardRightsRequired | QueryConfig | ChangeConfig |
                     QueryStatus | EnumerateDependants | Start | Stop | PauseContinue |
                     Interrogate | UserDefinedControl)
    }

    /// <summary>
    /// Enum ServiceBootFlag
    /// </summary>
    public enum ServiceBootFlag
    {
        /// <summary>
        /// The start
        /// </summary>
        Start = 0x00000000,
        /// <summary>
        /// The system start
        /// </summary>
        SystemStart = 0x00000001,
        /// <summary>
        /// The automatic start
        /// </summary>
        AutoStart = 0x00000002,
        /// <summary>
        /// The demand start
        /// </summary>
        DemandStart = 0x00000003,
        /// <summary>
        /// The disabled
        /// </summary>
        Disabled = 0x00000004
    }

    /// <summary>
    /// Enum ServiceControl
    /// </summary>
    public enum ServiceControl
    {
        /// <summary>
        /// The stop
        /// </summary>
        Stop = 0x00000001,
        /// <summary>
        /// The pause
        /// </summary>
        Pause = 0x00000002,
        /// <summary>
        /// The continue
        /// </summary>
        Continue = 0x00000003,
        /// <summary>
        /// The interrogate
        /// </summary>
        Interrogate = 0x00000004,
        /// <summary>
        /// The shutdown
        /// </summary>
        Shutdown = 0x00000005,
        /// <summary>
        /// The parameter change
        /// </summary>
        ParamChange = 0x00000006,
        /// <summary>
        /// The net bind add
        /// </summary>
        NetBindAdd = 0x00000007,
        /// <summary>
        /// The net bind remove
        /// </summary>
        NetBindRemove = 0x00000008,
        /// <summary>
        /// The net bind enable
        /// </summary>
        NetBindEnable = 0x00000009,
        /// <summary>
        /// The net bind disable
        /// </summary>
        NetBindDisable = 0x0000000A
    }

    /// <summary>
    /// Enum ServiceError
    /// </summary>
    public enum ServiceError
    {
        /// <summary>
        /// The ignore
        /// </summary>
        Ignore = 0x00000000,
        /// <summary>
        /// The normal
        /// </summary>
        Normal = 0x00000001,
        /// <summary>
        /// The severe
        /// </summary>
        Severe = 0x00000002,
        /// <summary>
        /// The critical
        /// </summary>
        Critical = 0x00000003
    }
}
