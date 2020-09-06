///-------------------------------------------------------------------------------------------------
// file:	Extensions\InternalExtensions\I_DataAccess_Extensions.cs
//
// summary:	Declares the I_DataAccess_Extensions interface
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACT.Core.Extensions;


namespace ACT.Core.Extensions
{
    public static class DataAccess_Extensions
    {
        public static Interfaces.DataAccess.I_DbTable CreateTable(string TableName, Dictionary<string, (System.Data.DbType, int)> Fields, string PrimaryKey)
        {
            string TableSQL = "CREATE TABLE [dbo].[" + TableName + "](" + Environment.NewLine;

            if (Fields.ContainsKey(PrimaryKey) == false) { throw new Exception("Error Locating Primary Key Field"); }

            foreach (var fld in Fields.Keys)
            {
                if (fld == PrimaryKey && Fields[fld].Item1 == System.Data.DbType.Int32 || Fields[fld].Item1 == System.Data.DbType.Int64 || Fields[fld].Item1 == System.Data.DbType.Int16)
                {
                    TableSQL += "[" + fld + "] [int] IDENTITY(1, 1) NOT NULL,";
                }
                else if (fld == PrimaryKey && Fields[fld].Item1 == System.Data.DbType.Guid)
                {
                    TableSQL += "[" + fld + "] [uniqueidentifier] NOT NULL,";
                }
                else
                {
                    string _typ = Fields[fld].Item1.ToDBStringCustom(Fields[fld].Item2);
                    TableSQL += "[" + fld + "] [nvarchar] (50) NOT NULL,";

                }
            }

            TableSQL += "CONSTRAINT[PK_Surveys] PRIMARY KEY CLUSTERED " + Environment.NewLine;
            TableSQL += "(" + Environment.NewLine;
            TableSQL += "[" + PrimaryKey + "] ASC" + Environment.NewLine;
            TableSQL += ") WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";
            return null;
        }
    }
}
