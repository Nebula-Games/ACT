// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 02-26-2019
// ***********************************************************************
// <copyright file="System.Data.DbType.cs" company="Stonegate Intel LLC">
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
    /// Class DataExtensions.
    /// </summary>
    public static partial class DataExtensions
    {
        /// <summary>
        /// Converts to sqldatatype.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="IncludeBrackets">if set to <c>true</c> [include brackets].</param>
        /// <returns>System.String.</returns>
        /// <exception cref="Exception">Type Not Currently Supported</exception>
        public static string ToSQLDataType(this Type x, bool IncludeBrackets = false)
        {
            string _TmpReturn = "";
            string _Size="";

            if (x == typeof(int))
            {
                _TmpReturn= "int";
            }
            else if (x == typeof(DateTime))
            {
                _TmpReturn = "datetime";
            }
            else if (x == typeof(string))
            {
                _TmpReturn = "nvarchar";
                _Size = "(MAX)";
            }
            else if (x == typeof(Guid))
            {
                _TmpReturn = "uniqueidentifier";
            }
            else if (x == typeof(double))
            {
                _TmpReturn = "float";
            }
            else if (x == typeof(float))
            {
                _TmpReturn = "float";
            }
            else if (x == typeof(bool))
            {
                _TmpReturn = "bit";
            }
            else if (x == typeof(byte[]))
            {
                _TmpReturn = "varbinary";
                _Size = "(MAX)";
            }
            

            if (_TmpReturn != "")
            {
                if (IncludeBrackets)
                {
                    return "[" + _TmpReturn + "]" + _Size;
                }
                else
                {
                    return _TmpReturn + " " + _Size;
                }
            }
            else
            {
                throw new Exception("Type Not Currently Supported");
            }
        }

        /// <summary>
        /// Converts a System.Data.DbType to a System.Data.SqlDbType
        /// </summary>
        /// <param name="DBType">In DBType</param>
        /// <returns>System.Data.SqlDbType</returns>
        /// <exception cref="Exception">Error Converting DataType: " + DBType.ToString()</exception>
        public static System.Data.SqlDbType ToSQLDataType(this System.Data.DbType DBType)
        {

            switch (DBType)
            {
                case System.Data.DbType.Boolean:
                    return System.Data.SqlDbType.Bit;
                case System.Data.DbType.Binary:
                    return System.Data.SqlDbType.Image;
                case System.Data.DbType.Object:
                    return System.Data.SqlDbType.Structured;
                case System.Data.DbType.String:
                    return System.Data.SqlDbType.NVarChar;
                case System.Data.DbType.StringFixedLength:
                    return System.Data.SqlDbType.NVarChar;
                case System.Data.DbType.AnsiString:
                    return System.Data.SqlDbType.Text;
                case System.Data.DbType.AnsiStringFixedLength:
                    return System.Data.SqlDbType.VarChar;
                case System.Data.DbType.Byte:
                    return System.Data.SqlDbType.SmallInt;
                case System.Data.DbType.Currency:
                    return System.Data.SqlDbType.Money;
                case System.Data.DbType.Date:
                    return System.Data.SqlDbType.Date;
                case System.Data.DbType.DateTime:
                    return System.Data.SqlDbType.DateTime;
                case System.Data.DbType.DateTime2:
                    return System.Data.SqlDbType.DateTime2;
                case System.Data.DbType.DateTimeOffset:
                    return System.Data.SqlDbType.DateTimeOffset;
                case System.Data.DbType.Decimal:
                    return System.Data.SqlDbType.Decimal;
                case System.Data.DbType.Double:
                    return System.Data.SqlDbType.Float;
                case System.Data.DbType.Guid:
                    return System.Data.SqlDbType.UniqueIdentifier;
                case System.Data.DbType.Int16:
                    return System.Data.SqlDbType.SmallInt;
                case System.Data.DbType.Int32:
                    return System.Data.SqlDbType.Int;
                case System.Data.DbType.Int64:
                    return System.Data.SqlDbType.BigInt;
                case System.Data.DbType.SByte:
                    return System.Data.SqlDbType.SmallInt;
                case System.Data.DbType.Single:
                    return System.Data.SqlDbType.Decimal;
                case System.Data.DbType.Time:
                    return System.Data.SqlDbType.Time;
                case System.Data.DbType.UInt16:
                    return System.Data.SqlDbType.Int;
                case System.Data.DbType.UInt32:
                    return System.Data.SqlDbType.BigInt;
                case System.Data.DbType.UInt64:
                    return System.Data.SqlDbType.BigInt;
                case System.Data.DbType.VarNumeric:
                    return System.Data.SqlDbType.BigInt;
                case System.Data.DbType.Xml:
                    return System.Data.SqlDbType.Xml;
                default:
                    throw new Exception("Error Converting DataType: " + DBType.ToString());

            }
        }

        /// <summary>
        /// Converts DbType to String
        /// </summary>
        /// <param name="DBType">DBType</param>
        /// <param name="MaxSize">Max Size</param>
        /// <returns>LowerCase String</returns>
        /// <exception cref="Exception"></exception>
        public static string ToDBStringCustom(this System.Data.DbType DBType, int MaxSize = 50)
        {
            switch (DBType)
            {
                case System.Data.DbType.AnsiString:
                    return "varchar(max)";
                case System.Data.DbType.AnsiStringFixedLength:
                    return "varchar(" + MaxSize.ToString() + ")";
                case System.Data.DbType.Binary:
                    return "binary";
                case System.Data.DbType.Boolean:
                    return "bit";
                case System.Data.DbType.Byte:
                    return "tinyint";
                case System.Data.DbType.Currency:
                    return "money";
                case System.Data.DbType.Date:
                    return "datetime";
                case System.Data.DbType.DateTime:
                    return "datetime";
                case System.Data.DbType.DateTime2:
                    return "datetime";
                case System.Data.DbType.DateTimeOffset:
                    return "datetime";
                case System.Data.DbType.Decimal:
                    return "decimal";
                case System.Data.DbType.Double:
                    return "float";
                case System.Data.DbType.Guid:
                    return "uniqueidentifier";
                case System.Data.DbType.Int16:
                    return "smallint";
                case System.Data.DbType.Int32:
                    return "int";
                case System.Data.DbType.Int64:
                    return "bigint";
                case System.Data.DbType.Object:
                    return "image";
                case System.Data.DbType.String:
                    return "nvarchar(max)";
                case System.Data.DbType.StringFixedLength:
                    return "nvarchar(" + MaxSize.ToString() +")";
                default:
                    throw new Exception(DBType.ToString() + " Not supported");
            }
        }


        /// <summary>
        /// Converts DbType to String
        /// </summary>
        /// <param name="DBType">DBType</param>
        /// <returns>LowerCase String</returns>
        /// <exception cref="Exception"></exception>
        public static string ToCSharpString(this System.Data.DbType DBType)
        {
            switch (DBType)
            {
                case System.Data.DbType.AnsiString:
                    return "string";
                case System.Data.DbType.AnsiStringFixedLength:
                    return "string";
                case System.Data.DbType.Binary:
                    return "byte[]";
                case System.Data.DbType.Boolean:
                    return "bool";
                case System.Data.DbType.Byte:
                    return "int";
                case System.Data.DbType.Currency:
                    return "double";
                case System.Data.DbType.Date:
                    return "DateTime";
                case System.Data.DbType.DateTime:
                    return "DateTime";
                case System.Data.DbType.DateTime2:
                    return "DateTime";
                case System.Data.DbType.DateTimeOffset:
                    return "DateTime";
                case System.Data.DbType.Decimal:
                    return "double";
                case System.Data.DbType.Double:
                    return "double";
                case System.Data.DbType.Guid:
                    return "Guid";
                case System.Data.DbType.Int16:
                    return "int";
                case System.Data.DbType.Int32:
                    return "int";
                case System.Data.DbType.Int64:
                    return "long";
                case System.Data.DbType.Object:
                    return "byte[]";
                case System.Data.DbType.String:
                    return "string";
                case System.Data.DbType.StringFixedLength:
                    return "string";
                default:
                    throw new Exception(DBType.ToString() + " Not supported");
            }
        }

        /// <summary>
        /// Converts DbType to Nullable String
        /// </summary>
        /// <param name="DBType">DBType</param>
        /// <returns>LowerCase String</returns>
        /// <exception cref="Exception"></exception>
        public static string ToCSharpStringNullable(this System.Data.DbType DBType)
        {
            switch (DBType)
            {
                case System.Data.DbType.AnsiString:
                    return "string";
                case System.Data.DbType.AnsiStringFixedLength:
                    return "string";
                case System.Data.DbType.Binary:
                    return "byte[]";
                case System.Data.DbType.Boolean:
                    return "bool?";
                case System.Data.DbType.Byte:
                    return "int?";
                case System.Data.DbType.Currency:
                    return "decimal?";
                case System.Data.DbType.Date:
                    return "DateTime?";
                case System.Data.DbType.DateTime:
                    return "DateTime?";
                case System.Data.DbType.DateTime2:
                    return "DateTime?";
                case System.Data.DbType.DateTimeOffset:
                    return "DateTime?";
                case System.Data.DbType.Decimal:
                    return "decimal?";
                case System.Data.DbType.Double:
                    return "double?";
                case System.Data.DbType.Guid:
                    return "Guid?";
                case System.Data.DbType.Int16:
                    return "int?";
                case System.Data.DbType.Int32:
                    return "int?";
                case System.Data.DbType.Int64:
                    return "long?";
                case System.Data.DbType.Object:
                    return "byte[]";
                case System.Data.DbType.String:
                    return "string";
                case System.Data.DbType.StringFixedLength:
                    return "string";
                default:
                    throw new Exception(DBType.ToString() + " Not supported");
            }
        }

        /// <summary>
        /// Determines whether [is string type] [the specified database type].
        /// </summary>
        /// <param name="DBType">Type of the database.</param>
        /// <returns><c>true</c> if [is string type] [the specified database type]; otherwise, <c>false</c>.</returns>
        public static bool IsStringType(this System.Data.DbType DBType)
        {
            if (DBType.ToCSharpString() == "string")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
