///-------------------------------------------------------------------------------------------------
// file:	Interfaces\DataAccess\DatabaseManagement\I_Database_Manager.cs
//
// summary:	Declares the I_Database_Manager interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Interfaces.DataAccess.DatabaseManagement
{
    /// <summary>
    /// Database Manager
    /// </summary>
    public interface I_Database_Manager
    {
        /// <summary>
        /// Holds the BASE Data Access Object
        /// Implementation is All You
        /// </summary>
        I_DataAccess DataAccessObject { get; }

        /// <summary>
        /// Create a Table
        /// </summary>
        /// <param name="tableDefinition"></param>
        /// <returns>Test Result Expanded</returns>
        Common.I_TestResultExpanded CreateTable(I_DbTable tableDefinition);

        /// <summary>
        /// Create a Column
        /// </summary>
        /// <param name="columnDefinition"></param>
        /// <returns>Test Result Expanded</returns>
        Common.I_TestResultExpanded CreateColumn(I_DbColumn columnDefinition);

        /// <summary>
        /// Create a Stored Proc
        /// </summary>
        /// <param name="procDefinition"></param>
        /// <returns>Test Result Expanded</returns>
        Common.I_TestResultExpanded CreateStoredProc(I_DbStoredProcedure procDefinition);

        /// <summary>
        /// Create a Function
        /// </summary>
        /// <param name="functionDefinition"></param>
        /// <returns>Test Result Expanded</returns>
        Common.I_TestResultExpanded CreateFunction(I_DbStoredProcedure functionDefinition);


    }
}
