﻿///-------------------------------------------------------------------------------------------------
// file:	Windows\Services\WindowsServices.cs
//
// summary:	Implements the windows services class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using ACT.Core.Enums.Windows;

namespace ACT.Core.Windows.Services
{
    /// <summary>
    /// Windows Services Helper Methods
    /// </summary>
    public static class WindowsServices
    {

        public static class ServiceInstaller
        {
            private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
            private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;

            [StructLayout(LayoutKind.Sequential)]
            private class SERVICE_STATUS
            {
                public int dwServiceType = 0;
                public ServiceState dwCurrentState = 0;
                public int dwControlsAccepted = 0;
                public int dwWin32ExitCode = 0;
                public int dwServiceSpecificExitCode = 0;
                public int dwCheckPoint = 0;
                public int dwWaitHint = 0;
            }

            #region OpenSCManager
            [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
            static extern IntPtr OpenSCManager(string machineName, string databaseName, ScmAccessRights dwDesiredAccess);
            #endregion

            #region OpenService
            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceAccessRights dwDesiredAccess);
            #endregion

            #region CreateService
            [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceAccessRights dwDesiredAccess, int dwServiceType, ServiceBootFlag dwStartType, ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);
            #endregion

            #region CloseServiceHandle
            [DllImport("advapi32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool CloseServiceHandle(IntPtr hSCObject);
            #endregion

            #region QueryServiceStatus
            [DllImport("advapi32.dll")]
            private static extern int QueryServiceStatus(IntPtr hService, SERVICE_STATUS lpServiceStatus);
            #endregion

            #region DeleteService
            [DllImport("advapi32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool DeleteService(IntPtr hService);
            #endregion

            #region ControlService
            [DllImport("advapi32.dll")]
            private static extern int ControlService(IntPtr hService, ServiceControl dwControl, SERVICE_STATUS lpServiceStatus);
            #endregion

            #region StartService
            [DllImport("advapi32.dll", SetLastError = true)]
            private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);
            #endregion

            /// <summary>
            /// Uninstall a Windows Service By Name
            /// </summary>
            /// <param name="serviceName"></param>
            public static void Uninstall(string serviceName)
            {
                IntPtr scm = OpenSCManager(ScmAccessRights.AllAccess);

                try
                {
                    IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.AllAccess);
                    if (service == IntPtr.Zero)
                        throw new ApplicationException("Service not installed.");

                    try
                    {
                        StopService(service);
                        if (!DeleteService(service))
                            throw new ApplicationException("Could not delete service " + Marshal.GetLastWin32Error());
                    }
                    finally
                    {
                        CloseServiceHandle(service);
                    }
                }
                finally
                {
                    CloseServiceHandle(scm);
                }
            }

            /// <summary>
            /// Test if a Services is installed by Name
            /// </summary>
            /// <param name="serviceName"></param>
            /// <returns></returns>
            public static bool ServiceIsInstalled(string serviceName)
            {
                IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

                try
                {
                    IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus);

                    if (service == IntPtr.Zero)
                        return false;

                    CloseServiceHandle(service);
                    return true;
                }
                finally
                {
                    CloseServiceHandle(scm);
                }
            }

            //[PrincipalPermission(SecurityAction.Demand, Role = "Administrators")]
            /// <summary>
            /// Install the Service
            /// This method will only work if the assembly is executing under elevated permissions
            /// </summary>
            /// <param name="serviceName"></param>
            /// <param name="displayName"></param>
            /// <param name="fileName"></param>
            public static void InstallAndStart(string serviceName, string displayName, string fileName)
            {
                IntPtr scm = OpenSCManager(ScmAccessRights.AllAccess);

                try
                {
                    IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.AllAccess);

                    if (service == IntPtr.Zero)
                        service = CreateService(scm, serviceName, displayName, ServiceAccessRights.AllAccess, SERVICE_WIN32_OWN_PROCESS, ServiceBootFlag.AutoStart, ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);

                    if (service == IntPtr.Zero)
                        throw new ApplicationException("Failed to install service.");

                    try
                    {
                        StartService(service);
                    }
                    finally
                    {
                        CloseServiceHandle(service);
                    }
                }
                finally
                {
                    CloseServiceHandle(scm);
                }
            }

