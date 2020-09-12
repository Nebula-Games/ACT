///-------------------------------------------------------------------------------------------------
// file:	Validation\FileType_Validations.cs
//
// summary:	Implements the file type validations class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using ACT.Core.Enums;
using ACT.Core.Interfaces.Common;
using ACT.Core.Interfaces.Security.Authentication;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ACT.Core.Validation
{
    public static class FileType_Validations
    {

        public static bool IsFileValid(string MIMEType)
        {
            return false;
            //ACT.Core.SystemSettings.GetSettingByName("");
               
            
        }
      
    }
}
