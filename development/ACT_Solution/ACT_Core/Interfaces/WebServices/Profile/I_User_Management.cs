///-------------------------------------------------------------------------------------------------
// file:	Interfaces\WebServices\Profile\I_User_Management.cs
//
// summary:	Declares the I_User_Management interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.WebServices.Profile
{
    public interface I_User_Management
    {

        string Register(string UserName, string Password, string EmailAddress, string MetaDataJSON);

        string UpdateInformation(string Token, string UserName, string Password, string MetaDataJSON);
    }
}
