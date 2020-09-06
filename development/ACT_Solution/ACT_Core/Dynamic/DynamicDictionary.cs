// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="DynamicDictionary.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace ACT.Core.Dynamic
{
    /// <summary>
    /// The class derived from DynamicObject.
    /// Implements the <see cref="System.Dynamic.DynamicObject" />
    /// </summary>
    /// <seealso cref="System.Dynamic.DynamicObject" />
    public class DynamicDictionary : DynamicObject
    {
        // The inner dictionary.
        /// <summary>
        /// The dictionary
        /// </summary>
        internal Dictionary<string, object> dictionary = new Dictionary<string, object>();

        /// <summary>
        /// This property returns the number of elements
        /// in the inner dictionary.
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
        /// not defined in the class, this method is called.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name.ToLower();

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dictionary.TryGetValue(name, out result);
        }

        /// <summary>
        /// If you try to set a value of a property that is
        /// not defined in the class, this method is called.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
        /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name.ToLower()] = value.ToString();

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }

        /// <summary>
        /// To Json
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dictionary, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// From JSON
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns>DynamicDictionary.</returns>
        public static DynamicDictionary FromJson(string json)
        {
            
            DynamicDictionary _tmpReturn = new DynamicDictionary();
            _tmpReturn.dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return _tmpReturn;
        }
    }
}
