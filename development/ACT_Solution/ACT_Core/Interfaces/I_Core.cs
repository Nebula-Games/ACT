// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Core.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Interfaces
{
    /// <summary>
    /// Core Functionality Definition
    /// Implements the <see cref="System.IDisposable" />
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_ErrorLoggable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="ACT.Core.Interfaces.Common.I_ErrorLoggable" />
    public interface I_Core : IDisposable, I_ErrorLoggable
    {
        /// <summary>
        /// Standard Text Replacement Functionality
        /// </summary>
        /// <param name="instr">The instr.</param>
        /// <param name="InputStandard">The input standard.</param>
        /// <returns>System.String.</returns>
        string StandardReplaceMent(string instr, Enums.RepacementStandard InputStandard);

        /// <summary>
        /// Imports the variable XML into the current class
        /// </summary>
        /// <param name="XML">XML Data to Import</param>
        /// <returns>true on success</returns>
        I_TestResult ImportXMLData(string XML);

        /// <summary>
        /// Exports the current class to XML
        /// </summary>
        /// <returns>XML Representation of class</returns>
        string ExportXMLData();

        /// <summary>
        /// Returns the Errors stored in the local variable
        /// </summary>
        /// <returns><![CDATA[List<Exception>]]></returns>
        List<Exception> GetErrors();

        /// <summary>
        /// Specifies if the class has changed in any way
        /// </summary>
        /// <value><c>true</c> if this instance has changed; otherwise, <c>false</c>.</value>
        bool HasChanged { get; set; }

        /// <summary>
        /// Get all of the Public Properties in the class
        /// </summary>
        /// <value>The public properties.</value>
        List<string> PublicProperties { get; }

        /// <summary>
        /// Return a property value by name
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>System.Object.</returns>
        object ReturnProperty(string PropertyName);

        /// <summary>
        /// Returns the type of the property
        /// </summary>
        /// <param name="PropertyName">Name of the property.</param>
        /// <returns>Type.</returns>
        Type ReturnPropertyType(string PropertyName);

        /// <summary>
        /// Trys to set a property using the propertyname and the value. Case sensitive people.
        /// </summary>
        /// <param name="PropertyName">Case Sensitive Property Name</param>
        /// <param name="value">value</param>
        /// <returns>I_TestResult - Specifying if the Set was successfull</returns>
        I_TestResult SetProperty(string PropertyName, object value);

        /// <summary>
        /// Checks The Health Of The Class.  Use this to return missing configuration.  Invalid Permissions Etc..
        /// </summary>
        /// <returns>I_TestResult - Specifying changes needed to be made to obtain a good health report.</returns>
        I_TestResult HealthCheck();
    }

  

   
}
