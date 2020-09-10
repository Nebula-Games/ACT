// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Library.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using ACT.Core.Extensions;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;


namespace ACT.Core.Dynamic
{
    /// <summary>
    /// Class Library.
    /// </summary>
    public static class Library
    {
        /// <summary>
        /// Executes A Static Method that executes the defined method and Returns a Data Table
        /// </summary>
        /// <param name="DLL">DLL to Execute From</param>
        /// <param name="ClassName">Class Name in the DLL to load</param>
        /// <param name="MethodName">Method Name to execute</param>
        /// <param name="Params">Params to load</param>
        /// <returns>DataTable</returns>
        public static System.Data.DataTable ExecuteDataTableMethod(string DLL, string ClassName, string MethodName, object[] Params = null)
        {
            System.Data.DataTable _TmpReturn = null;
            var ErrorLogger = ACT.Core.CurrentCore<ACT.Core.Interfaces.Common.I_ErrorLoggable>.GetCurrent();
            Assembly assembly = Assembly.LoadFile(DLL);
            Type type = assembly.GetType(ClassName);
            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod(MethodName);
                if (methodInfo != null)
                {
                    if (methodInfo.IsStatic)
                    {

                        if (methodInfo.ReturnType == typeof(System.Data.DataTable))
                        {
                            try
                            {

                                ParameterInfo[] parameters = methodInfo.GetParameters();

                                if (parameters.Length == 0)
                                {
                                    _TmpReturn = (System.Data.DataTable)methodInfo.Invoke(null, null);
                                }
                                else
                                {
                                    _TmpReturn = (System.Data.DataTable)methodInfo.Invoke(null, Params);
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError("ACT.Core.Dynamic.Library", "Failed To Execute Method.", ex, DLL + " - " + ClassName + " - " + MethodName, Enums.ErrorLevel.Severe);
                            }
                        }
                        else
                        {

                            ErrorLogger.LogError("ACT.Core.Dynamic.Library", "Method Specified is not of type DataTable", null, DLL + " - " + ClassName + " - " + MethodName, Enums.ErrorLevel.Warning);
                        }
                    }
                    else
                    {
                        ErrorLogger.LogError("ACT.Core.Dynamic.Library", "Method Was Not Static.  Must use static methods", null, DLL + " - " + ClassName + " - " + MethodName, Enums.ErrorLevel.Warning);

                    }
                }
                else
                {
                    ErrorLogger.LogError("ACT.Core.Dynamic.Library", "Method Not Found", null, DLL + " - " + ClassName + " - " + MethodName, Enums.ErrorLevel.Warning);
                }
            }

            ErrorLogger = null;
            return _TmpReturn;
        }

        /// <summary>
        /// Load The DLL and return an instance of the specified class
        /// </summary>
        /// <typeparam name="T">Type to Return</typeparam>
        /// <param name="FileLocation">Location of the dll file</param>
        /// <param name="ClassName">Class Name To Return</param>
        /// <param name="Arguments">Any Arguments needed for the init of the class, default is null</param>
        /// <returns>instance of class</returns>
        /// <exception cref="System.TypeLoadException">
        /// Plugin is not of Type " + typeof(T).FullName
        /// or
        /// Error Locating " + typeof(T).FullName
        /// </exception>
        public static T LoadDLL<T>(string FileLocation, string ClassName, List<object> Arguments = null)
        {
            if (FileLocation.ToLower().EndsWith(".dll") == false) { return default(T); }

            string _Directory = FileLocation.GetDirectoryFromFileLocation();

            if (_Directory.NullOrEmpty())
            {
                string _BasePath = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
                _Directory = _BasePath.FindFileReturnPath(FileLocation);
            }

            FileLocation = _Directory.EnsureDirectoryFormat() + FileLocation.GetFileName(true);

            if (FileLocation.NullOrEmpty()) { return default(T); }

            // Long Term Depreciation Issues
            System.Reflection.Assembly _A = System.Reflection.Assembly.LoadFile(FileLocation);

            string _ClassName = "";

            try
            {
                bool _FullBreak = false;
                foreach (var x in _A.GetExportedTypes())
                {
                    foreach (var y in x.GetInterfaces())
                    {
                        if (y.FullName == typeof(T).FullName)
                        {
                            _ClassName = x.FullName;

                            if (ClassName == x.Name)
                            {
                                _FullBreak = true;
                                break;
                            }
                        }                        
                    }
                    if (_FullBreak) { break; }
                }

                if (_ClassName == "")
                {
                    throw new System.TypeLoadException("Plugin is not of Type " + typeof(T).FullName);
                }
            }
            catch (Exception ex)
            {
                throw new System.TypeLoadException("Error Locating " + typeof(T).FullName, ex);
            }

            if (Arguments == null)
            {
                return (T)_A.CreateInstance(_ClassName, true, System.Reflection.BindingFlags.CreateInstance, null, null, System.Globalization.CultureInfo.CurrentCulture, null);
            }
            else
            {
                return (T)_A.CreateInstance(_ClassName, true, System.Reflection.BindingFlags.CreateInstance, null, Arguments.ToArray(), System.Globalization.CultureInfo.CurrentCulture, null);
            }
        }

