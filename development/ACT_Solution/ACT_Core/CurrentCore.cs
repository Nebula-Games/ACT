// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-04-2019
// ***********************************************************************
// <copyright file="CurrentCore.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;


namespace ACT.Core
{
    /// <summary>
    /// Plugin Arguments Defines the Information needed to load a Assembly.
    /// </summary>
    [ACT.Core.CustomAttributes.ClassID("ACT.Core.PluginArguments")]
    public class PluginArguments
    {
        /// <summary>
        /// The loaded
        /// </summary>
        public bool Loaded = false;

        /// <summary>
        /// Full DLL Name (i.e) MyDLL.dll
        /// </summary>
        /// <value>The name of the DLL.</value>
        public string DLLName { get; set; }

        /// <summary>
        /// Full Class Name (i.e) MyNameSpace.MySub.MyClass
        /// </summary>
        /// <value>The full name of the class.</value>
        public string FullClassName { get; set; }

        /// <summary>
        /// Defines if the class should be treated like a singleton or not
        /// </summary>
        public bool StoreOnce = false;

        /// <summary>
        /// Optional Arguments the are required to create an instance of the class
        /// </summary>
        public List<object> Arguments = new List<object>();

        /// <summary>
        /// Empty Constructor for Generic Use
        /// </summary>
        public PluginArguments() { }

        /// <summary>
        /// Loads the Plugin Arguments From the SystemConfiguration File Settings
        /// </summary>
        /// <param name="Interface">The interface.</param>
        /// <exception cref="Exception">Error Locating System Setting: " + Interface</exception>
        public PluginArguments(string Interface)
        {
            string _Delimeter = ACT.Core.SystemSettings.GetSettingByName("Delimeter").Value;
            DLLName = ACT.Core.SystemSettings.GetSettingByName(Interface).Value;
            FullClassName = ACT.Core.SystemSettings.GetSettingByName(Interface + ".FullClassName").Value;
            string _StoreClass = ACT.Core.SystemSettings.GetSettingByName(Interface + ".StoreOnce").Value;

            if (FullClassName.NullOrEmpty() == true)
            {
                //ACT.Core.Helper.ErrorLogger.LogError(this, "Error Locating System Setting " + Interface, null, Enums.ErrorLevel.Critical);
                throw new Exception("Error Locating System Setting: " + Interface);
            }

            if (!String.IsNullOrEmpty(_StoreClass))
            {
                if (_StoreClass.ToLower() == "true")
                {
                    StoreOnce = true;
                }
            }

            string _Args = ACT.Core.SystemSettings.GetSettingByName(Interface + ".Args").Value;

            if (!String.IsNullOrEmpty(_Args))
            {
                string[] _Data = _Args.SplitString(_Delimeter, StringSplitOptions.RemoveEmptyEntries);

                foreach (string _x in _Data)
                {
                    Arguments.Add(_x);
                }
            }

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginArguments"/> class.
        /// </summary>
        /// <param name="Interface">The interface.</param>
        /// <param name="CustomSettings">The custom settings.</param>
        public PluginArguments(string Interface, SystemSettingsInstance CustomSettings)
        {
            if (CustomSettings.GetSettingByName(Interface) == null) { return; }
            if (CustomSettings.GetSettingByName("Delimeter") == null) { return; }
            if (CustomSettings.GetSettingByName(Interface + ".FullClassName") == null) { return; }
            if (CustomSettings.GetSettingByName(Interface + ".StoreOnce") == null) { return; }

            string _Delimeter = CustomSettings.GetSettingByName("Delimeter").Value;
            DLLName = CustomSettings.GetSettingByName(Interface).Value;
            FullClassName = CustomSettings.GetSettingByName(Interface + ".FullClassName").Value;
            string _StoreClass = CustomSettings.GetSettingByName(Interface + ".StoreOnce").Value;

            if (!String.IsNullOrEmpty(_StoreClass))
            {
                if (_StoreClass.ToLower() == "true")
                {
                    StoreOnce = true;
                }
            }

            try
            {
                string _Args = ACT.Core.SystemSettings.GetSettingByName(Interface + ".Args").Value;

                if (_Args.NullOrEmpty() == false)
                {
                    string[] _Data = _Args.SplitString(_Delimeter, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string _x in _Data)
                    {
                        Arguments.Add(_x);
                    }
                }
            }
            catch { }
        }
    }


