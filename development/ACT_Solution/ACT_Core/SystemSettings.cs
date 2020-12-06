using System;
using System.Collections.Generic;
using System.Text;
using ACT.Core.Extensions;
using System.Linq;
using sysc = ACT.Core.Types.SystemConfiguration;
using System.Reflection;

namespace ACT.Core
{
    /// <summary>
    /// System Settings Version 2.0 Using The New System Configuration File    /// 
    /// </summary>
    /// <dependson>
    ///     <class>ACT.Core.SystemStatus</class>
    /// </dependson>
    public static class SystemSettings
    {
        /// <summary>
        /// Private SystemConfiguration Property
        /// </summary>
        private static Types.SystemConfiguration.SystemConfiguration _SystemConfiguration = null;

        public static ACT.Core.Interfaces.Common.I_TestResult MeetsExpectations(ACT.Core.Interfaces.Common.I_Plugin pluginObject)
        {
            return null;
        }

        public static List<string> SettingKeys
        {
            get
            {                
                return _SystemConfiguration.SettingKeys;
            }
        }
        public static void Reloaded()
        {
            _SystemConfiguration = SystemStatus.LoadedConfigurationFile;
        }

        public static bool Load(string LocationOverride = "")
        {
            // CREATES POINTER TO SystemStatus Class Object
            if (_SystemConfiguration == null)
            {
                _SystemConfiguration = SystemStatus.LoadedConfigurationFile;
            }

            return true;
        }

        public static void GenerateSystemSettingsTemplate()
        {
            sysc.SystemConfiguration _SysConfig = new Types.SystemConfiguration.SystemConfiguration();
            _SysConfig.FileVersion = sysc.SystemConfiguration.CurrentFileVersion;
            _SysConfig.BasicSettings = new List<sysc.BasicSetting>();

            var _Asm = Assembly.GetExecutingAssembly();
            foreach(Type typ in _Asm.GetTypes().Where(x => x.IsInterface))
            {
                string _fullName = typ.FullName;
                var _Att = typ.GetAttributeValue(typeof(ACT.Core.CustomAttributes.ClassID));
                
                

            }
        }

        public static bool HasSettingWithValue(string SettingName)
        {
            if (GetSettingByName(SettingName) == null) { return false; }
            else { return true; }
        }

        public static dynamic GetSettingByName(string SettingName)
        {
            if (_SystemConfiguration.BasicSettings.Exists(x => x.Name.ToLower() == SettingName.ToLower()))
            {
                return _SystemConfiguration.BasicSettings.FirstOrDefault(x => x.Name.ToLower() == SettingName.ToLower());
            }

            if (_SystemConfiguration.ComplexSettings.Exists(x => x.Name.ToLower() == SettingName.ToLower()))
            {
                return _SystemConfiguration.ComplexSettings.FirstOrDefault(x => x.Name.ToLower() == SettingName.ToLower());
            }

            if (_SystemConfiguration.Plugins.Exists(x => x.FullClassName.ToLower() == SettingName.ToLower()))
            {
                return _SystemConfiguration.Plugins.FirstOrDefault(x => x.FullClassName.ToLower() == SettingName.ToLower());
            }

            if (_SystemConfiguration.Applications.Exists(x => x.Name.ToLower() == SettingName.ToLower()))
            {
                return _SystemConfiguration.Applications.FirstOrDefault(x => x.Name.ToLower() == SettingName.ToLower());
            }

            return null;
        }
    }
}
