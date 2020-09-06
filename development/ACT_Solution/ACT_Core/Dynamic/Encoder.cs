// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Encoder.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;

namespace ACT.Core.Dynamic
{

    /// <summary>
    /// Class ACTDynamic.
    /// Implements the <see cref="System.Dynamic.DynamicObject" />
    /// </summary>
    /// <seealso cref="System.Dynamic.DynamicObject" />
    public class ACTDynamic : System.Dynamic.DynamicObject
    {
        /// <summary>
        /// The dictionary
        /// </summary>
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        /// <summary>
        /// The children
        /// </summary>
        Dictionary<string, dynamic> Children = new Dictionary<string, dynamic>();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>

        [CustomAttributes.DevelopmentStatus(Comments ="Needs Work", Completed = false, LastAuthor = "Mark Alicz", LastUpdateDate = "8/2/2017")]
        
        public object Name
        {
            get
            {
                return dictionary["name"];
            }
        }

        /// <summary>
        /// Gets the get member names.
        /// </summary>
        /// <value>The get member names.</value>
        public List<string> GetMemberNames
        {
            get
            {
                var _Dic = dictionary.Select(x => x.Key).ToList();
                _Dic.Remove("name");
                return _Dic;
            }
        }
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }
        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
        public override bool TryGetMember(
            System.Dynamic.GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            return dictionary.TryGetValue(name, out result);
        }
        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
        /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
        public override bool TrySetMember(
            System.Dynamic.SetMemberBinder binder, object value)
        {
            dictionary[binder.Name.ToLower()] = value;
            return true;
        }
        /// <summary>
        /// Sets the member.
        /// </summary>
        /// <param name="Member">The member.</param>
        /// <param name="Value">The value.</param>
        public void SetMember(string Member, object Value)
        {

            if (dictionary.ContainsKey(Member.ToLower()))
            {                
                dictionary[Member.ToLower()] = Value;
            }
            else
            {
                dictionary.Add(Member.ToLower(), Value);
            }

        }
        /// <summary>
        /// Adds the replace child.
        /// </summary>
        /// <param name="ChildName">Name of the child.</param>
        /// <param name="child">The child.</param>
        public void AddReplaceChild(string ChildName, dynamic child)
        {
            if (Children.ContainsKey(ChildName))
            {
                Children[ChildName] = child;
            }
            else
            {
                Children.Add(ChildName, child);
            }
        }
        /// <summary>
        /// Gets the child.
        /// </summary>
        /// <param name="ChildName">Name of the child.</param>
        /// <returns>dynamic.</returns>
        public dynamic GetChild(string ChildName)
        {
            if (Children.ContainsKey(ChildName.ToLower()))
            {
                return Children[ChildName.ToLower()];
            }
            else { return null; }

        }
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>System.Object.</returns>
        public object GetObject(string Name)
        {
            if (dictionary.ContainsKey(Name.ToLower()))
            {
                return dictionary[Name.ToLower()];
            }
            else
            {
                return null;
            }
        }
    }
    /// <summary>
    /// Class Encoder.
    /// </summary>
    public static class Encoder
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public static string Description
        {
            get
            {
                return "This Class Reads and Creates Based On A New Format Called ACT Dynamic.  For Full Info see Documentation.";
            }
        }
        /// <summary>
        /// Loads from string.
        /// </summary>
        /// <param name="FileData">The file data.</param>
        /// <returns>dynamic.</returns>
        public static dynamic LoadFromString(string FileData)
        {
            dynamic _TmpReturn = new ACTDynamic();

            string[] _FileData = FileData.SplitString(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            for (int pos = 0; pos < _FileData.Length; pos++)
            {
                string _LineData = _FileData[pos];
                if (_LineData.StartsWith("{"))
                {
                    dynamic _TmpParsed = LoadFilePart(_FileData, pos, out pos);

                    (_TmpReturn as ACTDynamic).SetMember(_TmpParsed.Name, _TmpParsed);
                }
                else
                {
                    try
                    {
                        string _PropertyName = _LineData.SplitString(":", StringSplitOptions.RemoveEmptyEntries)[0];
                        string _PropertyValue = _LineData.SplitString(":", StringSplitOptions.RemoveEmptyEntries)[1];
                        (_TmpReturn as ACTDynamic).SetMember(_PropertyName, _PropertyValue);

                    }
                    catch { }
                }
            }

            _TmpReturn.ClassType = "ACT.Core.Dynamic.ACTDynamic";

            return _TmpReturn;
        }
        /// <summary>
        /// Loads from file.
        /// </summary>
        /// <param name="FileName">Name of the file.</param>
        /// <returns>dynamic.</returns>
        public static dynamic LoadFromFile(string FileName)
        {
            dynamic _TmpReturn = new ACTDynamic();

            if (!System.IO.File.Exists(FileName))
            {
                return null;
            }

            string[] _FileData = System.IO.File.ReadAllLines(FileName);

            for (int pos = 0; pos < _FileData.Length; pos++)
            {
                string _LineData = _FileData[pos];
                if (_LineData.StartsWith("{"))
                {
                    dynamic _TmpParsed = LoadFilePart(_FileData, pos, out pos);

                    (_TmpReturn as ACTDynamic).SetMember(_TmpReturn.name, _TmpParsed);
                }
                else
                {
                    try
                    {
                        string _PropertyName = _LineData.SplitString(":", StringSplitOptions.RemoveEmptyEntries)[0];
                        string _PropertyValue = _LineData.SplitString(":", StringSplitOptions.RemoveEmptyEntries)[1];
                        (_TmpReturn as ACTDynamic).SetMember(_PropertyName, _PropertyValue);

                    }
                    catch {  }
                }
            }

            _TmpReturn.ClassType = "ACT.Core.Dynamic.ACTDynamic";

            return _TmpReturn;
        }
        /// <summary>
        /// Loads the file part.
        /// </summary>
        /// <param name="FileData">The file data.</param>
        /// <param name="StartPosition">The start position.</param>
        /// <param name="EndPosition">The end position.</param>
        /// <returns>dynamic.</returns>
        internal static dynamic LoadFilePart(string[] FileData, int StartPosition, out int EndPosition)
        {
            bool _FoundEnd = false;
            dynamic _TmpReturn = new ACTDynamic();
            string _LastName = "";

            while (_FoundEnd == false)
            {
                string ObjectName = "";
                for (int pos = StartPosition; pos < FileData.Length; pos++)
                {
                    string LineData = FileData[pos].Trim();

                    if (pos == StartPosition)
                    {
                        ObjectName = LineData.Replace("{", "").Trim();
                        continue;
                    }

                    if (LineData.Trim().StartsWith("}"))
                    {
                        EndPosition = pos;
                        _FoundEnd = true;
                        return _TmpReturn;
                    }

                    if (LineData.StartsWith("{"))
                    {
                        dynamic _TmpParsed = LoadFilePart(FileData, pos, out pos);

                       // (_TmpReturn as ACTDynamic).SetMember(_TmpParsed.Name, _TmpParsed);
                        (_TmpReturn as ACTDynamic).SetMember(_LastName, _TmpParsed);
                       // (_TmpReturn as ACTDynamic).chil.SetMember("children", _TmpParsed);
                        continue;
                    }

                    string _PropertyName = LineData.SplitString(":", StringSplitOptions.RemoveEmptyEntries)[0];
                    string _PropertyValue = LineData.SplitString(":", StringSplitOptions.RemoveEmptyEntries)[1];

                    if (_PropertyName.ToLower() == "name") { _LastName = _PropertyValue; }

                    (_TmpReturn as ACTDynamic).SetMember(_PropertyName, _PropertyValue.Replace("\\n",Environment.NewLine));

                }
            }

            EndPosition = FileData.Length;
            return _TmpReturn;
        }
        /// <summary>
        /// Saves me.
        /// </summary>
        /// <param name="Me">Me.</param>
        /// <returns>System.String.</returns>
        public static string SaveMe(object Me)
        {
            return "Not Implemented";
        }
    }
}
