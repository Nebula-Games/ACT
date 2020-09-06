// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_Test.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.Common
{
    /// <summary>
    /// Interface I_Test
    /// </summary>
    public interface I_Test
    {
        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>The messages.</value>
        Dictionary<string, List<string>> Messages { get; }
        /// <summary>
        /// Gets a value indicating whether [overall success].
        /// </summary>
        /// <value><c>true</c> if [overall success]; otherwise, <c>false</c>.</value>
        bool OverallSuccess { get; }
        /// <summary>
        /// Gets the method test results.
        /// </summary>
        /// <value>The method test results.</value>
        Dictionary<string, bool> MethodTestResults { get; }

        /// <summary>
        /// Gets the method names.
        /// </summary>
        /// <value>The method names.</value>
        List<string> MethodNames { get; }

        /// <summary>
        /// Gets the method definitions.
        /// </summary>
        /// <value>The method definitions.</value>
        Dictionary<string, ACT.Core.Common.Code.CodeSignature> MethodDefinitions { get; }

        /// <summary>
        /// Executes the tests.
        /// </summary>
        /// <param name="TestsToPerform">The tests to perform.</param>
        void ExecuteTests(List<ACT.Core.Common.Code.CodeSignature> TestsToPerform);
        /// <summary>
        /// Executes the tests.
        /// </summary>
        void ExecuteTests();
        /// <summary>
        /// Executes the tests.
        /// </summary>
        /// <param name="TestsToPerform">The tests to perform.</param>
        void ExecuteTests(List<string> TestsToPerform);
    }
}
