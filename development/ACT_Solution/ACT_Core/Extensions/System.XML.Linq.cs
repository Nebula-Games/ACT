///-------------------------------------------------------------------------------------------------
// file:	Extensions\System.XML.Linq.cs
//
// summary:	Implements the system. xml. linq class
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// XML Linq Extensions
    /// </summary>
    public static class SystemXMLLinqExtensions
    {
        /// <summary>Gets the first (in document order) child element with the specified <see cref="XName"/>.</summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The <see cref="XName"/> to match.</param>
        /// <param name="ignoreCase">If set to <c>true</c> case will be ignored whilst searching for the <see cref="XElement"/>.</param>
        /// <returns>A <see cref="XElement"/> that matches the specified <see cref="XName"/>, or null. </returns>
        public static XElement Element(this XElement element, XName name, bool ignoreCase)
        {
            var el = element.Element(name);

            if (el != null) { return el; }
            if (!ignoreCase) { return null; }

            var elements = element.Elements().Where(e => e.Name.LocalName.ToString().ToLowerInvariant() == name.ToString().ToLowerInvariant());
            return elements.Count() == 0 ? null : elements.First();
        }

        /// <summary>
        /// Gets the element desendants
        /// </summary>
        /// <param name="xelement"></param>
        /// <param name="elementname"></param>
        /// <param name="ignorecase"></param>
        /// <returns></returns>
        public static List<XElement> GetElementDesendants(this XElement xelement, string elementname, bool ignorecase)
        {
            //  var _query = from x in xelement.Descendants() where x.Name.LocalName
            if (xelement == null) { return null; }
            var _items = xelement.Descendants().ToList().Where(x => x.Name.LocalName.ToLower() == elementname).ToList();
            return _items;
        }

        /// <summary>
        /// Get All Attributes
        /// </summary>
        /// <param name="xelement">Element To Query</param>      
        /// <param name="makeNameLowerCase">Make all the attribute names lower case</param>
        /// <returns>List(string,string)</returns>
        public static Dictionary<string, string> GetAllAttributes(this XElement xelement, bool makeNameLowerCase = false)
        {
            //  var _query = from x in xelement.Descendants() where x.Name.LocalName
            if (xelement == null) { return null; }

            Dictionary<string, string> _tmp = new Dictionary<string, string>();

            foreach (var tmpAttribute in xelement.Attributes())
            {
                string _Name = tmpAttribute.Name.LocalName;
                if (makeNameLowerCase) { _Name = _Name.ToLower(); }

                _tmp.Add(_Name, tmpAttribute.Value.ToString());
            }

            _tmp.Add("ElementValue", xelement.Value);

            return _tmp;
        }
    }
}
