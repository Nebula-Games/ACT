// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Generic.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class Generic.
    /// </summary>
    public static class Generic
    {
        /// <summary>
        /// Ins the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">source</exception>
        public static bool In<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source);
        }

        /// <summary>
        /// Serializes an object of type T in to an xml string
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="obj">Object to serialize</param>
        /// <returns>A string that represents Xml, empty otherwise</returns>
        /// <exception cref="ArgumentNullException">obj</exception>
        public static string XmlSerialize<T>(this T obj) where T : class, new()
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var serializer = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Deserializes an xml string in to an object of Type T
        /// </summary>
        /// <typeparam name="T">Any class type</typeparam>
        /// <param name="xml">Xml as string to deserialize from</param>
        /// <returns>A new object of type T is successful, null if failed</returns>
        /// <exception cref="ArgumentNullException">xml</exception>
        public static T XmlDeserialize<T>(this string xml) where T : class, new()
        {
            if (xml == null) throw new ArgumentNullException("xml");

            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                try { return (T)serializer.Deserialize(reader); }
                catch { return null; } // Could not be deserialized to this type.
            }
        }

        /// <summary>
        /// Class DynamicXml.
        /// Implements the <see cref="System.Dynamic.DynamicObject" />
        /// </summary>
        /// <seealso cref="System.Dynamic.DynamicObject" />
        public class DynamicXml : DynamicObject
        {
            /// <summary>
            /// The root
            /// </summary>
            XElement _root;
            /// <summary>
            /// Initializes a new instance of the <see cref="DynamicXml"/> class.
            /// </summary>
            /// <param name="root">The root.</param>
            private DynamicXml(XElement root)
            {
                _root = root;
            }

            /// <summary>
            /// Parses the specified XML string.
            /// </summary>
            /// <param name="xmlString">The XML string.</param>
            /// <returns>DynamicXml.</returns>
            public static DynamicXml Parse(string xmlString)
            {
                return new DynamicXml(XDocument.Parse(xmlString).Root);
            }

            /// <summary>
            /// Loads the specified filename.
            /// </summary>
            /// <param name="filename">The filename.</param>
            /// <returns>DynamicXml.</returns>
            public static DynamicXml Load(string filename)
            {
                return new DynamicXml(XDocument.Load(filename).Root);
            }

            /// <summary>
            /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
            /// </summary>
            /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
            /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
            /// <returns><see langword="true" /> if the operation is successful; otherwise, <see langword="false" />. If this method returns <see langword="false" />, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)</returns>
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = null;

                var att = _root.Attribute(binder.Name);
                if (att != null)
                {
                    result = att.Value;
                    return true;
                }

                var nodes = _root.Elements(binder.Name);
                if (nodes.Count() > 1)
                {
                    result = nodes.Select(n => new DynamicXml(n)).ToList();
                    return true;
                }

                var node = _root.Element(binder.Name);
                if (node != null)
                {
                    if (node.HasElements)
                    {
                        result = new DynamicXml(node);
                    }
                    else
                    {
                        result = node.Value;
                    }
                    return true;
                }

                return true;
            }
        }

        /// <summary>
        /// Clones the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentException">The type must be serializable. - source</exception>
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

    }
}
