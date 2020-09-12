///-------------------------------------------------------------------------------------------------
// file:	Windows\Kernel32\kernel32.cs
//
// summary:	Implements the kernel 32 class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ACT.Core.Windows.Kernel32
{
    public static class ExternMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
    }
}
