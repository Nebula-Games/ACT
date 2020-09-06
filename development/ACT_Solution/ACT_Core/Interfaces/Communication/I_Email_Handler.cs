///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Communication\I_Email_Handler.cs
//
// summary:	Declares the I_Email_Handler interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Communication
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Interface for email handler. </summary>
    ///
    /// <remarks>   Mark Alicz, 9/22/2019. </remarks>
    ///-------------------------------------------------------------------------------------------------
    public interface I_Email_Handler<T>
    {
        I_Email_Message Message { get; set; }

        Dictionary<string,string> Settings { get; set; }

        T Send();

        T Continue();
    }
}
