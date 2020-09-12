///-------------------------------------------------------------------------------------------------
// file:	Windows\Controls\ACT_ControlHelpers.cs
//
// summary:	Implements the act control helpers class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;



namespace ACT.Core.Windows.Controls
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   A window 32. </summary>
    ///
    /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class Win32
    {
        // Constants for sending and receiving messages in BrowseCallBackProc
        /// <summary>   The windows message user. </summary>
        public const int WM_USER = 0x400;
        /// <summary>   The windows message getfont. </summary>
        public const int WM_GETFONT = 0x0031;

        /// <summary>   Full pathname of the maximum file. </summary>
        public const int MAX_PATH = 260;

        /// <summary>   The bffm initialized. </summary>
        public const int BFFM_INITIALIZED = 1;
        /// <summary>   The bffm selchanged. </summary>
        public const int BFFM_SELCHANGED = 2;
        /// <summary>   The bffm validatefaileda. </summary>
        public const int BFFM_VALIDATEFAILEDA = 3;
        /// <summary>   The bffm validatefailedw. </summary>
        public const int BFFM_VALIDATEFAILEDW = 4;
        /// <summary>   provides IUnknown to client. lParam: IUnknown*. </summary>
        public const int BFFM_IUNKNOWN = 5;
        /// <summary>   The bffm setstatustexta. </summary>
        public const int BFFM_SETSTATUSTEXTA = WM_USER + 100;
        /// <summary>   The bffm enableok. </summary>
        public const int BFFM_ENABLEOK = WM_USER + 101;
        /// <summary>   The bffm setselectiona. </summary>
        public const int BFFM_SETSELECTIONA = WM_USER + 102;
        /// <summary>   The bffm setselectionw. </summary>
        public const int BFFM_SETSELECTIONW = WM_USER + 103;
        /// <summary>   The bffm setstatustextw. </summary>
        public const int BFFM_SETSTATUSTEXTW = WM_USER + 104;
        /// <summary>   Unicode only. </summary>
        public const int BFFM_SETOKTEXT = WM_USER + 105;
        /// <summary>   Unicode only. </summary>
        public const int BFFM_SETEXPANDED = WM_USER + 106;

        // Browsing for directory.
        /// <summary>   For finding a folder to start document searching. </summary>
        public const uint BIF_RETURNONLYFSDIRS = 0x0001;
        /// <summary>   For starting the Find Computer. </summary>
        public const uint BIF_DONTGOBELOWDOMAIN = 0x0002;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Top of the dialog has 2 lines of text for BROWSEINFO.lpszTitle and one line if.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public const uint BIF_STATUSTEXT = 0x0004;
                                                    // this flag is set.  Passing the message BFFM_SETSTATUSTEXTA to the hwnd can set the
                                                    // rest of the text.  This is not used with BIF_USENEWUI and BROWSEINFO.lpszTitle gets
                                                    // all three lines of text.
        /// <summary>   The bif returnfsancestors. </summary>
        public const uint BIF_RETURNFSANCESTORS = 0x0008;
        /// <summary>   Add an editbox to the dialog. </summary>
        public const uint BIF_EDITBOX = 0x0010;
        /// <summary>   insist on valid result (or CANCEL) </summary>
        public const uint BIF_VALIDATE = 0x0020;

        /// <summary>   Use the new dialog layout with the ability to resize. </summary>
        public const uint BIF_NEWDIALOGSTYLE = 0x0040;
                                                         // Caller needs to call OleInitialize() before using this API
        /// <summary>   (BIF_NEWDIALOGSTYLE | BIF_EDITBOX); </summary>
        public const uint BIF_USENEWUI = 0x0040 + 0x0010;

        /// <summary>   Allow URLs to be displayed or entered. (Requires BIF_USENEWUI) </summary>
        public const uint BIF_BROWSEINCLUDEURLS = 0x0080;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a UA hint to the dialog, in place of the edit box. May not be combined with BIF_EDITBOX.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public const uint BIF_UAHINT = 0x0100;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        /// Do not add the "New Folder" button to the dialog.  Only applicable with BIF_NEWDIALOGSTYLE.
        /// </summary>
        ///-------------------------------------------------------------------------------------------------

        public const uint BIF_NONEWFOLDERBUTTON = 0x0200;
        /// <summary>   don't traverse target as shortcut. </summary>
        public const uint BIF_NOTRANSLATETARGETS = 0x0400;

        /// <summary>   Browsing for Computers. </summary>
        public const uint BIF_BROWSEFORCOMPUTER = 0x1000;
        /// <summary>   Browsing for Printers. </summary>
        public const uint BIF_BROWSEFORPRINTER = 0x2000;
        /// <summary>   Browsing for Everything. </summary>
        public const uint BIF_BROWSEINCLUDEFILES = 0x4000;
        /// <summary>   sharable resources displayed (remote shares, requires BIF_USENEWUI) </summary>
        public const uint BIF_SHAREABLE = 0x8000;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Browse callback procedure. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hwnd"> The hwnd. </param>
        /// <param name="msg">  The message. </param>
        /// <param name="lp">   The pointer to a. </param>
        /// <param name="wp">   The wp. </param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

        public delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lp, IntPtr wp);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A browseinfo. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            /// <summary>   The owner. </summary>
            public IntPtr hwndOwner;
            /// <summary>   The PIDL root. </summary>
            public IntPtr pidlRoot;
            //public IntPtr pszDisplayName;
            /// <summary>   Name of the display. </summary>
            public string pszDisplayName;
            //[MarshalAs(UnmanagedType.LPTStr)]
            /// <summary>   The title. </summary>
            public string lpszTitle;
            /// <summary>   The ul flags. </summary>
            public uint ulFlags;
            /// <summary>   The lpfn. </summary>
            public BrowseCallbackProc lpfn;
            /// <summary>   The parameter. </summary>
            public IntPtr lParam;
            /// <summary>   Zero-based index of the image. </summary>
            public int iImage;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sh browse for folder. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="lpbi"> [in,out] The lpbi. </param>
        ///
        /// <returns>   An IntPtr. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("shell32.dll")]
        public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        // Note that the BROWSEINFO object's pszDisplayName only gives you the name of the folder.
        // To get the actual path, you need to parse the returned PIDL

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sh get path from identifier list. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="pidl">     The PIDL. </param>
        /// <param name="pszPath">  Full pathname of the file. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SHGetPathFromIDList(IntPtr pidl, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszPath);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sh get special folder location. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hwndOwner">    The owner. </param>
        /// <param name="nFolder">      Pathname of the folder. </param>
        /// <param name="ppidl">        [in,out] The ppidl. </param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHGetSpecialFolderLocation(IntPtr hwndOwner, int nFolder, ref IntPtr ppidl);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a message. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="msg">      The message. </param>
        /// <param name="wParam">   The parameter. </param>
        /// <param name="lParam">   The parameter. </param>
        ///
        /// <returns>   An IntPtr. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a message. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="msg">      The message. </param>
        /// <param name="wParam">   The parameter. </param>
        /// <param name="lParam">   The parameter. </param>
        ///
        /// <returns>   An IntPtr. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sends a message. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="Msg">      The message. </param>
        /// <param name="wParam">   The parameter. </param>
        /// <param name="lParam">   The parameter. </param>
        ///
        /// <returns>   An IntPtr. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets window text. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hwnd">     The hwnd. </param>
        /// <param name="lpString"> The string. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets dialog item. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hDlg">         The dialog. </param>
        /// <param name="nIDDlgItem">   The identifier dialog item. </param>
        ///
        /// <returns>   The dialog item. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        /// <summary>   The software hide. </summary>
        public const int SW_HIDE = 0;
        /// <summary>   The software show. </summary>
        public const int SW_SHOW = 5;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Shows the window. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="nCmdShow"> The command show. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);//ShowWindowCommands nCmdShow);



        /// <summary>   The swp asyncwindowpos. </summary>
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        /// <summary>   The swp defererase. </summary>
        public const int SWP_DEFERERASE = 0x2000;
        /// <summary>   The swp drawframe. </summary>
        public const int SWP_DRAWFRAME = 0x0020;
        /// <summary>   The swp framechanged. </summary>
        public const int SWP_FRAMECHANGED = 0x0020;
        /// <summary>   The swp hidewindow. </summary>
        public const int SWP_HIDEWINDOW = 0x0080;
        /// <summary>   The swp noactivate. </summary>
        public const int SWP_NOACTIVATE = 0x0010;
        /// <summary>   The swp nocopybits. </summary>
        public const int SWP_NOCOPYBITS = 0x0100;
        /// <summary>   The swp nomove. </summary>
        public const int SWP_NOMOVE = 0x0002;
        /// <summary>   The swp noownerzorder. </summary>
        public const int SWP_NOOWNERZORDER = 0x0200;
        /// <summary>   The swp noredraw. </summary>
        public const int SWP_NOREDRAW = 0x0008;
        /// <summary>   The swp noreposition. </summary>
        public const int SWP_NOREPOSITION = 0x0200;
        /// <summary>   The swp nosendchanging. </summary>
        public const int SWP_NOSENDCHANGING = 0x0400;
        /// <summary>   The swp nosize. </summary>
        public const int SWP_NOSIZE = 0x0001;
        /// <summary>   The swp nozorder. </summary>
        public const int SWP_NOZORDER = 0x0004;
        /// <summary>   The swp showwindow. </summary>
        public const int SWP_SHOWWINDOW = 0x0040;

        /// <summary>   The top. </summary>
        public const int HWND_TOP = 0;
        /// <summary>   The bottom. </summary>
        public const int HWND_BOTTOM = 1;
        /// <summary>   The topmost. </summary>
        public const int HWND_TOPMOST = -1;
        /// <summary>   The notopmost. </summary>
        public const int HWND_NOTOPMOST = -2;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets window position. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">             The window. </param>
        /// <param name="hWndInsertAfter">  The window insert after. </param>
        /// <param name="x">                The x coordinate. </param>
        /// <param name="y">                The y coordinate. </param>
        /// <param name="cx">               The cx. </param>
        /// <param name="cy">               The cy. </param>
        /// <param name="uFlags">           The flags. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Move window. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="X">        The X coordinate. </param>
        /// <param name="Y">        The Y coordinate. </param>
        /// <param name="nWidth">   The width. </param>
        /// <param name="nHeight">  The height. </param>
        /// <param name="bRepaint"> True to repaint. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Move window. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="rect">     The rectangle. </param>
        /// <param name="bRepaint"> True to repaint. </param>
        ///-------------------------------------------------------------------------------------------------

        public static void MoveWindow(IntPtr hWnd, RECT rect, bool bRepaint)
        {
            MoveWindow(hWnd, rect.Left, rect.Top, rect.Width, rect.Height, bRepaint);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A rectangle. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>   The left. </summary>
            public int Left;
            /// <summary>   The top. </summary>
            public int Top;
            /// <summary>   The right. </summary>
            public int Right;
            /// <summary>   The bottom. </summary>
            public int Bottom;

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Constructor. </summary>
            ///
            /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
            ///
            /// <param name="left">     The left. </param>
            /// <param name="top">      The top. </param>
            /// <param name="width">    The width. </param>
            /// <param name="height">   The height. </param>
            ///-------------------------------------------------------------------------------------------------

            public RECT(int left, int top, int width, int height)
            {
                this.Left = left;
                this.Top = top;
                this.Right = left + width;
                this.Bottom = top + height;
            }

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Gets the height. </summary>
            ///
            /// <value> The height. </value>
            ///-------------------------------------------------------------------------------------------------

            public int Height { get { return this.Bottom - this.Top; } }

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Gets the width. </summary>
            ///
            /// <value> The width. </value>
            ///-------------------------------------------------------------------------------------------------

            public int Width { get { return this.Right - this.Left; } }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets window rectangle. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="lpRect">   [out] The rectangle. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets client rectangle. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="lpRect">   [out] The rectangle. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A point. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>   The X coordinate. </summary>
            public int X;
            /// <summary>   The Y coordinate. </summary>
            public int Y;

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Constructor. </summary>
            ///
            /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
            ///
            /// <param name="x">    The x coordinate. </param>
            /// <param name="y">    The y coordinate. </param>
            ///-------------------------------------------------------------------------------------------------

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Implicit cast that converts the given POINT to a Point. </summary>
            ///
            /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
            ///
            /// <param name="p">    A Point to process. </param>
            ///
            /// <returns>   The result of the operation. </returns>
            ///-------------------------------------------------------------------------------------------------

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            ///-------------------------------------------------------------------------------------------------
            /// <summary>   Implicit cast that converts the given System.Drawing.Point to a POINT. </summary>
            ///
            /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
            ///
            /// <param name="p">    A Point to process. </param>
            ///
            /// <returns>   The result of the operation. </returns>
            ///-------------------------------------------------------------------------------------------------

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Screen to client. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="lpPoint">  [in,out] The point. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Screen to client. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd"> The window. </param>
        /// <param name="rc">   [in,out] The rectangle. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static bool ScreenToClient(IntPtr hWnd, ref RECT rc)
        {
            POINT pt1 = new POINT(rc.Left, rc.Top);
            if (!ScreenToClient(hWnd, ref pt1))
                return false;
            POINT pt2 = new POINT(rc.Right, rc.Bottom);
            if (!ScreenToClient(hWnd, ref pt2))
                return false;

            rc.Left = pt1.X;
            rc.Top = pt1.Y;
            rc.Right = pt2.X;
            rc.Bottom = pt2.Y;

            return true;
        }


        /// <summary>   The gwl wndproc. </summary>
        public static readonly int GWL_WNDPROC = (-4);
        /// <summary>   The gwl hinstance. </summary>
        public static readonly int GWL_HINSTANCE = (-6);
        /// <summary>   The gwl hwndparent. </summary>
        public static readonly int GWL_HWNDPARENT = (-8);
        /// <summary>   The gwl style. </summary>
        public static readonly int GWL_STYLE = (-16);
        /// <summary>   The gwl exstyle. </summary>
        public static readonly int GWL_EXSTYLE = (-20);
        /// <summary>   The gwl userdata. </summary>
        public static readonly int GWL_USERDATA = (-21);
        /// <summary>   Identifier for the gwl. </summary>
        public static readonly int GWL_ID = (-12);
        /// <summary>   The ds contexthelp. </summary>
        public static readonly int DS_CONTEXTHELP = 0x2000;
        /// <summary>   The ws ex contexthelp. </summary>
        public static readonly int WS_EX_CONTEXTHELP = 0x00000400;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets window long. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="nIndex">   Zero-based index of the. </param>
        ///
        /// <returns>   The window long. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sets window long. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">         The window. </param>
        /// <param name="nIndex">       Zero-based index of the. </param>
        /// <param name="dwNewLong">    The new long. </param>
        ///
        /// <returns>   An int. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first window ex. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hwndParent">       The parent. </param>
        /// <param name="hwndChildAfter">   The child after. </param>
        /// <param name="lpszClass">        The class. </param>
        /// <param name="lpszWindow">       The window. </param>
        ///
        /// <returns>   The found window ex. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first window ex. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hwndParent">       The parent. </param>
        /// <param name="hwndChildAfter">   The child after. </param>
        /// <param name="lpszClass">        The class. </param>
        /// <param name="windowTitle">      The window title. </param>
        ///
        /// <returns>   The found window ex. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, IntPtr windowTitle);


        /// <summary>   pszPath is a pidl. </summary>
        public static readonly uint SHGFI_PIDL = 0x000000008;
        /// <summary>   get attributes. </summary>
        public static readonly uint SHGFI_ATTRIBUTES = 0x000000800;

        /// <summary>   Shortcut (link) </summary>
        public static readonly uint SFGAO_LINK = 0x00010000;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   A shfileinfo. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///-------------------------------------------------------------------------------------------------

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            /// <summary>   The icon. </summary>
            public IntPtr hIcon;
            /// <summary>   Zero-based index of the icon. </summary>
            public int iIcon;
            /// <summary>   The attributes. </summary>
            public uint dwAttributes;
            /// <summary>   Name of the display. </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            /// <summary>   Name of the type. </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        //[DllImport("shell32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Sh get file information. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="pidlPath">         Full pathname of the PIDL file. </param>
        /// <param name="dwFileAttributes"> The file attributes. </param>
        /// <param name="psfi">             [in,out] The psfi. </param>
        /// <param name="cbFileInfo">       Information describing the file. </param>
        /// <param name="uFlags">           The flags. </param>
        ///
        /// <returns>   An IntPtr. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(IntPtr pidlPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbFileInfo, uint uFlags);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enables the window. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd">     The window. </param>
        /// <param name="bEnable">  True to enable, false to disable. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a device context. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd"> The window. </param>
        ///
        /// <returns>   The device context. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Releases the device context. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd"> The window. </param>
        /// <param name="hDC">  The device-context. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets text extent point 32. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hdc">      The hdc. </param>
        /// <param name="lpString"> The string. </param>
        /// <param name="cbString"> The string. </param>
        /// <param name="lpSize">   [out] The size. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("gdi32.dll")]
        public static extern bool GetTextExtentPoint32(IntPtr hdc, string lpString, int cbString, out Size lpSize);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Select object. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hdc">      The hdc. </param>
        /// <param name="hgdiobj">  The hgdiobj. </param>
        ///
        /// <returns>   An IntPtr. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets desktop window. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <returns>   The desktop window. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the parent of this item. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="hWnd"> The window. </param>
        ///
        /// <returns>   The parent. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern IntPtr CreateWindowEx(
        //   WindowStylesEx dwExStyle,
        //   string lpClassName,
        //   string lpWindowName,
        //   WindowStyles dwStyle,
        //   int x,
        //   int y,
        //   int nWidth,
        //   int nHeight,
        //   IntPtr hWndParent,
        //   IntPtr hMenu,
        //   IntPtr hInstance,
        //   IntPtr lpParam);

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates window ex. </summary>
        ///
        /// <remarks>   Mark Alicz, 10/4/2019. </remarks>
        ///
        /// <param name="dwExStyle">    The ex style. </param>
        /// <param name="lpClassName">  Name of the class. </param>
        /// <param name="lpWindowName"> Name of the window. </param>
        /// <param name="dwStyle">      The style. </param>
        /// <param name="x">            The x coordinate. </param>
        /// <param name="y">            The y coordinate. </param>
        /// <param name="nWidth">       The width. </param>
        /// <param name="nHeight">      The height. </param>
        /// <param name="hWndParent">   The window parent. </param>
        /// <param name="hMenu">        The menu. </param>
        /// <param name="hInstance">    The instance. </param>
        /// <param name="lpParam">      The parameter. </param>
        ///
        /// <returns>   The new window ex. </returns>
        ///-------------------------------------------------------------------------------------------------

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreateWindowEx(
           uint dwExStyle,
           string lpClassName,
           string lpWindowName,
           uint dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);
    }
}
