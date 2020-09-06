///-------------------------------------------------------------------------------------------------
// file:	Interfaces\DataAccess\I_DbFunctionParameter.cs
//
// summary:	Declares the I_DbFunctionParameter interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Interfaces.Common;
using System.ComponentModel;

namespace ACT.Core.Interfaces.DataAccess
{
    /// <summary>
    /// Interface I_DbStoredProcedureParameter
    /// Implements the <see cref="ACT.Core.Interfaces.Common.I_Plugin" />
    /// </summary>
    /// <seealso cref="ACT.Core.Interfaces.Common.I_Plugin" />
    public interface I_DbFunctionParameter : I_Plugin
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        /// <value>The type of the data.</value>
        System.Data.DbType DataType { get; set; }

        /// <summary>
        /// Gets or sets the name of the data type.
        /// </summary>
        /// <value>The name of the data type.</value>
        string DataTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the Default Value of the Parameter.
        /// </summary>
        /// <value>The Default Value.</value>
        string DefaultValue { get; set; }

    }
}
