///-------------------------------------------------------------------------------------------------
// file:	Constants\Windows\IO\MIME_Types.cs
//
// summary:	Implements the mime types class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using ACT.Core.Extensions;

namespace ACT.Core.Constants.Windows.IO
{
    public static class MimeTypes
    {
        public static string ResourceNameConfigFile = "MIME_TYPES_JSON";

        private static bool _UsingConfigurationFileData = false;
        static ACT.Core.Types.Data.MimeTypes CachedData = null;
        public static bool UsingConfigurationFileData { get { return _UsingConfigurationFileData; } }

        private static string ResourceLocation = "Data\\MIME_TYPES.json";

        //private static Types.Data.MimeTypes _loadedMIMETypes = null;
        static MimeTypes()
        {
            string _FileLocation = "";
            _FileLocation = SystemStatus.GetDirectoryPath(true, Enums.Installation.CoreDirectories.Resources) + ResourceLocation; // Will Throw Error

            if (_FileLocation.FileExists() == false) { throw new System.IO.FileNotFoundException(_FileLocation + " Was not Found"); }
            dynamic _SettingOverride = ACT.Core.SystemSettings.GetSettingByName(ResourceNameConfigFile);

            if (_SettingOverride.Valid) { _FileLocation = _FileLocation.Replace("MIME_TYPES.json", _SettingOverride.value); }

            CachedData = ACT.Core.Types.Data.MimeTypes.FromJson(_FileLocation);
        }



        public static string FindByValue(string Val)
        {
            return "";
        }

    }


}

