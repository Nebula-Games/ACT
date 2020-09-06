// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="I_ConsoleItemClicked.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Interfaces.Common;

namespace ACT.Core.Interfaces.ACTConsole
{
    /// <summary>
    /// Interface I_ConsoleItemClicked
    /// </summary>
    public interface I_ConsoleItemClicked
    {
        /// <summary>
        /// Executes the menu command.
        /// </summary>
        /// <param name="MenuCommand">The menu command.</param>
        /// <param name="OtherData">The other data.</param>
        /// <returns>I_TestResultExpanded.</returns>
        I_TestResultExpanded ExecuteMenuCommand(string MenuCommand, string[] OtherData);
    }
}
