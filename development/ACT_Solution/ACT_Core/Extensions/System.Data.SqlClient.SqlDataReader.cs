// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Data.SqlClient.SqlDataReader.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Helper.Database;

namespace ACT.Core.Extensions
{

    /// <summary>
    /// Class DataExtensions.
    /// </summary>
    public static partial class DataExtensions
    {
        /// <summary>
        /// Converts to datatable.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <returns>System.Data.DataTable.</returns>
        public static System.Data.DataTable ToDataTable(this System.Data.SqlClient.SqlDataReader Reader)
        {
            System.Data.DataTable _TmpReturn;
            using (InternalDataAdapter _IDA = new InternalDataAdapter())
            {
                _TmpReturn = _IDA.ConvertToDataTable((System.Data.IDataReader)Reader);

            }

            return _TmpReturn;
        }
    }
}