            /// <summary>
            /// Start the Service
            /// </summary>
            /// <param name="serviceName"></param>
            public static void StartService(string serviceName)
            {
                IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

                try
                {
                    IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus | ServiceAccessRights.Start);
                    if (service == IntPtr.Zero)
                        throw new ApplicationException("Could not open service.");

                    try
                    {
                        StartService(service);
                    }
                    finally
                    {
                        CloseServiceHandle(service);
                    }
                }
                finally
                {
                    CloseServiceHandle(scm);
                }
            }

            /// <summary>
            /// Stop The Service
            /// </summary>
            /// <param name="serviceName"></param>
            public static void StopService(string serviceName)
            {
                IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

                try
                {
                    IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus | ServiceAccessRights.Stop);
                    if (service == IntPtr.Zero)
                        throw new ApplicationException("Could not open service.");

                    try
                    {
                        StopService(service);
                    }
                    finally
                    {
                        CloseServiceHandle(service);
                    }
                }
                finally
                {
                    CloseServiceHandle(scm);
                }
            }

            private static void StartService(IntPtr service)
            {
                SERVICE_STATUS status = new SERVICE_STATUS();
                StartService(service, 0, 0);
                var changedStatus = WaitForServiceStatus(service, ServiceState.StartPending, ServiceState.Running);
                if (!changedStatus)
                    throw new ApplicationException("Unable to start service");
            }

            private static void StopService(IntPtr service)
            {
                SERVICE_STATUS status = new SERVICE_STATUS();
                ControlService(service, ServiceControl.Stop, status);
                var changedStatus = WaitForServiceStatus(service, ServiceState.StopPending, ServiceState.Stopped);
                if (!changedStatus)
                    throw new ApplicationException("Unable to stop service");
            }

            /// <summary>
            /// Get the Service Status
            /// </summary>
            /// <param name="serviceName"></param>
            /// <returns></returns>
            public static ServiceState GetServiceStatus(string serviceName)
            {
                IntPtr scm = OpenSCManager(ScmAccessRights.Connect);

                try
                {
                    IntPtr service = OpenService(scm, serviceName, ServiceAccessRights.QueryStatus);
                    if (service == IntPtr.Zero)
                        return ServiceState.NotFound;

                    try
                    {
                        return GetServiceStatus(service);
                    }
                    finally
                    {
                        CloseServiceHandle(service);
                    }
                }
                finally
                {
                    CloseServiceHandle(scm);
                }
            }

            private static ServiceState GetServiceStatus(IntPtr service)
            {
                SERVICE_STATUS status = new SERVICE_STATUS();

                if (QueryServiceStatus(service, status) == 0)
                    throw new ApplicationException("Failed to query service status.");

                return status.dwCurrentState;
            }

            private static bool WaitForServiceStatus(IntPtr service, ServiceState waitStatus, ServiceState desiredStatus)
            {
                SERVICE_STATUS status = new SERVICE_STATUS();

                QueryServiceStatus(service, status);
                if (status.dwCurrentState == desiredStatus) return true;

                int dwStartTickCount = Environment.TickCount;
                int dwOldCheckPoint = status.dwCheckPoint;

                while (status.dwCurrentState == waitStatus)
                {
                    // Do not wait longer than the wait hint. A good interval is
                    // one tenth the wait hint, but no less than 1 second and no
                    // more than 10 seconds.

                    int dwWaitTime = status.dwWaitHint / 10;

                    if (dwWaitTime < 1000) dwWaitTime = 1000;
                    else if (dwWaitTime > 10000) dwWaitTime = 10000;

                    System.Threading.Thread.Sleep(dwWaitTime);

                    // Check the status again.

                    if (QueryServiceStatus(service, status) == 0) break;

                    if (status.dwCheckPoint > dwOldCheckPoint)
                    {
                        // The service is making progress.
                        dwStartTickCount = Environment.TickCount;
                        dwOldCheckPoint = status.dwCheckPoint;
                    }
                    else
                    {
                        if (Environment.TickCount - dwStartTickCount > status.dwWaitHint)
                        {
                            // No progress made within the wait hint
                            break;
                        }
                    }
                }
                return (status.dwCurrentState == desiredStatus);
            }

            private static IntPtr OpenSCManager(ScmAccessRights rights)
            {
                IntPtr scm = OpenSCManager(null, null, rights);
                if (scm == IntPtr.Zero)
                    throw new ApplicationException("Could not connect to service control manager.");

                return scm;
            }
        }


    }


}
