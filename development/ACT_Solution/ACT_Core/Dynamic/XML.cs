// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="XML.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using System.Globalization;

namespace ACT.Core.Dynamic.XML
{
    /// <summary>
    /// Class Encoder.
    /// </summary>
    public class Encoder
    {
        /// <summary>
        /// The known lists
        /// </summary>
        private static List<string> KnownLists;
        /// <summary>
        /// Parses the specified parent.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="node">The node.</param>
        /// <param name="knownLists">The known lists.</param>
        public static void Parse(dynamic parent, XElement node, List<string> knownLists = null)
        {
            if (knownLists != null)
            {
                KnownLists = knownLists;
            }
            IEnumerable<XElement> sorted = from XElement elt in node.Elements() orderby node.Elements(elt.Name.LocalName).Count() descending select elt;

            if (node.HasElements)
            {
                int nodeCount = node.Elements(sorted.First().Name.LocalName).Count();
                bool foundNode = false;
                if (KnownLists != null && KnownLists.Count > 0)
                {
                    foundNode = (from XElement el in node.Elements() where KnownLists.Contains(el.Name.LocalName) select el).Count() > 0;
                }

                if (nodeCount > 1 || foundNode == true)
                {
                    // At least one of the child elements is a list
                    var item = new ExpandoObject();
                    List<dynamic> list = null;
                    string elementName = string.Empty;
                    foreach (var element in sorted)
                    {
                        if (element.Name.LocalName != elementName)
                        {
                            list = new List<dynamic>();
                            elementName = element.Name.LocalName;
                        }

                        if (element.HasElements ||
                            (KnownLists != null && KnownLists.Contains(element.Name.LocalName)))
                        {
                            Parse(list, element);
                            AddProperty(item, element.Name.LocalName, list);
                        }
                        else
                        {
                            Parse(item, element);
                        }
                    }

                    foreach (var attribute in node.Attributes())
                    {
                        AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }

                    AddProperty(parent, node.Name.ToString(), item);
                }
                else
                {
                    var item = new ExpandoObject();

                    foreach (var attribute in node.Attributes())
                    {
                        AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }

                    //element
                    foreach (var element in sorted)
                    {
                        Parse(item, element);
                    }
                    if ((parent as IDictionary<String, object>) == null) { }
                    else
                    {
                        AddProperty(parent, node.Name.ToString(), item);
                    }
                }
            }
            else
            {
                AddProperty(parent, node.Name.ToString(), node.Value.Trim());
            }
        }

        /// <summary>
        /// Adds the property.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        private static void AddProperty(dynamic parent, string name, object value)
        {
            if (parent is List<dynamic>)
            {
                (parent as List<dynamic>).Add(value);
            }
            else
            {
                (parent as IDictionary<String, object>)[name] = value;
            }
        }
    }


    /// <summary>
    /// Class DynamicElement.
    /// Implements the <see cref="System.Dynamic.DynamicObject" />
    /// </summary>
    /// <seealso cref="System.Dynamic.DynamicObject" />
    public class DynamicElement : DynamicObject
    {
        /// <summary>
        /// The attributes
        /// </summary>
        private IDictionary<string, string> attributes = new Dictionary<string, string>();

        /// <summary>
        /// The sub elements
        /// </summary>
        internal IDictionary<string, dynamic> SubElements = new Dictionary<string, dynamic>();

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public string this[string key]
        {
            get { return attributes[key]; }
            set { attributes[key] = value; }
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = SubElements[binder.Name];
            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that set member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as setting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member to which the value is being assigned. For example, for the statement sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="value">The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the <paramref name="value" /> is "Test".</param>
        /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)</returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            SubElements[binder.Name] = value as dynamic;
            return true;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
    }

    /// <summary>
    /// Class XElementExtensions.
    /// </summary>
    public static class XElementExtensions
    {
        ///// <summary>
        ///// The pluralization service
        ///// </summary>
        //private static PluralizationService pluralizationService;

        ///// <summary>
        ///// Initializes static members of the <see cref="XElementExtensions"/> class.
        ///// </summary>
        //static XElementExtensions()
        //{
        //    pluralizationService = PluralizationService.CreateService(CultureInfo.CurrentCulture);
        //}

        /// <summary>
        /// Converts to dynamic.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>dynamic.</returns>
        public static dynamic ToDynamic(this XElement element)
        {
            Pluralize.NET.Core.Pluralizer _P = new Pluralize.NET.Core.Pluralizer();
            var item = new DynamicElement();

            // Add sub-elements
            if (element.HasElements)
            {
                var uniqueElements = element.Elements().Where(el => element.Elements().Count(el2 => el2.Name.LocalName.Equals(el.Name.LocalName)) == 1);
                var repeatedElements = element.Elements().Except(uniqueElements);

                foreach (var repeatedElementGroup in repeatedElements.GroupBy(re => re.Name.LocalName).OrderBy(el => el.Key))
                {
                    var list = new List<dynamic>();
                    foreach (var repeatedElement in repeatedElementGroup)
                        list.Add(ToDynamic(repeatedElement));

                    item.SubElements
                        .Add(_P.Pluralize(repeatedElementGroup.Key), list);
                }

                foreach (var uniqueElement in uniqueElements.OrderBy(el => el.Name.LocalName))
                {
                    item.SubElements
                        .Add(uniqueElement.Name.LocalName, ToDynamic(uniqueElement));
                }
            }

            // Add attributes, if any
            if (element.Attributes().Any())
            {
                foreach (var attribute in element.Attributes())
                    item[attribute.Name.LocalName] = attribute.Value;
            }

            // Add value
            item.Value = element.Value;

            return item;
        }
    }
}