        /// <summary>
        /// Loads all of the available defined types from a Dll.  Must be blank constructors
        /// </summary>
        /// <typeparam name="T">Type to look for</typeparam>
        /// <param name="FileLocation">DLL File Locations</param>
        /// <returns>List of all the DLLS Loaded</returns>
        /// <exception cref="System.TypeLoadException">
        /// Plugin is not of Type " + typeof(T).FullName
        /// or
        /// Error Locating " + typeof(T).FullName
        /// </exception>
        public static List<T> LoadDLL<T>(string FileLocation)
        {
            List<T> _TmpReturn = new List<T>();
            if (FileLocation.ToLower().EndsWith(".dll") == false) { return _TmpReturn; }

            string _Directory = FileLocation.GetDirectoryFromFileLocation();

            if (_Directory.NullOrEmpty())
            {
                string _BasePath = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
                FileLocation = _BasePath.FindFileReturnPath(FileLocation).EnsureDirectoryFormat() + FileLocation;
            }

            if (FileLocation.NullOrEmpty()) { return _TmpReturn; }

            // Long Term Depreciation Issues
            System.Reflection.Assembly _A = System.Reflection.Assembly.LoadFile(FileLocation);

            string _ClassName = "";

            try
            {
                bool _Found = false;
                foreach (var x in _A.GetExportedTypes())
                {
                    foreach (var y in x.GetInterfaces())
                    {
                        if (y.FullName == typeof(T).FullName)
                        {
                            _ClassName = x.FullName;
                            _TmpReturn.Add((T)_A.CreateInstance(_ClassName, true, System.Reflection.BindingFlags.CreateInstance, null, null, System.Globalization.CultureInfo.CurrentCulture, null));
                            _Found = true;
                        }
                    }
                }
                if (_Found == false)
                {
                    throw new System.TypeLoadException("Plugin is not of Type " + typeof(T).FullName);
                }
            }
            catch (Exception ex)
            {
                throw new System.TypeLoadException("Error Locating " + typeof(T).FullName, ex);
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Loads all of the available defined types from a Dll.  Must be blank constructors
        /// </summary>
        /// <typeparam name="T">Type to look for</typeparam>
        /// <param name="SearchAssembly">Assembly to Search</param>
        /// <returns>List of all the DLLS Loaded</returns>
        /// <exception cref="System.TypeLoadException">
        /// Plugin is not of Type " + typeof(T).FullName
        /// or
        /// Error Locating " + typeof(T).FullName
        /// </exception>
        public static List<T> LoadDLL<T>(Assembly SearchAssembly)
        {
            List<T> _TmpReturn = new List<T>();

            if (SearchAssembly == null) { return _TmpReturn; }

            // Long Term Depreciation Issues
            System.Reflection.Assembly _A = SearchAssembly;

            string _ClassName = "";

            try
            {
                bool _Found = false;
                foreach (var x in _A.GetExportedTypes())
                {
                    foreach (var y in x.GetInterfaces())
                    {
                        if (y.FullName == typeof(T).FullName)
                        {
                            _ClassName = x.FullName;
                            _TmpReturn.Add((T)_A.CreateInstance(_ClassName, true, System.Reflection.BindingFlags.CreateInstance, null, null, System.Globalization.CultureInfo.CurrentCulture, null));
                            _Found = true;
                        }
                    }
                }
                if (_Found == false)
                {
                    throw new System.TypeLoadException("Plugin is not of Type " + typeof(T).FullName);
                }
            }
            catch (Exception ex)
            {
                throw new System.TypeLoadException("Error Locating " + typeof(T).FullName, ex);
            }

            return _TmpReturn;
        }


        //static Lazy<CSharpCodeProvider> CodeProvider { get; } = new Lazy<CSharpCodeProvider>(() => {
        //    var csc = new CSharpCodeProvider();
        //    var settings = csc
        //        .GetType()
        //        .GetField("_compilerSettings", BindingFlags.Instance | BindingFlags.NonPublic)
        //        .GetValue(csc);

        //    var path = settings
        //        .GetType()
        //        .GetField("_compilerFullPath", BindingFlags.Instance | BindingFlags.NonPublic);

        //    path.SetValue(settings, ((string)path.GetValue(settings)).Replace(@"bin\roslyn\", @"roslyn\"));

        //    return csc;
        //});

        /// <summary>
        /// Compiles the code.
        /// </summary>
        /// <param name="Code">The code.</param>
        /// <returns>Assembly.</returns>
        public static Assembly CompileCode(string Code)
        {
            // Drew was here... 
            var csc = CodeProvider;

            var parameters = new CompilerParameters(new[] { "System.dll" });

            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Data.dll");
            parameters.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            parameters.ReferencedAssemblies.Add("System.XML.dll");
            parameters.ReferencedAssemblies.Add("System.XML.Linq.dll");
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll"))
            {
                parameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "ACT.Core.dll");
            }
            else if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll"))
            {
                parameters.ReferencedAssemblies.Add(AppDomain.CurrentDomain.BaseDirectory + "Bin\\ACT.Core.dll");
            }

            parameters.GenerateExecutable = false;
            CompilerResults results = csc.Value.CompileAssemblyFromSource(parameters, Code);
            results.Errors.Cast<CompilerError>().ToList().ForEach(error => System.Console.WriteLine(error.ErrorText));
            return results.CompiledAssembly;
        }
    }


}
