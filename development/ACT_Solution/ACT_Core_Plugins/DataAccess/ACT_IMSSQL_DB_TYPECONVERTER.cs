using ACT.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ACT.Plugins.DataAccess
{


    /// <summary>
    /// Type Converter For MSSQL
    /// </summary>
    public class ACT_IMSSQL_DB_TYPECONVERTER : ACT.Core.Interfaces.DataAccess.I_Db_TypeConverter
    {
        private readonly string[] _ValidTypes;
        private Dictionary<string, bool> _TypesWithLengthHasTwo = new Dictionary<string, bool>();

        /// <summary>
        /// Generic Constructor Setup Properties
        /// </summary>
        public ACT_IMSSQL_DB_TYPECONVERTER()
        {
            _ValidTypes = new string[] { "bigint","int","smallint","tinyint","bit","decimal","numeric","money","nvarchar(max)","varchar(max)","varbinary(max)",
                    "smallmoney","float","real","datetime","smalldatetime","char","varchar","text","nchar","nvarchar","ntext","binary","varbinary",
                    "image","cursor","sql_variant","table","timestamp","uniqueidentifier" };

            _TypesWithLengthHasTwo = new Dictionary<string, bool>();
            _TypesWithLengthHasTwo.Add("decimal", true);
            _TypesWithLengthHasTwo.Add("varchar", false);
            _TypesWithLengthHasTwo.Add("varchar(max)", false);
            _TypesWithLengthHasTwo.Add("nvarchar(max)", false);
            _TypesWithLengthHasTwo.Add("char", false);
            _TypesWithLengthHasTwo.Add("nchar", false);
            _TypesWithLengthHasTwo.Add("ntext", false);
            _TypesWithLengthHasTwo.Add("text", false);
            _TypesWithLengthHasTwo.Add("binary", false);
            _TypesWithLengthHasTwo.Add("image", false);
            _TypesWithLengthHasTwo.Add("varbinary", false);
            _TypesWithLengthHasTwo.Add("varbinary(max)", false);
        }

        #region Properties

        public string SourceType { get; set; }
        public int SourceLengthA { get; set; }
        public int SourceLengthB { get; set; }
        public object SourceValue { get; set; }
        public string DestType { get; set; }
        public int DestLengthA { get; set; }
        public int DestLengthB { get; set; }
        public object DestValue { get; set; }

        /// <summary>
        /// Types With Length
        /// </summary>
        public Dictionary<string, bool> TypesWithLengthHasTwo
        {
            get { return _TypesWithLengthHasTwo; }
        }

        /// <summary>
        /// Valid Types
        /// </summary>
        public string[] ValidTypes
        {
            get
            {
                return _ValidTypes;
            }
        }

        #endregion

        /// <summary>
        /// Convert
        /// </summary>
        /// <returns></returns>
        public object Convert()
        {


            if (DestType.ToLower() == "int") { if (SourceValue.ToNullableInt() == null) { return null; } else { return SourceValue.ToInt(); } }
            if (DestType.ToLower() == "float") { return SourceValue.ToFloat(); }
            if (DestType.ToLower() == "decimal") { return SourceValue.ToDecimal(); }
            if (DestType.ToLower() == "datetime") { return SourceValue.ToDateTime(); }
            if (DestType.ToLower() == "nvarchar" || DestType.ToLower() == "nvarchar(max)") { return SourceValue.ToString(); }
            if (DestType.ToLower() == "char" || DestType.ToLower() == "nchar") { return SourceValue.ToString(); }


            return null;
            //object _tmpReturn;

            //if (SourceType.ToLower() == "nvarchar(max)" || SourceType.ToLower() == "varchar(max)" ||
            //    SourceType.ToLower() == "nvarchar" || SourceType.ToLower() == "varchar"
            //    || SourceType.ToLower() == "nchar" || SourceType.ToLower() == "char"
            //    || SourceType.ToLower() == "ntext" || SourceType.ToLower() == "text")
            //{
            //    if (DestType.ToLower() == "int") { if (SourceValue.ToNullableInt() == null) { return null; } else { return SourceValue.ToInt(); } }

            //}

            //return _tmpReturn;
        }

        private bool ValidConfig()
        {
            if (SourceType.NullOrEmpty() || DestType.NullOrEmpty()) { return false; }
            if (ValidTypes.Contains(SourceType.ToLower()) == false || ValidTypes.Contains(SourceType.ToLower()) == false) { return false; }

            return true;
        }
    }
}
