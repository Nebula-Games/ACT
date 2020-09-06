///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Configuration\I_ConfigurationFile.cs
//
// summary:	Declares the I_ConfigurationFile interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ACT.Core.Interfaces.Configuration
{
    public interface I_ConfigurationFile
    {
        List<I_ConfigurationProject> Projects { get; set; }

    }
}
