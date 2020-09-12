///-------------------------------------------------------------------------------------------------
// file:	Windows\I32WindowWrapper.cs
//
// summary:	Declares the I32WindowWrapper interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACT.Core.Windows
{
#if DOTNETFRAMEWORK
    /// <summary>
    /// Window Wrapper
    /// </summary>
    public class I32WindowWrapper : System.Windows.Forms.IWin32Window
    {

        /// <summary>
        /// Constructor With IntPTR
        /// </summary>
        /// <param name="handle"></param>
        public I32WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }
        
        /// <summary>
        /// Handle
        /// </summary>
        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
#endif
}
