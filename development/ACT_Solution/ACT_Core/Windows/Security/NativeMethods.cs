///-------------------------------------------------------------------------------------------------
// file:	Windows\Security\NativeMethods.cs
//
// summary:	Implements the native methods class
///-------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using ACT.Core.Enums.Windows;

namespace ACT.Core.Windows.Security

{


    /// <summary> 
    /// The structure represents a security identifier (SID) and its  
    /// attributes. SIDs are used to uniquely identify users or groups. 
    /// </summary> 
    [StructLayout(LayoutKind.Sequential)]
    internal struct SID_AND_ATTRIBUTES
    {
        public IntPtr Sid;
        public UInt32 Attributes;
    }

    /// <summary> 
    /// The structure indicates whether a token has elevated privileges. 
    /// </summary> 
    [StructLayout(LayoutKind.Sequential)]
    internal struct TOKEN_ELEVATION
    {
        public Int32 TokenIsElevated;
    }

    /// <summary> 
    /// The structure specifies the mandatory integrity level for a token. 
    /// </summary> 
    [StructLayout(LayoutKind.Sequential)]
    internal struct TOKEN_MANDATORY_LABEL
    {
        public SID_AND_ATTRIBUTES Label;
    }

    /// <summary> 
    /// Represents a wrapper class for a token handle. 
    /// </summary> 
    internal class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeTokenHandle() : base(true)
        {
        }

        internal SafeTokenHandle(IntPtr handle) : base(true)
        {
            base.SetHandle(handle);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr handle);

        protected override bool ReleaseHandle()
        {
            return CloseHandle(base.handle);
        }
    }

    internal class NativeMethods
    {
        // Token Specific Access Rights 
        
        /// <summary> 
        /// The function opens the access token associated with a process. 
        /// </summary> 
        /// <param name="hProcess"> 
        /// A handle to the process whose access token is opened. 
        /// </param> 
        /// <param name="desiredAccess"> 
        /// Specifies an access mask that specifies the requested types of  
        /// access to the access token.  
        /// </param> 
        /// <param name="hToken"> 
        /// Outputs a handle that identifies the newly opened access token  
        /// when the function returns. 
        /// </param> 
        /// <returns></returns> 
        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr hProcess,
            UInt32 desiredAccess, out SafeTokenHandle hToken);


        /// <summary> 
        /// The function creates a new access token that duplicates one  
        /// already in existence. 
        /// </summary> 
        /// <param name="ExistingTokenHandle"> 
        /// A handle to an access token opened with TOKEN_DUPLICATE access. 
        /// </param> 
        /// <param name="ImpersonationLevel"> 
        /// Specifies a SECURITY_IMPERSONATION_LEVEL enumerated type that  
        /// supplies the impersonation level of the new token. 
        /// </param> 
        /// <param name="DuplicateTokenHandle"> 
        /// Outputs a handle to the duplicate token.  
        /// </param> 
        /// <returns></returns> 
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(
            SafeTokenHandle ExistingTokenHandle,
            SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
            out SafeTokenHandle DuplicateTokenHandle);


        /// <summary> 
        /// The function retrieves a specified type of information about an  
        /// access token. The calling process must have appropriate access  
        /// rights to obtain the information. 
        /// </summary> 
        /// <param name="hToken"> 
        /// A handle to an access token from which information is retrieved. 
        /// </param> 
        /// <param name="tokenInfoClass"> 
        /// Specifies a value from the TOKEN_INFORMATION_CLASS enumerated  
        /// type to identify the type of information the function retrieves. 
        /// </param> 
        /// <param name="pTokenInfo"> 
        /// A pointer to a buffer the function fills with the requested  
        /// information. 
        /// </param> 
        /// <param name="tokenInfoLength"> 
        /// Specifies the size, in bytes, of the buffer pointed to by the  
        /// TokenInformation parameter.  
        /// </param> 
        /// <param name="returnLength"> 
        /// A pointer to a variable that receives the number of bytes needed  
        /// for the buffer pointed to by the TokenInformation parameter.  
        /// </param> 
        /// <returns></returns> 
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetTokenInformation(
            SafeTokenHandle hToken,
            TOKEN_INFORMATION_CLASS tokenInfoClass,
            IntPtr pTokenInfo,
            Int32 tokenInfoLength,
            out Int32 returnLength);
        

        /// <summary> 
        /// Sends the specified message to a window or windows. The function  
        /// calls the window procedure for the specified window and does not  
        /// return until the window procedure has processed the message.  
        /// </summary> 
        /// <param name="hWnd"> 
        /// Handle to the window whose window procedure will receive the  
        /// message. 
        /// </param> 
        /// <param name="Msg">Specifies the message to be sent.</param> 
        /// <param name="wParam"> 
        /// Specifies additional message-specific information. 
        /// </param> 
        /// <param name="lParam"> 
        /// Specifies additional message-specific information. 
        /// </param> 
        /// <returns></returns> 
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, IntPtr lParam);


        /// <summary> 
        /// The function returns a pointer to a specified subauthority in a  
        /// security identifier (SID). The subauthority value is a relative  
        /// identifier (RID). 
        /// </summary> 
        /// <param name="pSid"> 
        /// A pointer to the SID structure from which a pointer to a  
        /// subauthority is to be returned. 
        /// </param> 
        /// <param name="nSubAuthority"> 
        /// Specifies an index value identifying the subauthority array  
        /// element whose address the function will return. 
        /// </param> 
        /// <returns> 
        /// If the function succeeds, the return value is a pointer to the  
        /// specified SID subauthority. To get extended error information,  
        /// call GetLastError. If the function fails, the return value is  
        /// undefined. The function fails if the specified SID structure is  
        /// not valid or if the index value specified by the nSubAuthority  
        /// parameter is out of bounds. 
        /// </returns> 
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSidSubAuthority(IntPtr pSid, UInt32 nSubAuthority);
    }
}