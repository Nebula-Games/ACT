///-------------------------------------------------------------------------------------------------
// file:	Windows\EnvironmentHelper.cs
//
// summary:	Implements the environment helper class
///-------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;


using System.Security.Principal;

using System.Security.Permissions;



namespace ACT.Core.Windows
{
    /// <summary>
    /// Environment Variable Helper Including PATH Directory Management
    /// </summary>   
    public static class EnvironmentHelper
    {

        const int HWND_BROADCAST = 0xffff;
        const uint WM_SETTINGCHANGE = 0x001a;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg,
            UIntPtr wParam, string lParam);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public static void AddPathDirectory(string DirectoryToAdd, string CleanupMatch, bool Cleanup = true)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) { }
            else
            {
                using (var envKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(
                 @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment",
                 true))
                {
                    Contract.Assert(envKey != null, @"registry key is missing!");
                    var _PATHDATA = envKey.GetValue("PATH");
                    SendNotifyMessage((IntPtr)HWND_BROADCAST, WM_SETTINGCHANGE,
                        (UIntPtr)0, "Environment");
                }
            }

        }
    }
}