    /// <summary>
    /// Current Core Represents the Entry Point for all Plugins.  Use this to gauruntee you get the defined plugin
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class CurrentCore<T>
    {
        /// <summary>
        /// Gets the cached assemblies.
        /// </summary>
        /// <value>The cached assemblies.</value>
        public static Dictionary<(Type, string), System.Reflection.Assembly> CachedAssemblies { get { return _CachedAssemblies; } }
        /// <summary>
        /// The cached assemblies
        /// </summary>
        private static Dictionary<(Type, string), System.Reflection.Assembly> _CachedAssemblies = new Dictionary<(Type, string), System.Reflection.Assembly>();
        /// <summary>
        /// The cached classes
        /// </summary>
        private static Dictionary<Type, object> _CachedClasses = new Dictionary<Type, object>();

        #region Methods (9)

        /// <summary>
        /// Adds the cached assembly.
        /// </summary>
        /// <param name="DLL">The DLL.</param>
        /// <param name="Type">The type.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AddCachedAssembly(string DLL, string Type)
        {
            System.Reflection.Assembly _A;

            try
            {
                _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + DLL);
            }
            catch
            {
                try
                {
                    _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + DLL);
                }
                catch
                {
                    try { _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "plugins\\" + DLL); }
                    catch
                    {
                        try { _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "bin\\plugins\\" + DLL); }
                        catch { return false; }
                    }
                }
            }
            if (_A == null) { return false; }
            else
            {
                var _T = System.Type.GetType(Type);
                if (_CachedAssemblies.ContainsKey((_T, _T.FullName)) == false)
                {
                    _CachedAssemblies.Add((_T, _T.FullName), _A);
                }
                return true;
            }
        }

        /// <summary>
        /// Installation Locations
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> ACTInstallLocations()
        {
            string[] _ACTLocations = new string[] { "c:\\act\\", "d:\\act\\", "d:\\program files\\ACT\\", "c:\\program files\\ACT\\", "D:\\Program Files (x86)\\ACT\\", "C:\\Program Files (x86)\\ACT\\" };

            List<string> _TmpReturn = new List<string>();

            foreach (string loc in _ACTLocations)
            {
                if (System.IO.File.Exists(loc + "SystemConfiguration.xml"))
                {
                    _TmpReturn.Add(loc);
                }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Public Method to Get a Built In Plugin
        /// </summary>
        /// <returns></returns>
        public static T GetBuiltIn() { return (T)GetBuiltInPlugin(typeof(T)); }

        /// <summary>
        /// Internal Method To Get Built In Plugin
        /// </summary>
        /// <param name="T">Type to Search For</param>
        /// <returns>object</returns>
        internal static object GetBuiltInPlugin(Type T)
        {
            if (T == typeof(Interfaces.Security.Hashing.I_ACT_SecureHash))
            {
                object _x = new ACT.Core.BuiltInPlugins.Security.Hashing.ACT_SecureHash();
                return (T)_x;
            }

            return null;
        }

        /// <summary>
        /// Gets the Current Default Interface Implementation as Defined in the Plugins Section of the Configuration File
        /// </summary>
        /// <returns>T.</returns>
        /// <exception cref="System.TypeLoadException">
        /// Error Locating " + typeof(T).FullName
        /// or
        /// Error Locating " + typeof(T).FullName
        /// </exception>
        public static T GetCurrent()
        {
            var _BuiltInPlugin = GetBuiltInPlugin(typeof(T));

            if (_BuiltInPlugin != null) { return (T)_BuiltInPlugin; }

            PluginArguments _PluginInfo;

            if (_CachedClasses.ContainsKey(typeof(T)))
            {
                if (_CachedClasses[typeof(T)] == null)
                {
                    _CachedClasses.Remove(typeof(T));
                }
                else
                {
                    return (T)_CachedClasses[typeof(T)];
                }
            }

            _PluginInfo = new PluginArguments(typeof(T).FullName);

            System.Reflection.Assembly _A;
            object _TmpClass;

            if (_CachedAssemblies.ContainsKey((typeof(T), typeof(T).FullName)))
            {
                _A = _CachedAssemblies[(typeof(T), typeof(T).FullName)];
                _TmpClass = _A.CreateInstance(_PluginInfo.FullClassName, true, System.Reflection.BindingFlags.CreateInstance, null, _PluginInfo.Arguments.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null);
            }
            else
            {
                if (_PluginInfo.DLLName != "")
                {
                    try
                    {
                        if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + _PluginInfo.DLLName))
                        {
                            _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + _PluginInfo.DLLName);
                        }
                        else if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + _PluginInfo.DLLName))
                        {
                            _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + _PluginInfo.DLLName);
                        }
                        else if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "bin\\plugins\\" + _PluginInfo.DLLName))
                        {
                            _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "bin\\plugins\\" + _PluginInfo.DLLName);
                        }
                        else
                        {
                            // Log Error
                            Helper.ErrorLogger.LogErrorToDisk("CurrentCore<>.GetCurrent() + Error locating DLL: " + _PluginInfo.DLLName);
                            throw new System.TypeLoadException("Error Locating " + typeof(T).FullName);                            
                        }

                        _CachedAssemblies.Add((typeof(T), typeof(T).FullName), _A);
                        _TmpClass = _A.CreateInstance(_PluginInfo.FullClassName, true, System.Reflection.BindingFlags.CreateInstance, null, _PluginInfo.Arguments.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null);

                    }
                    catch (Exception ex)
                    {
                        throw new System.TypeLoadException("Error Locating " + typeof(T).FullName, ex);
                    }

                }
                else
                {
                    _TmpClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(_PluginInfo.FullClassName);
                }
            }

            if (_PluginInfo.StoreOnce == true)
            {
                _CachedClasses.Add(typeof(T), _TmpClass);
            }

            return (T)_TmpClass;
        }

        /// <summary>
        /// Gets the Current Default Interface Implementation as Defined in the Plugins Section of the Configuration File
        /// </summary>
        /// <param name="SettingsInstance">The settings instance.</param>
        /// <returns>T.</returns>
        /// <exception cref="System.TypeLoadException">Error Locating " + typeof(T).FullName</exception>
        public static T GetCurrent(SystemSettingsInstance SettingsInstance)
        {
            PluginArguments _PluginInfo;

            if (_CachedClasses.ContainsKey(typeof(T)))
            {
                if (_CachedClasses[typeof(T)] == null)
                {
                    _CachedClasses.Remove(typeof(T));
                }
                else
                {
                    return (T)_CachedClasses[typeof(T)];
                }
            }

            _PluginInfo = new PluginArguments(typeof(T).FullName, SettingsInstance);

            System.Reflection.Assembly _A;
            object _TmpClass;

            if (_CachedAssemblies.ContainsKey((typeof(T), typeof(T).FullName)))
            {
                _A = _CachedAssemblies[(typeof(T), typeof(T).FullName)];
                _TmpClass = _A.CreateInstance(_PluginInfo.FullClassName, true, System.Reflection.BindingFlags.CreateInstance, null, _PluginInfo.Arguments.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null);
            }
            else
            {
                if (_PluginInfo.DLLName != "")
                {
                    try
                    {
                        try
                        {
                            _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + _PluginInfo.DLLName);
                        }
                        catch
                        {
                            _A = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "bin\\" + _PluginInfo.DLLName);
                        }

                        _CachedAssemblies.Add((typeof(T), typeof(T).FullName), _A);
                        _TmpClass = _A.CreateInstance(_PluginInfo.FullClassName, true, System.Reflection.BindingFlags.CreateInstance, null, _PluginInfo.Arguments.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null);

                    }
                    catch (Exception ex)
                    {
                        throw new System.TypeLoadException("Error Locating " + typeof(T).FullName, ex);
                    }

                }
                else
                {
                    _TmpClass = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance(_PluginInfo.FullClassName);
                }
            }

            if (_PluginInfo.StoreOnce == true)
            {
                _CachedClasses.Add(typeof(T), _TmpClass);
            }

            return (T)_TmpClass;
        }

        /// <summary>
        /// Returns the Interface implementation as defined by the custom plugin arguments
        /// </summary>
        /// <param name="Plugin">Plugin Arguments Defining Interface Implementation</param>
        /// <returns>T.</returns>
        /// <exception cref="System.TypeLoadException">
        /// Plugin is not of Type " + typeof(T).FullName
        /// or
        /// Error Locating " + typeof(T).FullName + " Plugin
        /// or
        /// Error Locating " + typeof(T).FullName + " Plugin
        /// </exception>
        public static T GetSpecific(PluginArguments Plugin)
        {
            T _TmpClass;
            System.Reflection.Assembly _A;

            if (!Plugin.DLLName.NullOrEmpty())
            {

                try
                {
                    _A = System.Reflection.Assembly.LoadFile(Plugin.DLLName);

                    if (!(_A is T))
                    {

                        throw new System.TypeLoadException("Plugin is not of Type " + typeof(T).FullName);
                    }
                }
                catch (Exception ex)
                {
                    throw new System.TypeLoadException("Error Locating " + typeof(T).FullName + " Plugin", ex);
                }
            }
            else
            {
                try
                {
                    _A = System.Reflection.Assembly.GetExecutingAssembly();
                }
                catch (Exception ex)
                {
                    throw new System.TypeLoadException("Error Locating " + typeof(T).FullName + " Plugin", ex);
                }
            }

            if (!_CachedAssemblies.ContainsKey((typeof(T), typeof(T).FullName)))
            {
                _CachedAssemblies.Add((typeof(T), typeof(T).FullName), _A);
            }
            _TmpClass = (T)_A.CreateInstance(Plugin.FullClassName, true, System.Reflection.BindingFlags.CreateInstance, null, Plugin.Arguments.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null);
            return _TmpClass;
        }

        #endregion Methods



    }
}
