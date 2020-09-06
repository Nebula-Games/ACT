///-------------------------------------------------------------------------------------------------
// file:	Interfaces\Common\I_Configurable.cs
//
// summary:	Declares the I_Configurable interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Common
{
    /// <summary>
    /// I Configurable - reads JSON Into a Setting Dictionary
    /// </summary>
    public interface I_Configurable
    {
        bool LoadConfiguration(string JSONData);
        bool SaveConfiguration(string FilePath);
        Dictionary<string, Types.SystemConfiguration.BasicSetting> ConfigurationSettings { get; set; }
    }
}

//{
//    "errorcodes":[
//    {
//    "name":"ACT.Core.Interfaces.Common.I_Configurable.LoadConfiguration.1",
//    "value":"707342220",
//    "action":"emailAdmin",
//    "displayerror":"Sorry something went wrong"
//    }
//    ]



//}