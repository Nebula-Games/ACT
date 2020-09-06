// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-27-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-27-2019
// ***********************************************************************
// <copyright file="InternalDataAdapter.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Helper.Database
{

    /// <summary>
    /// This class Supports Turning DataReaders to DataTables
    /// Implements the <see cref="System.Data.Common.DbDataAdapter" />
    /// </summary>
    /// <seealso cref="System.Data.Common.DbDataAdapter" />
    public class InternalDataAdapter : System.Data.Common.DbDataAdapter
    {
        #region Methods

        /// <summary>
        /// Convert DataReader to DataTable
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <returns>System.Data.DataTable.</returns>
        public System.Data.DataTable ConvertToDataTable(System.Data.IDataReader Reader)
        {
            System.Data.DataTable _TmpReturn = new System.Data.DataTable();
            this.Fill(_TmpReturn, Reader);
            return _TmpReturn;
        }

        #endregion Methods
    }
}
