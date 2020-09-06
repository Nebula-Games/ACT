///-------------------------------------------------------------------------------------------------
// file:	Types\Communication\ACT_Email_Attachment.cs
//
// summary:	Implements the act email attachment class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Types.Communication
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   An act email attachment. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/19/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public struct ACT_Email_Attachment
    {
        /// <summary>   Filename of the file. </summary>
        public string FileName;
        /// <summary>   The file data base 64. </summary>
        public string FileData_Base64;
        /// <summary>   Type of the mime. </summary>
        public string MIMEType;
    }
}
