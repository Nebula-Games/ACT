// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="CodeSignature.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Enums.Code;

using ACT.Core.Extensions;

namespace ACT.Core.Common.Code
{


    /// <summary>
    /// Class CodeParameter.
    /// </summary>
    public class CodeParameter
    {
        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public string Parameter { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CodeParameter"/> is optional.
        /// </summary>
        /// <value><c>true</c> if optional; otherwise, <c>false</c>.</value>
        public bool Optional { get; set; }
        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        /// <value>The name of the type.</value>
        public string TypeName { get; set; }
    }
    /// <summary>
    /// Class CodeComment.
    /// </summary>
    public class CodeComment
    {
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public string Summary { get; set; }
        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>The remarks.</value>
        public string Remarks { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public List<CodeParameter> Parameters { get; set; }
        /// <summary>
        /// Gets or sets the name of the return type.
        /// </summary>
        /// <value>The name of the return type.</value>
        public string ReturnTypeName { get; set; }
    }

    /// <summary>
    /// A code signature.
    /// </summary>
    /// <remarks>Mark Alicz, 12/19/2016.</remarks>
    public class CodeSignature
    {
        /// <summary>
        /// Gets or sets the type of the return.
        /// </summary>
        /// <value>The type of the return.</value>
        public string ReturnType { get; set; }
        /// <summary>
        /// Gets or sets the interfaces.
        /// </summary>
        /// <value>The interfaces.</value>
        public List<string> Interfaces { get; set; }
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>The name of the class.</value>
        public string ClassName { get; set; }
        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }
        /// <summary>
        /// Gets or sets the name space.
        /// </summary>
        /// <value>The name space.</value>
        public string NameSpace { get; set; }
        /// <summary>
        /// Gets or sets the raw comments.
        /// </summary>
        /// <value>The raw comments.</value>
        public string RawComments { get; set; }
        /// <summary>
        /// Gets or sets the type of the signature.
        /// </summary>
        /// <value>The type of the signature.</value>
        public CodeSignature_ItemType SignatureType { get; set; }

        /// <summary>
        /// Try get assembly.
        /// </summary>
        /// <param name="SearchPaths">(Optional) the search paths.</param>
        /// <param name="DeepSearch">(Optional) true to deep search.</param>
        /// <param name="Architecture64Bit">(Optional) true to architecture 64 bit.</param>
        /// <returns>A System.Reflection.Assembly.</returns>
        /// <exception cref="Exception">Not Implemented</exception>
        /// <remarks>Mark Alicz, 12/19/2016.</remarks>
        public System.Reflection.Assembly TryGetAssembly(string[] SearchPaths = null, bool DeepSearch = false, bool Architecture64Bit = false)
        {
            throw new Exception("Not Implemented");
        }
    }
}
