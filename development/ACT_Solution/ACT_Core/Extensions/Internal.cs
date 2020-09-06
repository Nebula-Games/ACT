// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="Internal.cs" company="Nebula Entertainment LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// Class InternalExtensions.
    /// </summary>
    public static class InternalExtensions
    {


        /// <summary>
        /// Generates the primary key select list MSSQL.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <returns>System.String.</returns>
        public static string GeneratePrimaryKeySelectListMSSQL(this ACT.Core.Interfaces.DataAccess.I_DbTable Table)
        {
            string _TmpReturn = "";

            foreach (var c in Table.Columns)
            {
                if (c.IsPrimaryKey)
                {
                    if (Table.Name.StartsWith("["))
                    {
                        _TmpReturn = Table.Name.Substring(Table.Name.IndexOf("[dbo].[") + 7).TrimEnd("]") + "." + c.ShortName + ", ";
                        //_TmpReturn += Table.Name + "." + c.ShortName + ", ";
                    }
                    else
                    {
                        _TmpReturn += "[" + Table.Name + "].[" + c.ShortName + "], ";

                    }
                }
            }

            return _TmpReturn.TrimEnd(", ".ToCharArray());
        }

        /// <summary>
        /// Generates the primary key parameter list casting.
        /// </summary>
        /// <param name="Table">The table.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GeneratePrimaryKeyParameterListCasting(this ACT.Core.Interfaces.DataAccess.I_DbTable Table)
        {
            List<string> _TmpReturn =  new List<string>();
            
            foreach (var c in Table.Columns)
            {
                if (c.IsPrimaryKey)
                {
                    _TmpReturn.Add("(" + c.DataType.ToCSharpStringNullable() + ")");
                }
            }

            return _TmpReturn;
        }
    }
}
