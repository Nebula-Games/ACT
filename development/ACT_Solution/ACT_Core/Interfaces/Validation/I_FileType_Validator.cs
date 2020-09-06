///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Validation\I_FileType_Validator.cs
//
// summary:	Declares the I_FileType_Validator interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Validation
{
    public interface I_FileType_Validator<T> : ACT.Core.Interfaces.Common.I_Plugin
    {
        bool IsValid(string FileContents);
        bool IsValid(T FileData);
        bool IsValid(object FileContents);
        bool IsValid(List<T> FileContents);
        bool IsValid(List<string> FileContents);
        bool IsValid(List<object> FileContents);
    }
}
